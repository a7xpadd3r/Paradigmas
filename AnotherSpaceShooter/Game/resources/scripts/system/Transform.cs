using System.Numerics;

namespace Game
{
    public class Transform
    {
        public Vector2 position { get; set; }
        public Vector2 scale { get; set; }
        public float angle { get; set; }

        public Transform(Vector2 newPosition, Vector2 newScale = new Vector2(), float newAngle = 0)
        {
            position = newPosition;
            scale = newScale;
            angle = newAngle;
        }
    }
}
