using System;
using System.Numerics;

namespace Game
{
    class wRedDiamond : iWeapon
    {
        // Public stuff
        public iGetWeapon Owner { get; private set; }
        WeaponTypes iWeapon.Type => WeaponTypes.RedDiamond;
        public Transform bulletTransform => new Transform(spawnPosition, new Vector2(1, 1));
        private Vector2 spawnPosition = new Vector2();

        // Ammo stuff
        private int ammo = 50;
        private readonly int ammoAdd = 50;
        public int CurrentAmmo => ammo;

        // Firerate stuff
        private bool canShoot = true;
        private float recoilTime = 0.8f;
        private float currentTime = 0;

        public wRedDiamond()
        {

        }
        public void NewOwner(iGetWeapon owner)
        {
            Owner = owner;
        }
        public void AddAmmo()
        {
            ammo += ammoAdd;
        }

        public void Fire()
        {
            if (canShoot)
            {
                new pRedDiamond(bulletTransform);
                ammo--;
                canShoot = false;
            }
        }

        public void Update(float delta, Vector2 currentPosition)
        {
            spawnPosition = currentPosition;

            if (!canShoot) currentTime += delta;

            if (currentTime >= recoilTime && !canShoot)
            {
                currentTime = 0;
                canShoot = true;
            }
        }
    }
}
