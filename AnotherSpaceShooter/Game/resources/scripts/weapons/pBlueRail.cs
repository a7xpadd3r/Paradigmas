using System;
using System.Numerics;

namespace Game
{
    class pBlueRail : GameObject, iProyectile
    {
        // iProyectile basics
        public Transform transform { get; set; }
        public Animation animation { get; set; }
        public float speed { get; set; }
        public Vector2 texturesize { get; set; }

        public pBlueRail(Transform newTransform)
        {
            this.owner = "Player";
            this.tag = "Proyectile";
            this.transform = newTransform;
            this.speed = 1000;
            this.damage = 1.8f;
            this.animation = ProyectilesTextures.GetProyectileAnim(0);
            this.texturesize = new Vector2(animation.CurrentTexture.Width, animation.CurrentTexture.Height);
            this.transform.UpdateTransform(newTransform);
            this.colliderProperties = new ColliderProperties(transform.Position, new Vector2(10, 10));

            Awake();
        }

        public override void OnCollision(Collider instigator)
        {
            if (instigator.tag != "Item" && instigator.owner != this.owner)
            {
                //new GenericEffect(transform.Position, new Vector2(3, 3), new Vector2(1, 1), 0, "HitBeam", Effects.GetEffectTextures(4), 0.08f, false);
                Destroy();
            }
        }

        public override void Update()
        {
            float posX = transform.Position.X;
            float posY = transform.Position.Y;
            posY -= speed * Program.GetDeltaTime();
            transform.UpdatePosition(new Vector2(transform.Position.X, posY)); // Set new position
            this.collider.UpdateColliderPosition(new Vector2(posX - 64, posY - 64));
            
            animation.Update();
            Render();
            if (transform.Position.Y < 0) Destroy();
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
