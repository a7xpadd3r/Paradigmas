namespace Game
{
    public enum WeaponTypes
    {
        BlueRail, RedDiamond, RedDiamondBall, GreenCrast, HeatTrail, OrbWeaver, Gamma, // Player weapons
        Enemy1, Enemy2, Enemy3 // Enemy weapons
    }
    public static class fWeapons
    {
        public static iWeapon CreateWeapon(WeaponTypes type)
        {
            switch (type)
            {
                case WeaponTypes.BlueRail: return new wBlueRail();
                case WeaponTypes.RedDiamond: return new wRedDiamond();
                case WeaponTypes.GreenCrast: return new wGreenCrast();
                case WeaponTypes.HeatTrail: return new wHeatTrail();
                case WeaponTypes.OrbWeaver: return new wOrbWeaver();
                case WeaponTypes.Gamma: return new wGamma();

                case WeaponTypes.Enemy1: return new wBlueRail();
                case WeaponTypes.Enemy2: return new wBlueRail();
                case WeaponTypes.Enemy3: return new wBlueRail();
                default: return new wBlueRail();
            }
        }
    }
}
