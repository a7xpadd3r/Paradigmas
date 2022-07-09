using System;
using System.Numerics;

namespace Game
{
    public class wBlueRail : iWeapon
    {
        // Weapon stats
        private int ammo = 0;
        private bool canShoot = true;
        private float recoilTime = 0.4f;
        private float currentTime = 0;
        public WeaponTypes ThisType => WeaponTypes.BlueRail;
        public int AddAmmoAmount => 0;

        // Public stuff
        public int CurrentAmmo => ammo;
        Vector2 iWeapon.BulletOut { get; set; }

        public iGetWeapon Owner { get; set; }

        public wBlueRail() { this.recoilTime = ProyectileProperties.BlueRail.Recoil; }

        public void NewOwner(iGetWeapon owner)
        {
            Owner = owner;
            
        }

        public void AddAmmo() { ammo += AddAmmoAmount; }

        public void Fire(string owner, Vector2 currentPosition)
        {
            if (canShoot)
            {
                int newID = mGameObject.GenerateObjectID();
                var pBlueRail = fyPoolDay.Pool.CreateProyectile(owner, newID);
                pBlueRail.Awake(currentPosition, WeaponTypes.BlueRail);
                canShoot = false;
            }
            //if (canShoot) { new pBlueRail(owner, currentPosition); canShoot = false; }
        }

        public void Update(float delta)
        {
            if (!canShoot) currentTime += delta;
            if (currentTime >= recoilTime && !canShoot) { currentTime = 0; canShoot = true; }
        }

    }
}
