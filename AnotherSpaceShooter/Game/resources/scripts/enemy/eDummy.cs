using System;
using System.Numerics;

namespace Game
{
    public class eDummy : ShipObject, iEnemy
    {
        // Basic stuff
        private Texture texture = OtherTextures.GetOtherTexture(1);
        private Transform render => new Transform(Position, new Vector2(1, 1), custRot);
        private Vector2 renderOffset => new Vector2(texture.Width, texture.Height);

        // AI stuff
        private bool isMoving = false;
        private Random random = new Random();
        private float currentNextMove = 0;

        private float custRot = 0;

        public eDummy(Vector2 newSpawnPosition = new Vector2(), float newRotation = 0, float newLifes = 30)
        {
            this.ShipConfiguration = ShipsData.GetShipConfig(3);
            this.ShipAnim = OtherTextures.GetOtherAnimation(0);
            this.currentLifes = newLifes;
            this.UpdatePosition(newSpawnPosition);
            this.custRot = newRotation;
            this.colliderProperties = new ColliderProperties(this.Position, renderOffset);
            Awake();
        }

        public override void Update()
        {
            //this.objectCollider.UpdatePos(new Vector2(Position.X + texture.Width, Position.Y + renderOffset.Y));
            if (ready && active)
            {
                UpdateShipPosition(render);
                collider.UpdateColliderPosition(Position);
                Render();
            }
        }
        
        /*
        public override void Render()
        {
            Engine.DrawTransform(texture, render);
        }
        */
        public override void OnCollision(Collider instigator)
        {
            
            new GenericEffect(Position, new Vector2(1.8f, 1.8f), new Vector2(160, 230), 0, "Smoke", Effects.GetEffectTextures(1), 0.12f, false, false, false);
        }

        public void AI()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}
