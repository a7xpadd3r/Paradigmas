using System.Numerics;

namespace Game
{
    public class TripleVector2
    {
        public Vector2 FirstVector;
        public Vector2 SecondVector;
        public Vector2 ThirdVector;

        public TripleVector2(Vector2 newFirstVector, Vector2 newSecondVector, Vector2 newThirdVector)
        {
            FirstVector = newFirstVector;
            SecondVector = newSecondVector;
            ThirdVector = newThirdVector;
        }
    }
}
