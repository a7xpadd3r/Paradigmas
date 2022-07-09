using System;
using System.Numerics;

namespace Game
{
    public enum ColliderType { Box, Circle }
    public class Collider
    {
        public bool DBG = false;

        // Basic stuff - Vector.Position is the offset for the collider, use case: Position + Offset (owner.position + collider.position)
        private bool active = true;
        public ColliderVectors colliderVectors { get; private set; }
        public ColliderType colliderType { get; private set; }
        public string colliderOwner { get; private set; }
        public string colliderTag { get; private set; }
        public int ownerID { get; private set; }

        public float ownerDamage { get; private set; }

        // Public stuff
        public bool ColliderActive => active;
        public ColliderVectors ColliderVectors => colliderVectors;
        public ColliderType ColliderType => colliderType;
        public string ColliderOwner => colliderOwner;
        public string ColliderTag => colliderTag;
        public int ColliderID => ownerID;
        public float Damage => ownerDamage;

        // Events
        public event Action<Collider> OnCollision;

        public Collider(float newDMG, string newOwner, string newTag, ColliderVectors newVectors, int newOwnerID)
        {
            this.ownerID = newOwnerID;
            this.ownerDamage = newDMG;
            this.colliderOwner = newOwner;
            this.colliderTag = newTag;
            this.colliderVectors = newVectors;
        }
        public Collider(float newDMG, string newOwner, string newTag, Vector2 newOwnerPosition, Vector2 newOwnerSize, Vector2 newColliderOffset, Vector2 newColliderSize, int newOwnerID)
        {
            this.ownerID = newOwnerID;
            this.ownerDamage = newDMG;
            this.colliderOwner = newOwner;
            this.colliderTag = newTag;
            this.colliderVectors = new ColliderVectors(new DoubleVector2(newOwnerPosition, newOwnerSize), new DoubleVector2(newColliderOffset, newColliderSize));
        }
        public Collider(float newDMG, string newOwner, string newTag, Vector2 newOwnerPosition, Vector2 newOwnerSize, Vector2 newColliderOffset, int newOwnerID)
        {
            this.ownerID = newOwnerID;
            this.ownerDamage = newDMG;
            this.colliderOwner = newOwner;
            this.colliderTag = newTag;
            this.colliderVectors = new ColliderVectors(new DoubleVector2(newOwnerPosition, newOwnerSize), new DoubleVector2(newColliderOffset, newOwnerSize));
        }
        public Collider(float newDMG, string newOwner, string newTag, Vector2 newOwnerPosition, Vector2 newOwnerSize, int newOwnerID)
        {
            this.ownerID = newOwnerID;
            this.ownerDamage = newDMG;
            this.colliderOwner = newOwner;
            this.colliderTag = newTag;
            this.colliderVectors = new ColliderVectors(new DoubleVector2(newOwnerPosition, newOwnerSize), new DoubleVector2(new Vector2(0,0), newOwnerSize));
        }
        public Collider(float newDMG, string newOwner, string newTag, Vector2 newOwnerPosition, Vector2 newOwnerSize, DoubleVector2 colliderVectors, int newOwnerID)
        {
            this.ownerID = newOwnerID;
            this.ownerDamage = newDMG;
            this.colliderOwner = newOwner;
            this.colliderTag = newTag;
            this.colliderVectors = new ColliderVectors(new DoubleVector2(newOwnerPosition, newOwnerSize), colliderVectors);
        }

        public void UpdateOwnerPosition(Vector2 newPosition) { this.colliderVectors.UpdateOwnerPosition(newPosition); }
        public void UpdateDamage(float howMuch) { this.ownerDamage = howMuch; }

        public void CheckForCollisions()
        {
            if (DBG) Renderer.DrawCenter(Textures.GetDebugTexture(DebugTexture.GreenDot), this.ColliderVectors.ColliderPosition, this.ColliderVectors.ColliderSize);

            for (int i = 0; i < mCollisions.GetAllColliders.Count; i++)
            {
                Collider currentCollider = mCollisions.GetAllColliders[i];
                if (this == currentCollider && this.ColliderOwner != currentCollider.ColliderOwner) return;
                    if (ColliderType == ColliderType.Box && currentCollider.ColliderType == ColliderType.Box && this.ColliderOwner != currentCollider.ColliderOwner)
                    {
                        if (Collision.IsBoxColliding(this.colliderVectors.ColliderPosition, this.ColliderVectors.ColliderSize, currentCollider.ColliderVectors.ColliderPosition, currentCollider.ColliderVectors.ColliderSize))
                        {
                            OnCollision?.Invoke(currentCollider);
                            currentCollider.OnCollision?.Invoke(this);
                            if (DBG) Renderer.DrawCenter(Textures.GetDebugTexture(DebugTexture.RedDot), this.ColliderVectors.ColliderPosition, this.ColliderVectors.ColliderSize);
                        }
                    }
            }
        }
    }

}
