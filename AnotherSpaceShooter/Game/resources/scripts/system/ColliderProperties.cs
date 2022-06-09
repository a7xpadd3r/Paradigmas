using System.Numerics;

namespace Game
{
    public class ColliderProperties
    {
        private float positionX = 0;
        private float positionY = 0;
        public Vector2 Position => new Vector2(positionX, positionY);

        private float collidersizeX = 0;
        private float collidersizeY = 0;
        public Vector2 Size => new Vector2(collidersizeX, collidersizeY);
        
        private float textureCenterX = 0;
        private float textureCenterY = 0;
        public Vector2 TextureCenter => new Vector2(textureCenterX, textureCenterY);

        public ColliderProperties(Vector2 newPosition, Vector2 fullTextureSize, bool bypassColliderSize = false, Vector2 bypassedSize = new Vector2())
        {
            this.positionX = newPosition.X;
            this.positionY = newPosition.Y;
            this.textureCenterX = fullTextureSize.X / 2;
            this.textureCenterY = fullTextureSize.Y / 2;

            if (bypassColliderSize)
            {
                this.collidersizeX = bypassedSize.X;
                this.collidersizeY = bypassedSize.Y;
            }

            else
            {
                this.collidersizeX = fullTextureSize.X;
                this.collidersizeY = fullTextureSize.Y;
            }
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            this.positionX = newPosition.X;
            this.positionY = newPosition.Y;
        }
    }
}
