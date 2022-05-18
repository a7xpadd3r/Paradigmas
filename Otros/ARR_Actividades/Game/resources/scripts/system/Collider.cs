using System;
using System.Numerics;

namespace Game
{
    public enum ColliderType { Box, Circle }
    public class Collider
    {
        public GameObject gObject { get; private set; }
        public event Action<Collider> OnCollision;
        public ColliderType type { get; private set; }
        public Transform transform;
        public bool active { get; private set; }

        public void UpdatePos(Transform newPos)
        {
            transform = newPos;
            //Console.WriteLine(gObject.objTransform.position);
        }

        public void SetActive(bool newStatus)
        {
            active = newStatus;
            //Console.WriteLine("{0} not active anymore", this);
        }

        public Collider(GameObject ownerGObject, Transform ownerTransform, ColliderType cType = ColliderType.Box, bool cActive = true)
        {
            this.gObject = ownerGObject;
            this.active = cActive;
            this.type = cType;
        }

        public void CheckForCollisions()
        {
            if (transform != null && active)
                for (int i = 0; i < CollisionManager.CurrentColliders.Count; i++)
                {
                    Collider currentCollider = CollisionManager.CurrentColliders[i];
                    if (currentCollider.active && currentCollider.transform != null && this != currentCollider)
                        if (type == ColliderType.Box && currentCollider.type == ColliderType.Box  )
                        {
                            if (Collision.IsBoxColliding(transform.position, transform.scale, currentCollider.transform.position, currentCollider.transform.scale))
                            {
                                OnCollision?.Invoke(currentCollider);
                            }
                        }
                }
        }
    }
}
