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
        MisilPlayer,
        Fuego
    }
    public static class WeaponFactory
    {
        public static IWeapon CreateWeapon(Weapon weaponType)
        {
            switch (weaponType)
            {
                case Weapon.MisilPlayer:
                    return new MisilPlayer(0.3f, Bullet.MisilPlayer, 2000);
                case Weapon.Fuego:
                    return new Plasma(1, Bullet.Fuego, 2000);
                default:
                    return null;
            }
        }
    }
}
