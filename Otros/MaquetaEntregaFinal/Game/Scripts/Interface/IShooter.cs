using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IShooter
    {
        event System.Action OnShoot;
        event System.Action OnEmptyAmmo;
        event System.Action OnReload;
        float Cooldown { get; }
        float CurrentCooldown { get; }
        Bullet BulletType { get; }
        int MaxAmmo { get; }
        int CurrentAmmo { get; }
        void Update();
        void ReloadMaxAmmo();
        void Reload(int ammo);       
        void Shoot(Vector2 direction);
    }
}
