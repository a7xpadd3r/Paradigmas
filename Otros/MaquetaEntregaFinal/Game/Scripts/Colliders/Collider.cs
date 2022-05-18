using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
   public enum ColliderType
   {
        Box,
        Circle
   }
    public class Collider
    {
        public GameObject GameObject { get; private set; }
        public event Action<GameObject> OnCollision;
        public event Action<int, int> OnCollide;

        public ColliderType Type { get; private set; }
        public bool IsActive { get; private set; }

        public Collider(GameObject gameObject, ColliderType type, bool isActive)
        {
            IsActive = isActive;
            Type = type;
            GameObject = gameObject;
            ColliderManager.AddCollider(this);
        }

        public void CheckCollision()
        {
            if (IsActive)
                for (int i = 0; i < ColliderManager.Colliders.Count; i++)
                {
                    Collider collider = ColliderManager.Colliders[i];
                    if (Type == ColliderType.Box && collider.Type == ColliderType.Box)
                    {
                        if (Collision.IsBoxColliding(GameObject.Position, GameObject.Size, collider.GameObject.Position, collider.GameObject.Size))
                        {
                            OnCollision?.Invoke(collider.GameObject);
                        }
                    }
                    else if (Type == ColliderType.Circle && collider.Type == ColliderType.Circle)
                    {
                        if (Collision.IsCircleColliding(GameObject.Position, GameObject.RealWidth / 2, collider.GameObject.Position, collider.GameObject.RealWidth / 2))
                        {
                            OnCollision?.Invoke(GameObject);
                        }
                    }
                    else
                    {
                        if (Type == ColliderType.Circle)
                        {
                            if (Collision.IsBoxWithCircleColliding(collider.GameObject.Position, collider.GameObject.Size, GameObject.Position, GameObject.RealWidth / 2))
                            {
                                OnCollision?.Invoke(collider.GameObject);
                            }
                        }
                        else
                        {
                            if (Collision.IsBoxWithCircleColliding(GameObject.Position, GameObject.Size, collider.GameObject.Position, collider.GameObject.RealWidth / 2))
                            {
                                OnCollision?.Invoke(collider.GameObject);
                            }
                        }
                    }
                }
        }
    }
}

