using System;
using System.Numerics;

namespace Game
{
    public enum ColliderType { Box, Circle }
    public class Collider
    {
        // Debug
        public bool drawcollisions = true;
        private Texture collIndicator = OtherTextures.GetOtherTexture(0);
        private Texture collRedIndicator = OtherTextures.GetOtherTexture(2);

        // Collisions Basics
        public ColliderType type { get; private set; }
        public int Id { get; private set; }
        public bool active { get; private set; }
        public string owner { get; private set; }
        public string tag { get; private set; }
        public ColliderProperties properties { get; private set; }
        public float damage { get; private set; }

        // Events
        public event Action<Collider> OnCollision;

        public Collider(ColliderProperties newProperties, string newOwner, string newTag, int newID, float newDamage, ColliderType newType = ColliderType.Box, bool isActive = true)
        {
            this.properties = newProperties;
            this.owner = newOwner;
            this.tag = newTag;
            this.Id = newID;
            this.damage = newDamage;
            this.type = newType;
            this.active = isActive;
        }

        public void UpdateColliderProperties(ColliderProperties newProperties)
        {
            this.properties = newProperties;
        }

        public void UpdateColliderPosition(Vector2 value)
        {
            this.properties.UpdatePosition(value);
        }

        public void CheckCollision()
        {
            if (active)
            {
                if (drawcollisions) DrawCollision();

                for (int i = 0; i < CollisionManager.GetAllColliders.Count; i++)
                {
                    Collider instigator = CollisionManager.GetAllColliders[i];

                    if (instigator.active && instigator != this)
                    {
                        if (this.type == ColliderType.Box && instigator.type == ColliderType.Box)
                        {
                            Vector2 shortThisPos = this.properties.Position - this.properties.TextureCenter;
                            Vector2 shortInstigatorPos = instigator.properties.Position - instigator.properties.TextureCenter;

                            if (Collision.IsBoxColliding(shortThisPos, this.properties.Size, shortInstigatorPos, instigator.properties.Size))
                            {
                                if (this.owner != instigator.owner)
                                {
                                    // Call event
                                    this.OnCollision?.Invoke(instigator);
                                    instigator.OnCollision?.Invoke(this);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DrawCollision() // Draws a box c:
        {
            Engine.DrawTransform(collIndicator, new Transform(new Vector2(properties.Position.X, properties.Position.Y - properties.TextureCenter.Y), new Vector2(properties.Size.Y, 1)));
            Engine.DrawTransform(collRedIndicator, new Transform(new Vector2(properties.Position.X, properties.Position.Y + properties.TextureCenter.Y), new Vector2(properties.Size.Y, 1)));

            Engine.DrawTransform(collIndicator, new Transform(new Vector2(properties.Position.X - properties.TextureCenter.X, properties.Position.Y), new Vector2(1, properties.Size.Y)));
            Engine.DrawTransform(collRedIndicator, new Transform(new Vector2(properties.Position.X + properties.TextureCenter.X, properties.Position.Y), new Vector2(1, properties.Size.Y)));
        }

    }
}
