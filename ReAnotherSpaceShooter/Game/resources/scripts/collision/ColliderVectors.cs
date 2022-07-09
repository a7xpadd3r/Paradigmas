using System;
using System.Numerics;

namespace Game
{
    public class ColliderVectors
    {
        public Vector2 ownerPosition { get; private set; }
        public Vector2 ownerRealSize { get; private set; }
        public Vector2 colliderOffset { get; private set; }
        public Vector2 colliderSize { get; private set; }

        public Vector2 OwnerPosition => ownerPosition;
        public Vector2 OwnerSize => ownerRealSize;
        public Vector2 ColliderOffset => colliderOffset;
        public Vector2 ColliderSize => colliderSize;
        public Vector2 ColliderPosition => (ownerPosition - ownerRealSize / 2) - colliderOffset;

        public ColliderVectors(DoubleVector2 newOwnerVectors, DoubleVector2 newColliderVectors)
        {
            this.ownerPosition = newOwnerVectors.Position;
            this.ownerRealSize = newOwnerVectors.Scale;

            this.colliderOffset = newColliderVectors.Position;
            this.colliderSize = newColliderVectors.Scale;
        }

        public ColliderVectors(Vector2 newOwnerPos, Vector2 newOwnerSize, Vector2 newColliderOffset, Vector2 newColliderSize)
        {
            this.ownerPosition = newOwnerPos;
            this.ownerRealSize = newOwnerSize;

            this.colliderOffset = newColliderOffset;
            this.colliderSize = newColliderSize;
        }

        public void UpdateOwnerPosition(Vector2 newPos)
        {
            this.ownerPosition = newPos;

        }
    }
}
