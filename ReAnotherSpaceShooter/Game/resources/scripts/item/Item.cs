using System;
using System.Numerics;

namespace Game
{
    public enum ItemType { Repair, Shield, Special, Weapon }
    public class Item : GameObject
    {
        // Basic stuff
        private ItemType type = ItemType.Repair; // Default item
        private WeaponTypes weaptypes = WeaponTypes.BlueRail; // Default weapon item
        private Texture itemtexture = Textures.ItemsList[0];
        private string owner = "World";
        private int objectid;

        // Movement
        private float speed = 40f;
        public new ItemType GetType => type;
        public WeaponTypes WeaponType => weaptypes;

        public Action OnSleep;

        /*
        public Item(Vector2 spawnPosition, ItemType newType, WeaponTypes newWeapType, float newSpeed = 80f)
        {
            this.objectID = mGameObject.GenerateObjectID();
            this.objectOwner = "Player";
            this.objectTag = "Item";
            this.objectTransform = new Transform(spawnPosition, new Vector2(1, 1));
            this.objectCollider = new Collider(0, "Player", "Item", spawnPosition, new Vector2(25, 25), this.objectID);
            this.type = newType;
            this.weaptypes = newWeapType;

            this.objectAnimation = Textures.GetEffectAnimation(EffectsAnimations.ItemGlow);
            mGameObject.AddGameObject(this);
            Console.WriteLine("ID: {0} - Item generado.", this.ID);

            this.objectActive = true;
            this.objectCollider.DBG = true;
        }
        public Item(ItemType newType, WeaponTypes newWeapType = WeaponTypes.BlueRail)
        {
            this.objectID = mGameObject.GenerateObjectID();
            this.objectOwner = "Player";
            this.objectTag = "Item";
            this.objectTransform = new Transform(new Vector2(0,0), new Vector2(0, 0));
            this.objectCollider = new Collider(0, "Player", "Item", new Vector2(0, 0), new Vector2(0, 0), this.objectID);
            this.type = newType;
            this.weaptypes = newWeapType;
            //mCollisions.AddCollider(objectCollider);

            this.objectAnimation = Textures.GetEffectAnimation(EffectsAnimations.ItemGlow);
            mGameObject.AddGameObject(this);
            Console.WriteLine("ID: {0} - Item generado.", this.ID);

            this.objectActive = true;
            this.objectCollider.DBG = true;
        }
        */

        public Item(string newOwner, int newID)
        {
            this.owner = "World";
            this.objectid = newID;
        }

        public void Awake(Vector2 spawnPosition, ItemType itemtype, WeaponTypes weaptype)
        {
            this.objectID = this.objectid;
            this.objectOwner = "World";
            this.objectTag = "Item";
            this.objectTransform = new Transform(spawnPosition, new Vector2(1, 1));
            this.objectCollider = new Collider(0, "World", "Item", spawnPosition, new Vector2(25, 25), this.objectID);
            this.type = itemtype;
            this.weaptypes = weaptype;
            this.objectAnimation = Textures.GetEffectAnimation(EffectsAnimations.ItemGlow);

            this.objectActive = true;
            this.objectCollider.DBG = true;
            this.objectTransform.UpdatePosition(spawnPosition);
            this.objectCollider.OnCollision += OnHit;

            mGameObject.AddGameObject(this);
            Console.WriteLine("ID: {0} - Item generado.", this.ID);
            RefreshTexture();
        }

        public override void Update()
        {
            this.objectAnimation.Update();
            this.objectCollider.UpdateOwnerPosition(this.Position);
            this.objectCollider.CheckForCollisions();
            Renderer.DrawCenter(this.objectAnimation.CurrentTexture, this.Position, new Vector2(3,3));
            Renderer.DrawCenter(this.itemtexture, this.Position);

            float posY = this.Position.Y;
            posY += speed * Program.GetDeltaTime;
            this.objectTransform.UpdatePosition(new Vector2(this.Position.X, posY));
        }

        public override void Sleep()
        {
            this.objectCollider.OnCollision -= OnHit;
            mGameObject.RemoveGameObject(this);
            this.objectCollider = null;
            OnSleep?.Invoke();
        }

        public void SetActive(bool newStatus) { this.objectActive = newStatus; }

        private void RefreshTexture()
        {
            switch (this.type)
            {
                case ItemType.Repair:   itemtexture = Textures.ItemsList[0]; break;
                case ItemType.Shield:   itemtexture = Textures.ItemsList[1]; break;
                case ItemType.Special:  itemtexture = Textures.ItemsList[2]; break;
                case ItemType.Weapon:
                    switch (weaptypes)
                    {
                        case WeaponTypes.BlueRail: itemtexture =        Textures.ItemsList[3];  break;
                        case WeaponTypes.RedDiamond: itemtexture =      Textures.ItemsList[4];  break;
                        case WeaponTypes.GreenCrast: itemtexture =      Textures.ItemsList[5];  break;
                        case WeaponTypes.HeatTrail: itemtexture =       Textures.ItemsList[6];  break;
                        case WeaponTypes.OrbWeaver: itemtexture =       Textures.ItemsList[7];  break;
                        case WeaponTypes.Gamma: itemtexture =           Textures.ItemsList[8];  break;
                        case WeaponTypes.Enemy1: itemtexture =          Textures.ItemsList[0]; break;
                        case WeaponTypes.Enemy2: itemtexture =          Textures.ItemsList[0]; break;
                        case WeaponTypes.Enemy3: itemtexture =          Textures.ItemsList[0]; break;
                    }   break;
            }
        }

        private void OnHit(Collider instigator)
        {
            if (instigator.ColliderOwner == "Player" && instigator.ColliderTag == "Ship")
            {
                GameManager.GiveItem(this.type, this.weaptypes);
                Sleep();
            }
        }
    }
}
