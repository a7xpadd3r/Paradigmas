using System.Numerics;

namespace Game
{
    public class wBlueRail : iWeapon
    {
        // Basic stuff
        public iGetWeapon Owner { get; private set; }
        WeaponTypes iWeapon.Type => WeaponTypes.BlueRail;
        private Vector2 spawnPosition = new Vector2();

        // Ammo stuff
        private int ammo = 0;
        public int CurrentAmmo => ammo;

        // Firerate stuff
        private bool canShoot = true;
        private float recoilTime = 0.4f;
        private float currentTime = 0;

        public wBlueRail()
        {

        }

        public void NewOwner(iGetWeapon owner)
        {
            Owner = owner;
        }
        public void AddAmmo()
        {
        }

        public void Fire()
        {
            if (canShoot)
            {
                new pBlueRail(spawnPosition);

                //new Proyectile(spawnPosition, 1, "Player");
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
