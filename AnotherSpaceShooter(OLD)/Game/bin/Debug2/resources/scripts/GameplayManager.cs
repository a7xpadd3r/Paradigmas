using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.resources.scripts
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
                    Engine.Draw(shipSkin[0], lifesPositionsOnScreen[i], lifesPositionsOnScreen[5], 0.4f, 0.4f);
        }

        public static void LifeLost()
        {
            --currentLifes;
        }
    }
}
