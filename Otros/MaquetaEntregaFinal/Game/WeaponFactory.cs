using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum Weapon
    {
        MisilEnemy,
        Plasma,
        MisilPlayer
    }

    public static class WeaponFactory
    {
        public static IWeapon CreateWeapon(Weapon weaponType)
        {
            switch (weaponType)
            {
                case Weapon.MisilEnemy:
                    return new MisilEnemy(1, Bullet.MisilEnemy, 2000);
                case Weapon.Plasma:
                    return new Plasma(0.5f, Bullet.Plasma, 2000);
                case Weapon.MisilPlayer:
                    return new Plasma(0.5f, Bullet.MisilPlayer, 2000);
                default:
                    return null;
            }
        }
    }
}
