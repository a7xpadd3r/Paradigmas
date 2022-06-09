using System;
using System.Numerics;

namespace Game
{
    public enum ColliderProperty { Position, Scale, Offset, TextureSize }
    public class ColliderRework
    {
        // Debug
        public bool drawcollisions = true;
        private Texture collIndicator = OtherTextures.GetOtherTexture(0);

        // Collisions Basics
        public ColliderType type { get; private set; }
        public int Id { get; private set; }
        public bool active { get; private set; }
        public string owner { get; private set; }
        public string tag { get; private set; }
        public ColliderProperties properties { get; private set; }
        public float damage { get; private set; }

        // Events
        public event Action<ColliderRework> OnCollision;

        public ColliderRework(ColliderProperties newProperties, string newOwner, string newTag, int newID, float newDamage, ColliderType newType = ColliderType.Box, bool isActive = true)
        {
            this.properties = newProperties;
            this.owner = newOwner;
            this.tag = newTag;
            this.Id = newID;
            this.damage = newDamage;
            this.type = newType;
            this.active = isActive;
        }

        public void UpdateCollider(ColliderProperties newProperties)
        {
            this.properties = newProperties;
        }

        public void UpdateProperty(ColliderProperty type, Vector2 value)
        {
            switch (type)
            {
                case ColliderProperty.Position:
                    this.properties.UpdatePosition(value);
                    break;
                case ColliderProperty.Scale:
                    this.properties.UpdateSize(value);
                    break;
                case ColliderProperty.Offset:
                    this.properties.UpdateOffset(value);
                    break;
                case ColliderProperty.TextureSize:
                    this.properties.UpdateTextureSize(value);
                    break;
            }
        }

        public void CheckCollision()
        {
            if (active)
            {
                for (int i = 0; i < CollisionManager.NewColliders.Count; i++)
                {
                    ColliderRework newCollider = CollisionManager.NewColliders[i];

                    if (newCollider.active && newCollider != this)
                    {
                        if (this.type == ColliderType.Box && newCollider.type == ColliderType.Box)
                        {
                             if (Collision.IsBoxColliding(this.properties.Position, this.properties.Size, newCollider.properties.Position, newCollider.properties.Size))
                            {
                                if (this.owner != newCollider.owner)
                                {
                                    // Call event
                                    this.OnCollision?.Invoke(newCollider);
                                    //newCollider.OnCollision?.Invoke(this);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DrawCollision() // Draws a box c:
        {
            Engine.Draw(OtherTextures.GetOtherTexture(0), properties.Position.X - properties.Offset.X, properties.Position.Y - properties.Offset.Y, properties.TextureSize.X, 4);
            Engine.Draw(OtherTextures.GetOtherTexture(0), properties.Position.X - properties.Offset.X, properties.Position.Y + properties.Offset.Y, properties.TextureSize.X, 4);
            Engine.Draw(OtherTextures.GetOtherTexture(0), properties.Position.X - properties.Offset.X, properties.Position.Y - properties.Offset.Y, 4, properties.TextureSize.Y);
            Engine.Draw(OtherTextures.GetOtherTexture(0), properties.Position.X + properties.Offset.X, properties.Position.Y - properties.Offset.Y, 4, properties.TextureSize.Y);
        }

    }
}
