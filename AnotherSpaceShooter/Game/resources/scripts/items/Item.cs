using System;
using System.Numerics;

namespace Game
{
    public enum ItemType { Repair, Shield, Special, Weapon }
    public class Item : GameObject
    {
        // Basic stuff
        //private float posX = 0;
        //private float posY = 0;
        private float sizeX = 5f;
        private float sizeY = 5f;
        private float offsetX = 175f;
        private float offsetY = 125f;
        //private Vector2 Position => new Vector2(posX, posY);
        private Vector2 Size => new Vector2(sizeX, sizeY);
        private Vector2 Offset => new Vector2(offsetX, offsetY);

        // Item stuff
        private ItemType type = ItemType.Repair; // Default item
        private WeaponTypes weaptype = WeaponTypes.BlueRail; // Default weapon type
        private Animation glow = new Animation("ItemGlow", 0.1f, Effects.GetEffectTextures(6));
        private Texture itemTexture = ItemsTextures.GetItemTexture(0); // Default texture

        // Optional stuff
        private float speed = 80f;
        public new ItemType GetType => type;
        public WeaponTypes WeaponType => weaptype;

        private Transform transform => new Transform(Position, Size);

        public Item(ItemType newType, Vector2 newSpawnPosition, WeaponTypes newWeapType = WeaponTypes.BlueRail, float newSpeed = 80f)
        {
            this.owner = "Item";
            this.tag = this.owner;
            this.speed = newSpeed;
            this.UpdatePosition(newSpawnPosition);
            this.callsDamageOnCollision = false;
            this.type = newType;
            this.weaptype = newWeapType;

            switch (type)
            {
                case ItemType.Repair:
                    itemTexture = ItemsTextures.GetItemTexture(0);
                    break;

                case ItemType.Shield:
                    itemTexture = ItemsTextures.GetItemTexture(1);
                    break;

                case ItemType.Special:
                    itemTexture = ItemsTextures.GetItemTexture(2);
                    break;

                case ItemType.Weapon:
                    switch (weaptype)
                    {
                        case WeaponTypes.BlueRail:
                            itemTexture = ItemsTextures.GetItemTexture(3);
                            break;
                        case WeaponTypes.RedDiamond:
                            itemTexture = ItemsTextures.GetItemTexture(4);
                            break;
                        case WeaponTypes.GreenCrast:
                            itemTexture = ItemsTextures.GetItemTexture(5);
                            break;
                        case WeaponTypes.HeatTrail:
                            itemTexture = ItemsTextures.GetItemTexture(6);
                            break;
                        case WeaponTypes.OrbWeaver:
                            break;
                        case WeaponTypes.Gamma:
                            break;
                        case WeaponTypes.Enemy1:
                            break;
                        case WeaponTypes.Enemy2:
                            break;
                        case WeaponTypes.Enemy3:
                            break;
                        default:
                            break;
                    }
                    break;
            }

            this.colliderProperties = new ColliderProperties(this.Position, new Vector2(itemTexture.Width, itemTexture.Height));
            GameObjectManager.AddItem(this);
            Awake();
        }

        public override void OnCollision(Collider instigator)
        {
            // Nothing goes here, but the event needs to be overrided.  
        }

        public override void Update()
        {
            float posX = Position.X;
            float posY = Position.Y;
            posY += speed * Program.GetDeltaTime();
            Vector2 newPosition = new Vector2(posX, posY);

            this.UpdatePosition(newPosition);
            this.collider.UpdateColliderPosition(newPosition);
            glow.Update();

            if (posY > 1400) { GameObjectManager.RemoveItem(this); Destroy(); }
        }

        public override void Render()
        {
            Engine.DrawTransform(glow.CurrentTexture, transform);
            //Engine.Draw(glow.CurrentTexture, Position.X, Position.Y, Size.X, Size.Y, 0, Offset.X, Offset.Y);
            //Engine.Draw(itemTexture, Position.X, Position.Y, 1.2f, 1.2f, 0, Offset.X - 21, Offset.Y - 19);
        }
    }
}
