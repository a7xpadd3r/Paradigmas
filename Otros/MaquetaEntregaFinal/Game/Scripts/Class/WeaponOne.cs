using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /*class WeaponOne : IWeapon, IShooter
    {/*

        private PoolBullet poolBullet;
        private PoolGeneric<IPooleable> poolGeneric;
        public float Cooldwon { get; private set; }

        public float CurrentCooldown { get; private set; }

        public BulletController2 BulletType { get; private set; }

        public int MaxAmmo { get; private set; }
        public int CurrentAmo { get; private set; }

        public event Action OnReload;
        public event Action OnShoot;
        public event Action OnEmptyAmmo;

        public IOwnWeapon Owner { get; private set; }

        public float Cooldown { get; private set; }

        public int CurrentAmmo { get; private set; }

        public WeaponOne(float cooldown, BulletController2 bulletType, int maxAmmo)
        {
            poolBullet = new PoolBullet();
            poolGeneric = new PoolGeneric<IPooleable>();
            Cooldwon = cooldown;
            BulletType = bulletType;
            MaxAmmo = maxAmmo;
            ReloadMaxAmmo();
        }
        public void Update()
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown -= Time.DeltaTime;
            }
            if (CurrentCooldown < 0)
            {
                CurrentCooldown = 0;

            }
        }
        public void Reload(int ammo)
        {
            CurrentAmo += ammo;
            if (CurrentAmo > MaxAmmo)
            {
                CurrentAmo = MaxAmmo;
            }
            OnReload?.Invoke();
        }

        public void ReloadMaxAmmo()
        {
            CurrentAmo = MaxAmmo;
            OnReload?.Invoke();
        }
        public void Shoot(Vector2 direction)
        {
            if (CurrentCooldown <= 0 && CurrentAmo > 0)
            {
                CurrentCooldown = Cooldwon;
                CurrentAmo -= 1;
                IBullet bullet = FactoryBullet.CreateObject(BulletType2.BulletPlayer, Owner.OwnerPosition, Owner.OwnerOffsetX);
                bullet.Direction = Owner.OwnerDirection;
                //IBullet bullet1 = poolBullet.Get(BulletType, Owner.OwnerPosition, Owner.OwnerOffsetX, Owner.OwnerOffsetY);
                //bullet1.Direction = Owner.OwnerDirection;
                //poolGeneric.Get(BulletType, Owner.OwnerPosition, Owner.OwnerOffsetX, Owner.OwnerOffsetY);
                //En el pool generic definir la direccion en el bullet controller

            }
        }
        public void StartAttack()
        {
            Shoot(Owner.OwnerDirection);
        }

        public void StopAttack()
        {

        }

        public void AssignOwner(IOwnWeapon owner)
        {
            Owner = owner;
        }
    }*/
}
