using System;
using System.Numerics;

namespace Game
{
    public class GenericEffect
    {
        private EffectsAnimations type = EffectsAnimations.PurpleBeam;
        private Animation anim;
        private Transform transform;
        private float speed = 10;
        private bool active = true;
        private float effectangle = 0;
        public bool Active => active;
        private static Random random = new Random();

        // Some delays
        private float time = 0.1f;
        private float currenttime = 0;

        // Movement values
        private float playerFirstX = -100;
        private float playerFirstY = -100;
        private float playerSecondX = -100;
        private float playerSecondY = -100;
        private float AbsoluteSpeed => speed * (this.anim.TextureSize.Length() / this.transform.Scale.Length()) / 2;

        // Actions
        public Action OnEffectFinished;

        public GenericEffect(EffectsAnimations newType) // Call ONLY when using the pool.
        {
            this.type = newType;
            this.anim = Textures.GetEffectAnimation(this.type);
            this.anim.OnAnimationFinished += RemoveMe;
        }
        public GenericEffect(EffectsAnimations newType, Transform newTransform)
        {
            this.type = newType;
            this.transform = newTransform;
            this.anim = Textures.GetEffectAnimation(this.type);
            this.anim.OnAnimationFinished += RemoveMe;
            mEffects.AddEffect(this);
        }
        public GenericEffect(EffectsAnimations newType, DoubleVector2 newVectors, float newAngle = 0)
        {
            this.type = newType;
            this.transform = new Transform(newVectors.Position, newVectors.Scale, newAngle);
            this.anim = Textures.GetEffectAnimation(this.type);
            this.anim.OnAnimationFinished += RemoveMe;
            mEffects.AddEffect(this);
        }
        public GenericEffect(EffectsAnimations newType, Vector2 spawnPosition, Vector2 scale, float newAngle = 0, bool useRandomRotation = false)
        {
            float angle = newAngle;
            if (useRandomRotation) angle = random.Next(0, 360);

            this.type = newType;
            this.transform = new Transform(spawnPosition, scale, angle);
            this.anim = Textures.GetEffectAnimation(this.type);
            this.anim.OnAnimationFinished += RemoveMe;
            mEffects.AddEffect(this);
        }

        public void Update(Vector2 playerPosition)
        {
            float t = Program.GetDeltaTime;

            this.anim.Update();
            float posX = this.transform.Position.X;
            float posY = this.transform.Position.Y;

            posY += speed * t;

            if (currenttime <= time)
            {
                playerFirstX = playerPosition.X;
                playerFirstY = playerPosition.Y;
                currenttime += Program.GetDeltaTime;
            }

            else if (currenttime >= time)
            {
                playerSecondX = playerPosition.X;
                playerSecondY = playerPosition.Y;
                currenttime = 0;
            }

            // Update this object following the player
            if (playerSecondX < playerFirstX) posX -= AbsoluteSpeed * t;
            if (playerSecondX > playerFirstX) posX += AbsoluteSpeed * t;
            if (playerSecondY < playerFirstY) posY -= AbsoluteSpeed * t;
            if (playerSecondY > playerFirstY) posY += AbsoluteSpeed * t;

            posY += AbsoluteSpeed * t;

            this.transform.UpdatePosition(new Vector2(posX, posY));
            Renderer.DrawCenter(this.anim.CurrentTexture, this.transform.Position, this.transform.Scale, effectangle);
        }

        public void Awake(Transform newTransform, bool useRandomRotation = false)
        {
            if (useRandomRotation) effectangle = random.Next(0, 360);

            this.playerFirstX = GameManager.PlayerPosition.X;
            this.playerFirstY = GameManager.PlayerPosition.Y;
            this.playerSecondX = GameManager.PlayerPosition.X;
            this.playerSecondY = GameManager.PlayerPosition.Y;

            this.transform = new Transform(newTransform.Position, newTransform.Scale, effectangle);
            this.anim.Play();
            mEffects.AddEffect(this);
        }

        public void Awake(Vector2 spawnPosition, Vector2 scale, float newAngle = 0, bool useRandomRotation = false)
        {
            effectangle = newAngle;
            if (useRandomRotation) effectangle = random.Next(0, 360);

            this.playerFirstX = GameManager.PlayerPosition.X;
            this.playerFirstY = GameManager.PlayerPosition.Y;
            this.playerSecondX = GameManager.PlayerPosition.X;
            this.playerSecondY = GameManager.PlayerPosition.Y;

            this.transform = new Transform(spawnPosition, scale, effectangle);
            this.anim.Play();
            mEffects.AddEffect(this);
        }

        private void RemoveMe() { OnEffectFinished?.Invoke(); mEffects.RemoveEffect(this); }
    }
}
