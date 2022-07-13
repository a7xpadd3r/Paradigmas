using System.Numerics;

namespace Game
{
    public class wGreenCrast : iWeapon
    {
        // Weapon stats
        private int ammo = 150;
        private bool canShoot = true;
        private float recoilTime = 0.4f;
        private float currentTime = 0;
        public WeaponTypes ThisType => WeaponTypes.GreenCrast;
        public int AddAmmoAmount => 200;

        // Public stuff
        public int CurrentAmmo => ammo;
        Vector2 iWeapon.BulletOut { get; set; }
        public float AdditionalSpeed { get; set; }
        public iGetWeapon Owner { get; set; }

        public wGreenCrast() { this.recoilTime = ProyectileProperties.GreenCrast.Recoil; }

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
                var pGreenCrast = fyPoolDay.Pool.CreateProyectile(owner, newID);
                pGreenCrast.Awake(currentPosition, WeaponTypes.GreenCrast, Direction.Up, 5, true);
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