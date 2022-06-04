using System;
using System.Numerics;

namespace Game
{
    class wGreenCrast : iWeapon
    {
        // Basic stuff
        public iGetWeapon Owner { get; private set; }
        WeaponTypes iWeapon.Type => WeaponTypes.GreenCrast;
        private Vector2 spawnPosition = new Vector2();

        // Ammo stuff
        private int ammo = 250;
        private readonly int ammoAdd = 250;
        public int CurrentAmmo => ammo;

        // Firerate stuff
        private bool canShoot = true;
        private float recoilTime = 0.2f;
        private float currentTime = 0;

        public wGreenCrast()
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
                new Proyectile(spawnPosition, 3, "Player");
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
