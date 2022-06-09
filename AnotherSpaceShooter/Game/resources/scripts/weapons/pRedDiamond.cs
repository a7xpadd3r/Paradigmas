using System;
using System.Numerics;

namespace Game
{
    class pRedDiamond : GameObject, iProyectile
    {
        // iProyectile basics
        public Transform transform { get; set; }
        public Animation animation { get; set; }
        public float speed { get; set; }
        public Vector2 texturesize { get; set; }

        // Iterations
        private int instigatorID = -1;

        public pRedDiamond(Transform newTransform)
        {
            this.owner = "Player";
            this.tag = "Proyectile";
            this.transform = newTransform;
            this.speed = 700;
            this.damage = 3.3f;
            this.animation = ProyectilesTextures.GetProyectileAnim(1);
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
                instigatorID = instigator.Id;
                new GenericEffect(transform.Position, new Vector2(3, 3), new Vector2(1, 1), 0, "HitBeam", Effects.GetEffectTextures(4), 0.08f, false);
                Destroy();
            }
        }
        
        public override void Update()
        {
            if (active)
            {
                float posY = transform.Position.Y;
                posY -= speed * Program.GetDeltaTime();
                transform.UpdatePosition(new Vector2(transform.Position.X, posY)); // Set new position
                this.collider.UpdateColliderPosition(transform.Position);

                animation.Update();
                Render();

                if (transform.Position.Y < 0 || transform.Position.Y > 1100) Destroy();
            }
        }

        public override void Render()
        {
            Engine.DrawTransform(animation.CurrentTexture, transform);
        }

        public override void Destroy()
        {
            if (instigatorID != -1) // No iteration if no hit.
            {
                new pRedDiamondBall(new Transform(new Vector2(transform.Position.X, transform.Position.Y), transform.Scale), "left", instigatorID);
                new pRedDiamondBall(new Transform(new Vector2(transform.Position.X, transform.Position.Y), transform.Scale), "right", instigatorID);
            }
            collider.OnCollision -= OnCollision;
            AnyDamage -= Damage;
            GameObjectManager.RemoveGameObject(this);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
