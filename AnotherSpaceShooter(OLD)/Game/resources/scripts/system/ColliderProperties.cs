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

        private float collideroffsetX = 0;
        private float collideroffsetY = 0;
        public Vector2 Offset => new Vector2(collideroffsetX, collideroffsetY);
        
        private float textureSizeX = 0;
        private float textureSizeY = 0;
        public Vector2 TextureSize => new Vector2(textureSizeX, textureSizeY);

        public ColliderProperties(Vector2 newPosition, Vector2 newSize, Vector2 newOffset, Vector2 newTextureSize)
        {
            this.positionX = newPosition.X;
            this.positionY = newPosition.Y;
            this.collidersizeX = newSize.X;
            this.collidersizeY = newSize.Y;
            this.collideroffsetX = newOffset.X;
            this.collideroffsetY = newOffset.Y;
            this.textureSizeX = newTextureSize.X;
            this.textureSizeY = newTextureSize.Y;
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            this.positionX = newPosition.X;
            this.positionY = newPosition.Y;
        }

        public void UpdateSize(Vector2 newSize)
        {
            this.collidersizeX = newSize.X;
            this.collidersizeY = newSize.Y;
        }

        public void UpdateOffset(Vector2 newOffset)
        {
            this.collideroffsetX = newOffset.X;
            this.collideroffsetY = newOffset.Y;
        }

        public void UpdateTextureSize(Vector2 textureSize)
        {
            this.textureSizeX = textureSize.X;
            this.textureSizeY = textureSize.Y;
        }
    }
}
