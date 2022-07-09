using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class Player : ShipObject, iGetWeapon
    {
        // Weapons
        private int currentWeaponIndex = -1;
        public List<iWeapon> AllWeapons { get; private set; } = new List<iWeapon>();
        public iWeapon CurrentWeapon => AllWeapons[currentWeaponIndex];

        // Delay to prevent swaping too fast
        private bool            canSwapWeapon = true;
        private float           currentInputCD = 0;
        private readonly float  maxInputCD = 0.25f;

        // Events
        public Action OnPlayerDeath;

        public Player(Vector2 spawnPosition, ShipData newShip)
        {
            this.Ship = newShip;
            this.objectID = mGameObject.GenerateObjectID();
            this.objectOwner = "Player";
            this.objectTag = "Ship";
            this.objectTransform = new Transform(spawnPosition, new Vector2(1, 1), 0);
            this.objectCollider = new Collider(5, this.objectOwner, "Ship", ShipCollidersVectors, this.objectID);
            this.objectCollider.OnCollision += OnHit;

            mGameObject.AddGameObject(this);

            
            int newitemid = mGameObject.GenerateObjectID();
            GetWeapon(WeaponTypes.BlueRail);    // Blue rail is always the default weapon.
            NextWeapon();
            GetWeapon(WeaponTypes.RedDiamond);
            GetWeapon(WeaponTypes.GreenCrast);
            GetWeapon(WeaponTypes.HeatTrail);
            GetWeapon(WeaponTypes.OrbWeaver);
            GetWeapon(WeaponTypes.Gamma);
            currentWeaponIndex = 0;

            this.objectActive = true;
            this.objectCollider.DBG = true;
        }

        private void OnHit(Collider instigator)
        {
            //Console.WriteLine("Player hit????");
        }

        public override void Update()
        {
            this.Ship.ShipAnim.Update();
            this.Ship.RedPropeller.Update();
            this.Ship.ShieldAnim.Update();
            this.objectCollider.UpdateOwnerPosition(this.Position);

            Renderer.DrawCenter(Ship.ShipAnim.CurrentTexture, this.objectTransform);
            Renderer.DrawCenter(Ship.RedPropeller.CurrentTexture, this.Position - Ship.FirstPropellerPosition);
            Renderer.DrawCenter(Ship.ShieldAnim.CurrentTexture, this.Position - this.Ship.ShieldVectors.Position, this.Ship.ShieldVectors.Scale);
            this.objectCollider.CheckForCollisions();

            Controls();
        }

        private void Controls()
        {
            float t = Program.GetDeltaTime;
            float posX = this.Position.X;
            float posY = this.Position.Y;
            if (!this.canSwapWeapon) WeaponSwapCD(t);

            // Movement controls
            if (Engine.GetKey(Keys.A) && posX > -30) posX -= this.Ship.MaxSpeed * t;
            if (Engine.GetKey(Keys.D) && posX < 1950) posX += this.Ship.MaxSpeed * t;
            if (Engine.GetKey(Keys.W) && posY > 0) posY -= (this.Ship.MaxSpeed / 1.1f) * t;
            if (Engine.GetKey(Keys.S) && posY < 1040) posY += (this.Ship.MaxSpeed / 1.1f) * t;

            if (Engine.GetKey(Keys.Q) && this.canSwapWeapon) PreviousWeapon();
            if (Engine.GetKey(Keys.E) && this.canSwapWeapon) NextWeapon();

            if (this.CurrentWeapon.ThisType != WeaponTypes.BlueRail && this.CurrentWeapon.CurrentAmmo <= 0) RemoveWeapon();
            if (Engine.GetKey(Keys.SPACE) && this.AllWeapons.Count > 0) 
            {
                this.canSwapWeapon = false;
                this.currentInputCD = 0;
                this.CurrentWeapon.Fire(this.Owner, this.Position - this.Ship.RailPosition);
            }

            if (this.CurrentWeapon != null) CurrentWeapon.Update(t);
            this.objectTransform.UpdatePosition(new Vector2(posX, posY));
        }

        public void GetWeapon(WeaponTypes type)
        {
            var newweap = fWeapons.CreateWeapon(WeaponTypes.BlueRail);
            switch (type)
            {
                case WeaponTypes.BlueRail:      newweap = fWeapons.CreateWeapon(WeaponTypes.BlueRail);      break;
                case WeaponTypes.RedDiamond:    newweap = fWeapons.CreateWeapon(WeaponTypes.RedDiamond);    break;
                case WeaponTypes.GreenCrast:    newweap = fWeapons.CreateWeapon(WeaponTypes.GreenCrast);    break;
                case WeaponTypes.HeatTrail:     newweap = fWeapons.CreateWeapon(WeaponTypes.HeatTrail);     break;
                case WeaponTypes.OrbWeaver:     newweap = fWeapons.CreateWeapon(WeaponTypes.OrbWeaver);     break;
                case WeaponTypes.Gamma:         newweap = fWeapons.CreateWeapon(WeaponTypes.Gamma);         break;
                default: Console.WriteLine("Player -> Ítem de arma recibido, pero el tipo es desconocido.");break;
            }

            bool alreadyhas = false;
            foreach (var weapon in AllWeapons)
            {
                if (weapon.ThisType == newweap.ThisType)
                {
                    weapon.AddAmmo();
                    alreadyhas = true;
                    Console.WriteLine("Ammo added {0}", weapon.CurrentAmmo);
                }
            }

            if (!alreadyhas) AllWeapons.Add(newweap);
        }

        private void RemoveWeapon() 
        {
            var cweap = CurrentWeapon;
            PreviousWeapon();
            AllWeapons.Remove(cweap);
        }

        private void PreviousWeapon()
        {
            if (AllWeapons.Count > 1)
            {
                if (currentWeaponIndex != 0) currentWeaponIndex--;
                else currentWeaponIndex = AllWeapons.Count - 1;
                canSwapWeapon = false;
            }
        }

        private void NextWeapon()
        {
            if (AllWeapons.Count > 1)
            {
                if (currentWeaponIndex == AllWeapons.Count -1) currentWeaponIndex = 0;
                else currentWeaponIndex++;
                canSwapWeapon = false;
            }
        }

        private void WeaponSwapCD(float delta)
        {
            if (this.currentInputCD >= this.maxInputCD) { this.canSwapWeapon = true; this.currentInputCD = 0;}
            else if (this.currentInputCD <= this.maxInputCD) this.currentInputCD += delta;
        }
    }
}
