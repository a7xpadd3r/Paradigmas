using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Plasma : IWeapon, IShooter
    {
        public IOwnWeapon Owner { get; private set; }

        public float CurrentCooldown { get; private set; }

        public float Cooldown { get; private set; }

        public Bullet BulletType { get; private set; }

        public int MaxAmmo { get; private set; }

        public int CurrentAmmo { get; private set; }

        public event Action OnShoot;
        public event Action OnReload;
        public event Action OnEmptyAmmo;

        public Plasma(float cooldown, Bullet bulletType, int maxAmmo)
        {
            Cooldown = cooldown;
            BulletType = bulletType;
            MaxAmmo = maxAmmo;

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
            if (CurrentCooldown <= 0 && CurrentAmmo > 0)
            {
                Engine.Debug("Ataque Plasma");
                CurrentCooldown = Cooldown;
                CurrentAmmo--;
                IBullet bullet = BulletFactory.CreateBullet(BulletType, Owner.OwnerPosition);
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
    }
}
