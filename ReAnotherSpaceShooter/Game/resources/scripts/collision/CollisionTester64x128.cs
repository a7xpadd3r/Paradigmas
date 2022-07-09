using System;
using System.Numerics;

namespace Game
{
    public class CollisionTester64x128 : GameObject
    {
        public CollisionTester64x128(Vector2 spawnPosition)
        {
            this.objectID = mGameObject.GenerateObjectID();
            this.objectOwner = "Enemy";
            this.objectTag = "Box64x128";
            this.objectTransform = new Transform(spawnPosition, new Vector2(1, 1));
            this.objectAnimation = Textures.GetDebugAnimation(DebugAnimation.Box64x128);
            this.objectCollider = new Collider(0, this.objectOwner, "Enemy", spawnPosition, this.objectAnimation.TextureSize, this.objectID);
            this.objectCollider.OnCollision += OnCollision;

            mGameObject.AddGameObject(this);
            this.objectActive = true;
        }

        public override void Update()
        {
            this.objectAnimation.Update();
            this.objectCollider.UpdateOwnerPosition(objectTransform.Position);

            Renderer.DrawCenter(this.objectAnimation.CurrentTexture, this.objectTransform);
            this.objectCollider.CheckForCollisions();

            float posX = this.objectTransform.Position.X;
            float posY = this.objectTransform.Position.Y;
            posX += Program.GetDeltaTime;

            this.objectTransform.UpdatePosition(new Vector2(posX, posY));
        }

        private void OnCollision(Collider instigator)
        {
            //Console.WriteLine("Collision detected");
        }
    }
}
