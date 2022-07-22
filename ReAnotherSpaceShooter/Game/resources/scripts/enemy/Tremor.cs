using System;
using System.Numerics;

namespace Game
{
    public class Tremor : ShipObject
    {
        //IA
        private Type2IA ia;

        // Status
        private float currentlife = 12;
        private float maxlife = 12;
        
        // Weapon
        private iWeapon CurrentWeapon;
        private int PointsGiven = 350;
        private bool Special = false;

        // Others
        private Random random = new Random();
        public Action OnEnemyDeath;

        public Tremor(int newID, string newOwner = "Enemy")
        { this.objectOwner = newOwner; this.objectID = newID; }
        public void SetActive(bool newStatus) { this.objectActive = newStatus; }
        public void Awake(Vector2 spawnPos, bool newSpecial = false)
        {
            this.ia = null;
            this.PointsGiven = 350;
            this.Special = newSpecial;

            if (this.Special)
            {
                this.Ship = ShipsProperties.SpecialTremor; this.PointsGiven = 450;
                this.Ship.ShipAnim = new Animation("SpecialTremorAnim", 0, Textures.SpecialTremorTextures, false, 0, true);
                this.CurrentWeapon = fWeapons.CreateWeapon(WeaponTypes.Enemy2);
            }
            else
            {
                this.Ship = ShipsProperties.Tremor;
                this.Ship.ShipAnim = new Animation("TremorAnim", 0, Textures.TremorTextures, false, 0, true);
                this.CurrentWeapon = fWeapons.CreateWeapon(WeaponTypes.Enemy1);
            }

            this.Ship.ShieldAnim = new Animation("RedShield", 0.027f, Textures.RedShield, true, 1, false, false);
            this.objectTransform = new Transform(spawnPos, new Vector2(1, 1));
            this.objectCollider = new Collider(this.Ship.Damage, this.Owner, this.Tag, this.ShipCollidersVectors, this.ID);
            this.objectCollider.OnCollision += OnHit;
            this.Ship.UpdateMaxShieldCD(0.4f);
            mGameObject.AddGameObject(this);
            this.objectActive = true;
            //objectCollider.DBG = true;
            this.ia = new Type2IA(this.Ship.MaxSpeed, this.Position);
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
            //this.objectCollider.CheckForCollisions();
            float t = Program.GetDeltaTime;
            if (ia != null) ia.Update(t);

            this.Ship.ShipUpdate(t);
            this.objectCollider.UpdateOwnerPosition(this.Position);
            for (int i = 0; i < this.Ship.ShipPropellers.Count; i++) this.Ship.ShipPropellers[i].PropellerAnimation.Update();
            this.Render();

            this.objectTransform.UpdatePosition(ia.IAPosition);

            if (this.CurrentWeapon != null)
            {
                this.CurrentWeapon.Update(t);
                this.CurrentWeapon.Fire(this.Owner, this.Position - this.Ship.RailPosition);
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

                if (randreward < 35 && randreward > 0) wtype = WeaponTypes.HeatTrail;
                else if (randreward < 80 && randreward > 35) wtype = WeaponTypes.OrbWeaver;
                else if (randreward > 80) wtype = WeaponTypes.Gamma;
                GameManager.SpawnItem(this.Position, type, wtype);
            }
        }

    }
}
