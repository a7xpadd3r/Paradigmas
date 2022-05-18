using Game.resources.scripts;
using System;

namespace Game
{
    public class GFX_GenericStar
    {
        // Configurable data
        public string name = "genericStar";
        private float speed = 400f;

        // Should-not-touch stuff
        private Texture starTexture = LevelsGFX.GetRandomStarFrame();
        private float defaultY = -10f;
        private float posY = 0f;
        private float maxY = 1100f;
        private Random rnd = new Random();

        private int rightX = -1000;
        private float xStartValue = Program.RndAnother(); // No need for initialization, when the timer resets, this value will too
        private int xModifier = 0;
       
        public GFX_GenericStar(float speedModifier = 0, int maxX = 2920)
        {
            speed += speedModifier;
            rightX = maxX;
        }

        public void Update()
        {
            posY += speed * Program.GetDeltaTime();

            if (posY > maxY)
            {
                posY = defaultY;
                xStartValue = rnd.Next(rightX, 1920);
                xModifier = rnd.Next(2, 5);
                Texture starTexture = LevelsGFX.GetRandomStarFrame();
            }

            // Follow the player by a random value
            float newXpos = xStartValue - PlayerConfig.GetPos() / xModifier ;
            // Draw only if visible
            if (newXpos > -10 && newXpos < 1930)
                Engine.Draw(starTexture, newXpos, posY);
        }

    }
}
