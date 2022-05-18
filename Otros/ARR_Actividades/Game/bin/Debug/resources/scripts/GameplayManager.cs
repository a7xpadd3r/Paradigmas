using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class GameplayManager
    {
        private static int currentLifes = 3;
        private readonly static float[] lifesPositionsOnScreen = { 30, 90, 150, 210, 270, 50 }; // Positions in X - X - X and Y 
        private readonly static List<Texture> shipSkin = PlayerTextures.SkinSet();

        public static void Update()
        {
            if (currentLifes > 0)
                for (int i = 0; i < currentLifes; i++)
                    Engine.Draw(shipSkin[shipSkin.Count() - 1], lifesPositionsOnScreen[i], lifesPositionsOnScreen[5], 0.4f, 0.4f);
        }

        public static void LifeLost()
        {
            --currentLifes;
        }
    }
}