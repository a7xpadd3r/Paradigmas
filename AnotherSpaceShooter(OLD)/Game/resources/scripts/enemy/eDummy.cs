using System;
using System.Numerics;

namespace Game
{
    public class eDummy : GameObject, iEnemy
    {
        // Basic stuff
        private Texture texture = OtherTextures.GetOtherTexture(1);
        private Transform render => new Transform(Position, new Vector2(1, 1), custRot);
        private Vector2 renderOffset => new Vector2(texture.Width / 2, texture.Height / 2);

        // AI stuff
        private bool isMoving = false;
        private Random random = new Random();
        private float currentNextMove = 0;

        private float custRot = 0;

        public eDummy(Vector2 newSpawnPosition = new Vector2(), float newRotation = 0, float newLifes = 30)
        {
            this.life = newLifes;
            this.spawnPosition = newSpawnPosition;
            this.realSize = new Vector2(texture.Width, texture.Height);
            this.posX = this.spawnPosition.X;
            this.posY = this.spawnPosition.Y;
            this.custRot = newRotation;
            this.objectCollider = new Collider(this.spawnPosition, this.realSize, "Enemy", "Ship", 1);
            Awake();
        }

        public override void Update()
        {
            this.objectCollider.UpdatePos(new Vector2(Position.X + texture.Width, Position.Y + renderOffset.Y));
            Render();
        }

        public override void Render()
        {
            Engine.DrawTransform(texture, render, renderOffset);
            Engine.Draw(OtherTextures.GetOtherTexture(0), Position.X - renderOffset.X, Position.Y - renderOffset.Y, texture.Width, 4);
            Engine.Draw(OtherTextures.GetOtherTexture(0), Position.X - renderOffset.X, Position.Y + renderOffset.Y, texture.Width, 4);
            Engine.Draw(OtherTextures.GetOtherTexture(0), Position.X - renderOffset.X, Position.Y - renderOffset.Y, 4, texture.Height);
            Engine.Draw(OtherTextures.GetOtherTexture(0), Position.X + renderOffset.X, Position.Y - renderOffset.Y, 4, texture.Height);

        }

        public override void OnCollision(Collider instigator)
        {
            
            new GenericEffect(Position + renderOffset, new Vector2(1.8f, 1.8f), new Vector2(160, 230), 0, "Smoke", Effects.GetEffectTextures(1), 0.12f, false, false, false);
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
