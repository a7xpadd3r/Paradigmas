using System.Numerics;

namespace Game
{
    class wEnemyBlueRail : iWeapon
    {
        // Weapon stats
        private int ammo = 0;
        private bool canShoot = true;
        private float recoilTime = 0.8f;
        private float currentTime = 0;
        public WeaponTypes ThisType => WeaponTypes.Enemy1;
        public int AddAmmoAmount => 0;

        // Public stuff
        public double Ammo => (double)ammo;
        public int CurrentAmmo => ammo;
        Vector2 iWeapon.BulletOut { get; set; }
        public iGetWeapon Owner { get; set; }
        public float AdditionalSpeed { get; set; }

        public wEnemyBlueRail() { this.recoilTime = ProyectileProperties.EnemyBlueRail.Recoil; }
        public void NewOwner(iGetWeapon owner) { Owner = owner; }
        public void AddAmmo() { }
        public void Fire(string owner, Vector2 currentPosition)
        {
            if (canShoot)
            {
                int newID = mGameObject.GenerateObjectID();
                var pEnemyBlueRail = fyPoolDay.Pool.CreateProyectile(owner, newID);
                pEnemyBlueRail.Awake(currentPosition, ThisType, GameManager.ProyectileDirection(currentPosition));
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
