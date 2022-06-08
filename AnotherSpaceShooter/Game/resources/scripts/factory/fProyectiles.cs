
using System.Numerics;

namespace Game
{
    public enum ProyectileTypes
    {
        BlueRail, RedDiamond, GreenCrast, OrbWeaver, Gamma, // Player proyectiles
        Enemy1, Enemy2, Enemy3 // Enemy proyectiles
    }
    public static class fProyectiles
    {
        public static  iProyectile CreateProyectile(ProyectileTypes type)
        {
            switch (type)
            {
                case ProyectileTypes.BlueRail:
                    return new pBlueRail(new Vector2());
                case ProyectileTypes.RedDiamond:
                    return new pBlueRail(new Vector2());
                case ProyectileTypes.GreenCrast:
                    return new pBlueRail(new Vector2());
                case ProyectileTypes.OrbWeaver:
                    return new pBlueRail(new Vector2());
                case ProyectileTypes.Gamma:
                    return new pBlueRail(new Vector2());

                case ProyectileTypes.Enemy1:
                    return new pBlueRail(new Vector2());
                case ProyectileTypes.Enemy2:
                    return new pBlueRail(new Vector2());
                case ProyectileTypes.Enemy3:
                    return new pBlueRail(new Vector2());
                default:
                    return new pBlueRail(new Vector2());
            }

        }
    }
}
