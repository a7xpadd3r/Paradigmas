﻿using System;
using System.Numerics;

namespace Game
{
    public enum ColliderType { Box, Circle }
    public class Collider
    {
        public int id { get; set; }
        public bool active { get; private set; }
        public string owner { get; private set; }
        public string tag { get; private set; }
        public ColliderType type { get; private set; }
        public Vector2 position { get; private set; }
        public Vector2 size { get; private set; }
        public Vector2 realSize;
        public float damage { get; private set; }
        private Texture collIndicator = OtherTextures.GetOtherTexture(0);

        private bool ready = false;
        public event Action<Collider> OnCollision;
        public Collider(Vector2 newPos, Vector2 newSize, string newOwner, string newTag, float newDamage, ColliderType cType = ColliderType.Box, bool cActive = true)
        {
            this.owner = newOwner;
            this.tag = newTag;
            this.active = cActive;
            this.position = newPos;
            this.size = newSize;
            this.type = cType;
            this.damage = newDamage;
            this.ready = true;
        }

        public void UpdatePos(Vector2 newPos)
        {
            position = newPos;
            //Console.WriteLine(realSize);
        }

        public void CheckForCollisions()
        {
            if (ready && active)
            {
                for (int i = 0; i < CollisionManager.GetAllColliders.Count; i++)
                {
                    Collider currentCollider = CollisionManager.GetAllColliders[i];

                    if (currentCollider.active && this != currentCollider)
                        if (type == ColliderType.Box && currentCollider.type == ColliderType.Box)
                        {
                            if (Collision.IsBoxColliding(position, size, currentCollider.position, currentCollider.size))
                            {
                                if (this.GetOwner() != currentCollider.GetOwner()) // Only if not the same owner
                                {
                                    OnCollision?.Invoke(currentCollider);
                                    currentCollider.OnCollision?.Invoke(this);
                                }
                            }
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
