using System.Numerics;

namespace Game
{
    public enum Position { X, Y }
    public class Transform
    {
        private float posX = 0;
        private float posY = 0;
        private float width = 1;
        private float height = 1;
        private float rotation = 0;

        public Vector2 Position => new Vector2(posX, posY);
        public Vector2 Scale => new Vector2(width, height);
        public float Rotation => rotation;

        public Transform(Vector2 newPosition = new Vector2(), Vector2 newScale = new Vector2(), float newAngle = 0)
        {
            this.posX = newPosition.X;
            this.posY = newPosition.Y;
            this.width = newScale.X;
            this.height = newScale.Y;
            this.rotation = newAngle;
        }

        public void UpdateTransform(Transform newTransform)
        {
            this.posX = newTransform.Position.X;
            this.posY = newTransform.Position.Y;
            this.width = newTransform.Scale.X;
            this.height = newTransform.Scale.Y;
            this.rotation = newTransform.Rotation;
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            this.posX = newPosition.X;
            this.posY = newPosition.Y;
        }

        public void UpdatePosition(Position direction, float value)
        {
            switch (direction)
            {
                case Game.Position.X:
                    this.posX = value;
                    break;
                case Game.Position.Y:
                    this.posY = value;
                    break;
            }
        }

        public void UpdateScale(Vector2 newScale)
        {
            this.width = newScale.X;
            this.height = newScale.Y;
        }

        public void UpdateRotation(float newRotation)
        {
            this.rotation = newRotation;
        }

    }
}
