using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class Slider : ShipObject
    {
        // IA
        private Type2IA ia;

        // Status
        private float currentlife = 5;
        private float maxlife = 5;

        // Weapon
        private iWeapon CurrentWeapon;
        private int PointsGiven = 220;
        private bool Special = false;

        // Others
        private Random random = new Random();
        public Action OnEnemyDeath;

        public Slider(int newID, string newOwner = "Enemy") 
        { this.objectOwner = newOwner; this.objectID = newID; this.objectTag = "Ship"; }
        public void SetActive(bool newStatus) { this.objectActive = newStatus; }
        public void Awake(Vector2 spawnPos, bool newSpecial = false)
        {
            this.PointsGiven = 220;
            this.Special = newSpecial;
            if (this.Special) 
            { 
                this.Ship = ShipsProperties.SpecialSlider; this.PointsGiven = 350;
                this.Ship.ShipAnim = new Animation("SliderAnim", 0, Textures.SpecialSliderTextures, false, 0, true);
                this.CurrentWeapon = fWeapons.CreateWeapon(WeaponTypes.Enemy3);
            }
            else
            { 
                this.Ship = ShipsProperties.Slider;
                this.Ship.ShipAnim = new Animation("SliderAnim", 0, Textures.SliderTextures, false, 0, true);
                this.CurrentWeapon = fWeapons.CreateWeapon(WeaponTypes.Enemy1);
            }

            this.Ship.ShieldAnim = new Animation("RedShield", 0.027f, Textures.RedShield, true, 1, false, false);
            this.objectTransform = new Transform(spawnPos, new Vector2(1, 1));
            this.objectCollider = new Collider(this.Ship.Damage, this.Owner, this.Tag, this.ShipCollidersVectors, this.ID);
            this.objectCollider.OnCollision += OnHit;
            this.Ship.UpdateMaxShieldCD(0.4f);

            mGameObject.AddGameObject(this);
            this.objectActive = true;
            this.ia = new Type2IA(this.Ship.MaxSpeed, this.Position);
            //objectCollider.DBG = true;
        }
        private void OnHit(Collider instigator)
        {
            if (instigator.ColliderOwner == "Player" && !this.Ship.IsShielding)
            {
                var fx = fyPoolDay.Pool.CreateEffect(EffectsAnimations.Smoke1);
                fx.Awake(this.Position - new Vector2(45, 50), new Vector2(1.5f, 1.5f));

                if (this.currentlife > 0) this.currentlife -= instigator.Damage / this.Ship.Durability;
                if (currentlife <= 0) Sleep();
                else if (this.currentlife > 0) { this.Ship.ShieldAnim.Play(); this.Ship.UpdateShieldStatus(true); GameManager.OnScoreUpdate(50); }
            }
            this.Ship.ShipDamage(this.currentlife, maxlife);
        }
        public override void Update()
        {
            //objectCollider.CheckForCollisions();
            float t = Program.GetDeltaTime;
            if (ia != null) ia.Update(t);

            this.Ship.ShipUpdate(t);
            for (int i = 0; i < this.Ship.ShipPropellers.Count; i++) this.Ship.ShipPropellers[i].PropellerAnimation.Update();
            this.Render();

            if (ia != null) this.objectTransform.UpdatePosition(ia.IAPosition);
            this.objectCollider.UpdateOwnerPosition(this.Position);

            if (this.CurrentWeapon != null)
            {
                this.CurrentWeapon.Update(t);
                if(ia.CanShoot) this.CurrentWeapon.Fire(this.Owner, this.Position - this.Ship.RailPosition);
            }
        }
        private void Render() 
        { 
            for (int i = 0; i < this.Ship.ShipPropellers.Count; i++)
                Renderer.DrawCenter(this.Ship.ShipPropellers[i].PropellerAnimation.CurrentTexture, this.Position - this.Ship.ShipPropellers[i].PropellerOffset, this.Ship.ShipPropellers[i].PropellerSize, 180);
            Renderer.DrawCenter(this.Ship.ShipAnim.CurrentTexture, this.objectTransform);
            Renderer.DrawCenter(this.Ship.ShieldAnim.CurrentTexture, this.Position - this.Ship.ShieldVectors.Position, this.Ship.ShieldVectors.Scale);
        }
        public override void Sleep()
        {
            this.objectCollider.OnCollision -= OnHit;
            mGameObject.RemoveGameObject(this);
            this.objectCollider = null;
            this.OnEnemyDeath?.Invoke();
            GameManager.OnScoreUpdate?.Invoke(this.PointsGiven);

            if (this.Special)
            {
                int randreward = random.Next(0, 100);
                ItemType type = ItemType.Weapon;
                WeaponTypes wtype = WeaponTypes.BlueRail;

                if (randreward < 35 && randreward > 0) wtype = WeaponTypes.RedDiamond;
                else if (randreward < 80 && randreward > 35) wtype = WeaponTypes.GreenCrast;
                else if (randreward > 80) wtype = WeaponTypes.HeatTrail;
                GameManager.SpawnItem(this.Position, type, wtype);
            }
        }
    }
}
