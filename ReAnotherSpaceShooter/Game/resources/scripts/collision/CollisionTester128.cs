using System;
using System.Numerics;

namespace Game
{
    public class CollisionTester128 : GameObject
    {
        // Stats
        public float life = 50;
        public float dmgCD = 1f;
        public float currentDMGcd = 0;

        // Events
        private Action OnObjectDeath;

        public CollisionTester128(Vector2 spawnPosition)
        {
            this.objectID = mGameObject.GenerateObjectID();
            this.objectOwner = "Enemy";
            this.objectTag = "Box128x128";
            this.objectTransform = new Transform(spawnPosition, new Vector2(1, 1));
            this.objectAnimation = Textures.GetDebugAnimation(DebugAnimation.Box128);
            this.objectCollider = new Collider(0, this.objectOwner, "Enemy", spawnPosition, this.objectAnimation.TextureSize, this.objectID);
            this.objectCollider.OnCollision += OnHit;

            mGameObject.AddGameObject(this);
            this.objectActive = true;
        }

        public override void Update()
        {
            if (this.objectActive)
            {
                if (currentDMGcd > 0) currentDMGcd -= Program.GetDeltaTime;

                this.objectAnimation.Update();
                this.objectCollider.UpdateOwnerPosition(objectTransform.Position);

                Renderer.DrawCenter(this.objectAnimation.CurrentTexture, this.objectTransform);
                this.objectCollider.CheckForCollisions();

                float posX = this.objectTransform.Position.X;
                float posY = this.objectTransform.Position.Y;
                posX += Program.GetDeltaTime;

                this.objectTransform.UpdatePosition(new Vector2(posX, posY));
            }
        }

        private void OnHit(Collider instigator)
        {
            if (currentDMGcd <= 0)
            {
                life -= instigator.Damage;
                currentDMGcd = dmgCD;
                //new GenericEffect(EffectsAnimations.Smoke1, this.Position - this.objectAnimation.TextureSize / 2 - new Vector2(17, 25), new Vector2(2, 2));

                var fx = fyPoolDay.Pool.CreateEffect(EffectsAnimations.Smoke1);
                fx.Awake(this.Position - this.objectAnimation.TextureSize / 2 - new Vector2(17, 25), new Vector2(2, 2));
            }

            if (life <= 0) Sleep();
        }

        public override void Sleep()
        {
            mGameObject.RemoveGameObject(this);
            this.objectActive = false;
            this.objectCollider.OnCollision -= OnHit;
            this.objectCollider = null;
        }

    }
}
