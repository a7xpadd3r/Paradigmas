using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class Player : ShipObject, iGetWeapon
    {
        // Status
        private float currentlife = 7;
        private float maxlife = 7;

        // Weapons
        private int currentWeaponIndex = -1;
        public List<iWeapon> AllWeapons { get; private set; } = new List<iWeapon>();
        public iWeapon CurrentWeapon => AllWeapons[currentWeaponIndex];

        // Delay to prevent swaping too fast
        private bool canSwapWeapon = true;
        private float currentInputCD = 0;
        private readonly float maxInputCD = 0.25f;

        // Events
        public Action OnPlayerDeath;
        public Action<double> OnAmmoUpdate;
        public Action<List<iWeapon>> OnWeaponsUpdate;
        public Action<iWeapon> OnWeaponChange;
        public Action<Texture> CurrentShipDamage;

        // Other
        private float newheatspeed = 0;

        public Player(int newID, string newOwner = "Player")
        {
            this.objectOwner = newOwner;
            this.objectID = newID;
        }
        public void Awake(Vector2 spawnPosition, ShipData newShip)
        {
            this.currentlife = this.maxlife;
            this.Ship = newShip;
            this.Ship.ShipDamage(this.currentlife, maxlife);
            this.objectTag = "Ship";
            this.objectTransform = new Transform(spawnPosition, new Vector2(1, 1), 0);
            this.objectCollider = new Collider(5, this.objectOwner, "Ship", this.ShipCollidersVectors, this.objectID);
            this.objectCollider.OnCollision += OnHit;
            this.AllWeapons.Clear();

            GetWeapon(WeaponTypes.BlueRail);    // Blue rail is always the default weapon.
            NextWeapon();
            currentWeaponIndex = 0;

            switch (this.Ship.ShipAnim.AnimationName)
            {
                case "ElCapitanAnim": newheatspeed = 0;     break;
                case "SonicShipAnim": newheatspeed = 120;   break;
                case "SkullFlowerAnim": newheatspeed = -80; break;
            }

            mGameObject.AddGameObject(this);
            this.objectActive = true;
            this.objectCollider.DBG = false;
            ResetBlinking();
            CurrentShipDamage?.Invoke(this.Ship.ShipAnim.CurrentTexture);
        }
        private void OnHit(Collider instigator)
        {
            if (instigator.ColliderOwner == "Enemy" && !this.Ship.IsShielding && BlinkingEnded)
            {
                var fx = fyPoolDay.Pool.CreateEffect(EffectsAnimations.Smoke1);
                fx.Awake(this.Position - new Vector2(45, 50), new Vector2(1.5f, 1.5f));

                if (this.currentlife > 0) this.currentlife -= instigator.Damage / this.Ship.Durability;
                if (currentlife <= 0) Sleep();
                else if (this.currentlife > 0) { this.Ship.ShieldAnim.Play(); this.Ship.UpdateShieldStatus(true); GameManager.OnScoreUpdate(-25); }
            }
            this.Ship.ShipDamage(this.currentlife, maxlife);
            CurrentShipDamage?.Invoke(this.Ship.ShipAnim.CurrentTexture);
        }
        public override void Update()
        {
            Controls();
            this.objectCollider.UpdateOwnerPosition(this.Position);
            Render();
            this.objectCollider.CheckForCollisions();
        }
        private void Render()
        {
            if (this.draw)
            {
                if (this.draw) Renderer.DrawCenter(Ship.ShipAnim.CurrentTexture, this.objectTransform);
                for (int i = 0; i < this.Ship.ShipPropellers.Count; i++)
                {
                    this.Ship.ShipPropellers[i].PropellerAnimation.Update();
                    Renderer.DrawCenter(Ship.ShipPropellers[i].PropellerAnimation.CurrentTexture, this.Position - this.Ship.ShipPropellers[i].PropellerOffset, this.Ship.ShipPropellers[i].PropellerSize);
                }
            }
            draw = false;
            Renderer.DrawCenter(this.Ship.ShieldAnim.CurrentTexture, this.Position - this.Ship.ShieldVectors.Position, this.Ship.ShieldVectors.Scale);
        }
        private void Controls()
        {
            float t = Program.GetDeltaTime;
            this.Ship.ShipUpdate(t);
            this.Invincibility(t);

            float posX = this.Position.X;
            float posY = this.Position.Y;
            if (!this.canSwapWeapon) WeaponSwapCD(t);

            // Movement controls
            if (Engine.GetKey(Keys.A) && posX > -30) posX -= this.Ship.MaxSpeed * t;
            if (Engine.GetKey(Keys.D) && posX < 1950) posX += this.Ship.MaxSpeed * t;
            if (Engine.GetKey(Keys.W) && posY > 0) posY -= (this.Ship.MaxSpeed / 1.1f) * t;
            if (Engine.GetKey(Keys.S) && posY < 1040) posY += (this.Ship.MaxSpeed / 1.1f) * t;
            if (Engine.GetKey(Keys.Q) && this.canSwapWeapon) NextWeapon();
            if (Engine.GetKey(Keys.E) && this.canSwapWeapon) PreviousWeapon();

            if (this.CurrentWeapon.ThisType != WeaponTypes.BlueRail && this.CurrentWeapon.CurrentAmmo <= 0) RemoveWeapon();
            if (Engine.GetKey(Keys.SPACE) && this.AllWeapons.Count > 0) 
            {
                this.canSwapWeapon = false;
                this.currentInputCD = 0;
                this.CurrentWeapon.Fire(this.Owner, this.Position - this.Ship.RailPosition);
                OnAmmoUpdate?.Invoke(this.CurrentWeapon.CurrentAmmo);
            }

            if (this.CurrentWeapon != null) CurrentWeapon.Update(t);
            this.objectTransform.UpdatePosition(new Vector2(posX, posY));
        }
        public void GetItem(ItemType type)
        {
            switch (type)
            {
                case ItemType.Repair: Repair(maxlife / 1.8f); break;
                case ItemType.Shield: ResetBlinking(45); break;
            }
        }
        public void GetWeapon(WeaponTypes type)
        {
            var newweap = fWeapons.CreateWeapon(WeaponTypes.BlueRail);
            switch (type)
            {
                case WeaponTypes.BlueRail:      newweap = fWeapons.CreateWeapon(WeaponTypes.BlueRail);      break;
                case WeaponTypes.RedDiamond:    newweap = fWeapons.CreateWeapon(WeaponTypes.RedDiamond);    break;
                case WeaponTypes.GreenCrast:    newweap = fWeapons.CreateWeapon(WeaponTypes.GreenCrast);    break;
                case WeaponTypes.HeatTrail:     newweap = fWeapons.CreateWeapon(WeaponTypes.HeatTrail, newheatspeed);     break;
                case WeaponTypes.OrbWeaver:     newweap = fWeapons.CreateWeapon(WeaponTypes.OrbWeaver);     break;
                case WeaponTypes.Gamma:         newweap = fWeapons.CreateWeapon(WeaponTypes.Gamma);         break;
                default: Console.WriteLine("Player -> Ítem de arma recibido, pero el tipo es desconocido.");break;
            }

            bool alreadyhas = false;
            foreach (var weapon in AllWeapons) { if (weapon.ThisType == newweap.ThisType) { weapon.AddAmmo(); alreadyhas = true; } }
            if (!alreadyhas) AllWeapons.Add(newweap);

            OnAmmoUpdate?.Invoke(this.CurrentWeapon.CurrentAmmo);
            OnWeaponsUpdate?.Invoke(AllWeapons);
            OnWeaponChange?.Invoke(CurrentWeapon);
        }
        private void RemoveWeapon() 
        {
            var cweap = CurrentWeapon;
            PreviousWeapon();
            AllWeapons.Remove(cweap);
            OnWeaponsUpdate?.Invoke(AllWeapons);
        }
        private void PreviousWeapon()
        {
            if (AllWeapons.Count > 1)
            {
                if (currentWeaponIndex != 0) currentWeaponIndex--;
                else currentWeaponIndex = AllWeapons.Count - 1;
                canSwapWeapon = false;
                OnAmmoUpdate?.Invoke(this.CurrentWeapon.CurrentAmmo);
                OnWeaponChange?.Invoke(CurrentWeapon);
                GameManager.changeweapon.controls.play();
            }
        }
        private void NextWeapon()
        {
            if (AllWeapons.Count > 1)
            {
                if (currentWeaponIndex == AllWeapons.Count -1) currentWeaponIndex = 0;
                else currentWeaponIndex++;
                canSwapWeapon = false;
                OnAmmoUpdate?.Invoke(this.CurrentWeapon.CurrentAmmo);
                OnWeaponChange?.Invoke(CurrentWeapon);
                GameManager.changeweapon.controls.play();
            }
        }
        private void WeaponSwapCD(float delta)
        {
            if (this.currentInputCD >= this.maxInputCD) { this.canSwapWeapon = true; this.currentInputCD = 0;}
            else if (this.currentInputCD <= this.maxInputCD) this.currentInputCD += delta;
        }
        public override void Sleep()
        {
            GameManager.OnScoreUpdate(-250);
            this.objectCollider.OnCollision -= OnHit;
            mGameObject.RemoveGameObject(this);
            this.objectCollider = null;
            this.OnPlayerDeath?.Invoke();
        }
        public void SetActive(bool newStatus) { this.objectActive = newStatus; }
        public void ForceUpdateUI()
        {
            OnWeaponChange?.Invoke(CurrentWeapon);
        }

        // Repair & Invulnerability
        private bool BlinkingEnded = false;
        private bool blinking = true;
        private bool draw = true;
        private float invFramesDuration = 0.08f;
        private float currentInv = 0;
        private int howManyBlinks = 15;
        private int currentBlinks = 0;

        private void Repair(float howmuch)
        {
            float repairamount = this.currentlife + howmuch;
            if (repairamount > this.maxlife) repairamount = this.maxlife;
            this.currentlife = repairamount;
            ResetBlinking();
            CurrentShipDamage?.Invoke(this.Ship.ShipAnim.CurrentTexture);
        }
        private void Invincibility(float delta)
        {
            if (this.blinking && this.currentInv < this.invFramesDuration) this.currentInv += delta;
            if (this.blinking && this.currentInv >= this.invFramesDuration && this.currentBlinks < this.howManyBlinks)
            {
                this.currentBlinks++;
                this.currentInv = 0;
                this.draw = true;
            }
            if (this.currentBlinks >= this.howManyBlinks) { this.blinking = false; this.draw = true; this.BlinkingEnded = true; this.howManyBlinks = 0; }
        }
        private void ResetBlinking(int newBlinksAmount = 5)
        {
            this.blinking = true;
            this.BlinkingEnded = false;
            this.currentInv = 0;
            this.currentBlinks = 0;
            this.howManyBlinks += newBlinksAmount;
        }
    }
}
