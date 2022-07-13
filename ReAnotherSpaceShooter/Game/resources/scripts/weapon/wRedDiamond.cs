using System;
using System.Numerics;

namespace Game
{
    public class wRedDiamond : iWeapon
    {
        // Weapon stats
        private int ammo = 50;
        private bool canShoot = true;
        private float recoilTime = 0.4f;
        private float currentTime = 0;
        public WeaponTypes ThisType => WeaponTypes.RedDiamond;
        public int AddAmmoAmount => 65;

        // Public stuff
        public int CurrentAmmo => ammo;
        Vector2 iWeapon.BulletOut { get; set; }
        public iGetWeapon Owner { get; set; }
        public float AdditionalSpeed { get; set; }
        public wRedDiamond() { this.recoilTime = ProyectileProperties.RedDiamond.Recoil; }

        public void NewOwner(iGetWeapon owner)
        {
            Owner = owner;
        }

        public void AddAmmo() { ammo += AddAmmoAmount; }

        public void Fire(string owner, Vector2 currentPosition)
        {
            if (canShoot && ammo > 0) 
            {
                int newID = mGameObject.GenerateObjectID();
                var pRedDiamond = fyPoolDay.Pool.CreateProyectile(owner, newID);
                pRedDiamond.Awake(currentPosition, WeaponTypes.RedDiamond, Direction.Up, 1, true);
                ammo--; 
                canShoot = false;
            }
        }

        public void Update(float delta)
        {
            if (!canShoot) currentTime += delta;
            if (currentTime >= recoilTime && !canShoot) { currentTime = 0; canShoot = true; }
        }

    }
}