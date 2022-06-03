using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public interface iGetWeapon
    {
        Vector2 OwnerRailPosition { get; }
        iWeapon CurrentWeapon { get; }
        List<iWeapon> AllWeapons { get; }
        void GetWeapon(iWeapon newWeapon);
    }
}
