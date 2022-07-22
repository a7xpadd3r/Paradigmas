using System;
using System.Numerics;

namespace Game
{
    public static class Collision
    {
        public static bool IsBoxColliding(Vector2 posA, Vector2 sizeA, Vector2 posB, Vector2 sizeB)
        {
            float distanceX = Math.Abs(posA.X - posB.X);
            float distanceY = Math.Abs(posA.Y - posB.Y);

            float sumHWidths = sizeA.X / 2 + sizeB.X / 2;
            float sumHHeights = sizeA.Y / 2 + sizeB.Y / 2;

            return distanceX <= sumHWidths && distanceY <= sumHHeights;
        }

        public static bool IsCircleColliding(Vector2 posA, float radA, Vector2 posB, float radB)
        {
            float distanceX = Math.Abs(posA.X - posB.X);
            float distanceY = Math.Abs(posA.Y - posB.Y);

            float distance = (float)Math.Sqrt(distanceX * distanceX * distanceY * distanceY);

            return distance < radA + radB;
        }

        public static bool IsBoxCollidingWithCircle(Vector2 boxPos, Vector2 boxSize, Vector2 circlePos, float circleRad)
        {
            float pX = circlePos.X;
            if (circlePos.X < boxPos.X) pX = boxPos.X;
            else if (circlePos.X > boxPos.X + boxSize.X) pX = boxPos.X + boxSize.X;

            float pY = circlePos.Y;
            if (circlePos.Y < boxPos.Y) pY = boxPos.Y;
            else if (circlePos.Y > boxPos.Y + boxSize.Y) pY = boxPos.Y + boxSize.Y;

            float distanceX = circlePos.X - pX;
            float distanceY = circlePos.Y - pY;
            float distance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

            return distance <= circleRad;
        }
    }
}
