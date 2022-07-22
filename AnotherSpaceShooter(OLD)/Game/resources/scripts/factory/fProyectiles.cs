
using System.Numerics;

namespace Game
{
    public enum ProyectileTypes
    {
        BlueRail, RedDiamond, GreenCrast, OrbWeaver, Gamma, // Player proyectiles
        Enemy1, Enemy2, Enemy3 // Enemy proyectiles
    }

    // Not needed?
    /*
    public static class fProyectiles
    {
        public static  iProyectile CreateProyectile(ProyectileTypes type)
        {
            switch (type)
            {
                case ProyectileTypes.BlueRail:
                    return new pBlueRail(new Transform());
                case ProyectileTypes.RedDiamond:
                    return new pRedDiamond(new Transform());
                case ProyectileTypes.GreenCrast:
                    return new pGreenCrast(new Transform());
                case ProyectileTypes.OrbWeaver:
                    return new pBlueRail(new Transform());
                case ProyectileTypes.Gamma:
                    return new pBlueRail(new Transform());

                case ProyectileTypes.Enemy1:
                    return new pBlueRail(new Transform());
                case ProyectileTypes.Enemy2:
                    return new pBlueRail(new Transform());
                case ProyectileTypes.Enemy3:
                    return new pBlueRail(new Transform());
                default:
                    return new pBlueRail(new Transform());
            }

        }
    }*/
}
