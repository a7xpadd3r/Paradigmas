using System;

namespace Game
{
    public abstract class GameObject
    {
        public Collider objectCollider = null;
        public bool isActive = true;
        public string Owner => owner;
        public string owner = "null";

        public void Awake()
        {
            GameObjectManager.AddGameObject(this);
            if (objectCollider != null) CollisionManager.AddCollider(objectCollider);
        }

        public void BeginPlay()
        {
        }

        public virtual void Render()
        {
        }

        public virtual void Update()
        {
        }

        private void UpdateOwner(string newOwner)
        {
            owner = newOwner;
        }
    }
}
