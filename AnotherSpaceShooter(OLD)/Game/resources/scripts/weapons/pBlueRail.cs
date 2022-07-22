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
        public float damage { get; set; }

        public pBlueRail(Transform newTransform)
        {
            this.transform = newTransform;
            this.speed = 1000;
            this.animation = ProyectilesTextures.GetProyectileAnim(0);
            this.damage = 1.8f;
            this.transform.UpdateTransform(newTransform);
            this.objectCollider = new Collider(transform.Position, transform.Scale, "Player", "Proyectile", damage);
            Awake();
        }

        public override void OnCollision(Collider instigator)
        {
            if (instigator.tag != "Item")
            {
                new GenericEffect(transform.Position, new Vector2(3, 3), new Vector2(1, 1), 0, "HitBeam", Effects.GetEffectTextures(4), 0.08f, false);
                Destroy();
            }
        }

        public override void Update()
        {
            if (isActive)
            {
                float posY = transform.Position.Y;
                posY -= speed * Program.GetDeltaTime();
                transform.UpdatePosition(new Vector2(transform.Position.X, posY)); // Set new position

                objectCollider.UpdatePos(new Vector2(transform.Position.X + 149, transform.Position.Y + 90));
                animation.Update();
                Render();

                if (transform.Position.Y < 0) Destroy();
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
