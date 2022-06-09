using System;
using System.Numerics;

namespace Game
{
    class pGreenCrast : GameObject, iProyectile
    {
        // iProyectile basics
        public Transform transform { get; set; }
        public Animation animation { get; set; }
        public float speed { get; set; }
        public Vector2 texturesize { get; set; }

        private bool canSpawnEffect = true;
        private float currentEDelay = 0;
        private float effectDelay = 0.05f;

        public pGreenCrast(Transform newTransform)
        {
            this.owner = "Player";
            this.tag = "Proyectile";
            this.transform = newTransform;
            this.speed = 1600;
            this.damage = 0.6f;
            this.animation = ProyectilesTextures.GetProyectileAnim(3);
            this.texturesize = new Vector2(animation.CurrentTexture.Width, animation.CurrentTexture.Height);
            this.transform.UpdateTransform(newTransform);
            this.colliderProperties = new ColliderProperties(this.Position, texturesize);
            Awake();
        }

        public override void OnCollision(Collider instigator)
        {
            //Console.WriteLine(instigator);
            if (instigator.tag != "Item" && instigator.owner != this.owner)
            {
                new GenericEffect(transform.Position, new Vector2(3, 3), new Vector2(1, 1), 0, "HitBeam", Effects.GetEffectTextures(4), 0.08f, false);
                Destroy();
            }
        }

        public override void Update()
        {
            if (this.active)
            {
                float delta = Program.GetDeltaTime();

                float posY = transform.Position.Y;
                posY -= speed * delta;
                transform.UpdatePosition(new Vector2(transform.Position.X, posY)); // Set new position
                this.collider.UpdateColliderPosition(transform.Position);

                animation.Update();
                Render();

                if (transform.Position.Y < 0) Destroy();

                // Don't spam the hit effect
                if (!canSpawnEffect && currentEDelay > effectDelay) { canSpawnEffect = true; currentEDelay = 0; }
                if (!canSpawnEffect && currentEDelay < effectDelay) currentEDelay += delta;
            }
        }

        public override void Render()
        {
            Engine.DrawTransform(animation.CurrentTexture, transform);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
