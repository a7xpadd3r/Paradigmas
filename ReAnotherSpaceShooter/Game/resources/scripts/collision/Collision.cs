using System;
using System.Numerics;


namespace Game
{
    // It brings functions to determine if two objects are colliding in the scene.
    public static class Collision
    {
        public static bool IsBoxColliding(Vector2 position1, Vector2 size1, Vector2 position2, Vector2 size2)
        {
            //var distance = new Vector2(Math.Abs(position1.X - position2.X), Math.Abs(position1.Y - position2.Y));

            //var sumSize = new Vector2((size1.X / 2 + size2.X / 2), (size1.Y / 2 + size2.Y / 2));

            bool checkX = false;
            if (position1.X < position2.X + size2.X &&
                size1.X + position1.X > position2.X) checkX = true;

            bool checkY = false;
            if (position1.Y < position2.Y + size2.Y &&
                size1.Y + position1.Y > position2.Y ) checkY = true;

            return checkX && checkY;
        }

        public static bool IsCircleColliding(Vector2 position1, float radio1, Vector2 position2, float radio2)
        {
            var distance = (float)Math.Sqrt((position1.X * position2.X) + (position1.Y * position2.Y));

            return distance < radio1 + radio2;
        }

        public static bool IsBoxWithCircleColliding(Vector2 boxPosition, Vector2 boxSize, Vector2 circlePosition,
            float circleRadius)
        {
            var position = circlePosition;
            if (circlePosition.X < boxPosition.X)
            {
                position.X = boxPosition.X;
            }
            else if (circlePosition.X > boxPosition.X + boxSize.X)
            {
                position.X = boxPosition.X + boxSize.X;
            }

            if (circlePosition.Y < boxPosition.Y)
            {
                position.Y = boxPosition.Y;
            }
            else if (circlePosition.Y > boxPosition.Y + boxSize.Y)
            {
                position.Y = boxPosition.Y + boxSize.Y;
            }

            var vectorDistance = circlePosition - position;

            var distance = (float)Math.Sqrt(vectorDistance.X * vectorDistance.X + vectorDistance.Y * vectorDistance.Y);

            return distance <= circleRadius;
        }
    }
}



/*
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
*/