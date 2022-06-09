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
        public float damage { get; set; }

        // Iterations
        private int instigatorID = -1;

        public pRedDiamond(Transform newTransform)
        {
            this.transform = newTransform;
            this.transform.UpdateTransform(newTransform); // Just in case.
            this.speed = 700;
            this.animation = ProyectilesTextures.GetProyectileAnim(1);
            this.damage = 3.3f;
            this.objectCollider = new Collider(transform.Position, transform.Scale, "Player", "Proyectile", damage);
            Awake();
        }

        public override void OnCollision(Collider instigator)
        {
            if (instigator.tag != "Item")
            {
                instigatorID = instigator.id;
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
            objectCollider.OnCollision -= OnCollision;
            AnyDamage -= Damage;
            GameObjectManager.RemoveGameObject(this);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
