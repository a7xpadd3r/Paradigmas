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
        private Vector2 renderitemoffset = new Vector2();
        private string owner = "World";
        private int objectid;

        // Movement
        private float speed = 40f;

        // Events
        public Action OnSleep;

        public Item(string newOwner, int newID)
        {
            this.owner = "World";
            this.objectid = newID;
        }
        public void Awake(Vector2 spawnPosition, ItemType itemtype, WeaponTypes weaptype = WeaponTypes.BlueRail)
        {
            this.objectID = this.objectid;
            this.objectOwner = "World";
            this.objectTag = "Item";
            this.objectTransform = new Transform(spawnPosition, new Vector2(1, 1));
            this.objectCollider = new Collider(0, "World", "Item", spawnPosition, new Vector2(35, 35), this.objectID);
            this.type = itemtype;
            this.weaptypes = weaptype;
            this.objectAnimation = Textures.GetEffectAnimation(EffectsAnimations.ItemGlow);

            this.objectActive = true;
            this.objectTransform.UpdatePosition(spawnPosition);
            this.objectCollider.OnCollision += OnHit;

            mGameObject.AddGameObject(this);
            Console.WriteLine("ID: {0} - Item generado.", this.ID);
            RefreshTexture();
        }
        public override void Update()
        {
            this.objectAnimation.Update();
            this.objectCollider.UpdateOwnerPosition(this.Position - this.renderitemoffset);
            this.objectCollider.CheckForCollisions();
            Renderer.DrawCenter(this.objectAnimation.CurrentTexture, this.Position, new Vector2(3,3));
            Renderer.DrawCenter(this.itemtexture, this.Position - this.renderitemoffset);

            float posY = this.Position.Y;
            posY += speed * Program.GetDeltaTime;
            this.objectTransform.UpdatePosition(new Vector2(this.Position.X, posY));

            if (this.Position.Y > 1180) Sleep();
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
                case ItemType.Repair:   this.itemtexture = Textures.ItemsList[0]; this.renderitemoffset = new Vector2(-15.5f, -15.5f); break;
                case ItemType.Shield:   this.itemtexture = Textures.ItemsList[1]; this.renderitemoffset = new Vector2(-15.5f, -15.5f); break;
                case ItemType.Special:  this.itemtexture = Textures.ItemsList[2]; this.renderitemoffset = new Vector2(-15.5f, -15.5f); break;
                case ItemType.Weapon:
                    switch (weaptypes)
                    {
                        case WeaponTypes.BlueRail:   this.itemtexture =  Textures.ItemsList[3]; this.renderitemoffset = new Vector2(-15.5f, -15.5f); break;
                        case WeaponTypes.RedDiamond: this.itemtexture =  Textures.ItemsList[4]; this.renderitemoffset = new Vector2(-15.5f, -15.5f); break;
                        case WeaponTypes.GreenCrast: this.itemtexture =  Textures.ItemsList[5]; this.renderitemoffset = new Vector2(-15.5f, -15.5f); break;
                        case WeaponTypes.HeatTrail:  this.itemtexture =  Textures.ItemsList[6]; this.renderitemoffset = new Vector2(-15.5f, -15.5f); break;
                        case WeaponTypes.OrbWeaver:  this.itemtexture =  Textures.ItemsList[7]; this.renderitemoffset = new Vector2(-15.5f, -15.5f); break;
                        case WeaponTypes.Gamma:      this.itemtexture =  Textures.ItemsList[8]; this.renderitemoffset = new Vector2(-15.5f, -15.5f); break;
                        case WeaponTypes.Enemy1:     this.itemtexture =  Textures.ItemsList[0]; this.renderitemoffset = new Vector2(-15.5f, -15.5f); break;
                        case WeaponTypes.Enemy2:     this.itemtexture =  Textures.ItemsList[0]; this.renderitemoffset = new Vector2(-15.5f, -15.5f); break;
                        case WeaponTypes.Enemy3:     this.itemtexture =  Textures.ItemsList[0]; this.renderitemoffset = new Vector2(-15.5f, -15.5f); break;
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
