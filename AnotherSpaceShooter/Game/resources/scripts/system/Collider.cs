using System;
using System.Numerics;

namespace Game
{
    public enum ColliderType { Box, Circle }
    public class Collider
    {
        public bool active { get; private set; }
        public string owner { get; private set; }
        public ColliderType type { get; private set; }
        public Vector2 position { get; private set; }
        public Vector2 size { get; private set; }
        private bool ready = false;

        public event Action<Collider> OnCollision;
        public Collider(Vector2 newPos, Vector2 newSize, string newOwner, ColliderType cType = ColliderType.Box, bool cActive = true)
        {
            this.owner = newOwner;
            this.active = cActive;
            this.position = newPos;
            this.size = newSize;
            this.type = cType;
            this.ready = true;
        }

        public void UpdatePos(Vector2 newPos)
        {
            position = newPos;
        }

        public void CheckForCollisions()
        {
            if (ready && active)
                for (int i = 0; i < CollisionManager.GetAllColliders.Count; i++)
                {
                    Collider currentCollider = CollisionManager.GetAllColliders[i];
                    if (currentCollider.active && this != currentCollider)
                        if (type == ColliderType.Box && currentCollider.type == ColliderType.Box  )
                        {
                            if (Collision.IsBoxColliding(position, size, currentCollider.position, currentCollider.size))
                            {
                                OnCollision?.Invoke(currentCollider);
                            }
                        }
                }
        }

        public void SetActive(bool newStatus)
        {
            active = newStatus;
            //Console.WriteLine("{0} not active anymore", this);
        }

        public string GetOwner()
        {
            return owner;
        }
    }
}
