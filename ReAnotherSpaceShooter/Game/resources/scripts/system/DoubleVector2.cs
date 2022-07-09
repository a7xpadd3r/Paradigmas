using System.Numerics;

namespace Game
{
    public enum VectorSelection { First, Second }
    public class DoubleVector2
    {
        public Vector2 position;
        public Vector2 scale;

        public Vector2 Position => position;
        public Vector2 Scale => scale;

        public DoubleVector2(Vector2 newPosition, Vector2 newScale)
        {
            position = newPosition;
            scale = newScale;
        }

        public void UpdateVector(VectorSelection whichone, Vector2 newVector)
        {
            switch (whichone)
            {
                case VectorSelection.First:
                    position = newVector;
                    break;

                case VectorSelection.Second:
                    scale = newVector;
                    break;
            }
        }
    }
}
