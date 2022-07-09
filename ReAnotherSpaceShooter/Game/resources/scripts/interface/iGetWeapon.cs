using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public interface iGetWeapon
    {
        iWeapon CurrentWeapon { get; }
        List<iWeapon> AllWeapons { get; }
        void GetWeapon(WeaponTypes type);
    }
}
