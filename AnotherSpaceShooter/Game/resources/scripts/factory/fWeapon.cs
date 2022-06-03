using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum WeaponTypes
    {
        BlueRail, RedDiamond, GreenCrast, HeatTrail, OrbWeaver, Gamma,
        Enemy1, Enemy2, Enemy3
    }
    public static class fWeapon
    {
        public static iWeapon CreateWeapon(WeaponTypes type)
        {
            switch (type)
            {
                case WeaponTypes.BlueRail:
                    return new wBlueRail(0.2f);
                case WeaponTypes.RedDiamond:
                    return new wRedDiamond(0.2f);
                case WeaponTypes.GreenCrast:
                    return new wGreenCrast(0.2f);
                case WeaponTypes.HeatTrail:
                    return new wBlueRail(0.2f);
                case WeaponTypes.OrbWeaver:
                    return new wBlueRail(0.2f);
                case WeaponTypes.Gamma:
                    return new wBlueRail(0.2f);
                case WeaponTypes.Enemy1:
                    return new wBlueRail(0.2f);
                case WeaponTypes.Enemy2:
                    return new wBlueRail(0.2f);
                case WeaponTypes.Enemy3:
                    return new wBlueRail(0.2f);
                default:
                    return new wBlueRail(0.2f);
            }
        }
    }
}
