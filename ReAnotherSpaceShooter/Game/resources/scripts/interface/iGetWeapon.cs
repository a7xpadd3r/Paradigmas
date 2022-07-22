using System.Collections.Generic;

namespace Game
{
    public interface iGetWeapon
    {
        iWeapon CurrentWeapon { get; }
        List<iWeapon> AllWeapons { get; }
        void GetWeapon(WeaponTypes type);
    }
}
