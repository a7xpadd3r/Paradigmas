using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class MisilPlayer : IWeapon, IShooter, IPooleable
    {
        private PoolGeneric<IPooleable> poolGeneric;
        public event Action OnShoot;
        public event Action OnReload;
        public event Action OnEmptyAmmo;

        public event Action<IPooleable> OnRecyclePool;

        public IOwnWeapon Owner { get; private set; }
        public Vector2 Direction { get; set; }
        public Vector2 Position { get; set; }
        public float CurrentCooldown { get; private set; }
        public float Cooldown { get; private set; }
        public Bullet BulletType { get; private set; }
        public int MaxAmmo { get; private set; }
        public int CurrentAmmo { get; private set; }
        public Bullet Type { get; set; }

        

        public MisilPlayer(float cooldown, Bullet bulletType, int maxAmmo)
        {
            
            Cooldown = cooldown;
            BulletType = bulletType;
            MaxAmmo = maxAmmo;
            poolGeneric = new PoolGeneric<IPooleable>();
            ReloadMaxAmmo();
        }

        public void AssignOwner(IOwnWeapon owner)
        {
            Owner = owner;
        }

        public void Reload(int ammo)
        {
            CurrentAmmo += ammo;

            if (CurrentAmmo > MaxAmmo)
            {
                CurrentAmmo = MaxAmmo;
            }
            OnReload?.Invoke();
        }

        public void ReloadMaxAmmo()
        {
            CurrentAmmo = MaxAmmo;
            OnReload?.Invoke();
        }

        public void Shoot(Vector2 direction)
        {
            //GenericsPool
            if (CurrentCooldown <= 0 && CurrentAmmo > 0)
            {
                //Engine.Debug("Misil Player");
                CurrentCooldown = Cooldown;
                CurrentAmmo--;

                IPooleable bullet = poolGeneric.Get(BulletType);   
                bullet.Position = Owner.OwnerPosition;
                bullet.Direction = Owner.OwnerDirection;


            }
        }
        public void StartAttack()
        {
            Shoot(Owner.OwnerDirection);
        }

        public void StopAttack()
        {

        }

        public void Update()
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown -= Time.DeltaTime;
            }

        }

        public void Reset()
        {
           
        }
    }
}
