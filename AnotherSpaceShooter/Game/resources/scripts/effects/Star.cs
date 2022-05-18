using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class Star
    {
        private static List<Texture> stars = Effects.GetEffectTextures(0);
        private Texture star = stars[3];
        private static Random random = new Random();
        bool active = true;

        // Movement stuff
        private float speed = 600;
        private float posX = 0;
        private float posY = -50;
        private Vector2 Position => new Vector2(posX, posY);
        private Vector2 PlayerPos => new Vector2(Player.Position.X / xModifier, (Player.Position.Y / yModifier) * -2);

        // This adds movement with the player
        private float xModifier = 1;
        private float yModifier = 1;


        public bool Active => active;

        public Star()
        {
            int randomStar = random.Next(0, stars.Count);
            star = stars[randomStar];

            int randomPosX = random.Next(0, 1920);
            posX = random.Next(0, randomPosX);

            int randomPosY = random.Next(0, 1080);
            posY = random.Next(0, randomPosY);

            int randomSpeed = random.Next(100, 800);
            speed = randomSpeed;

            int randomXModifier = random.Next(5, 40);
            xModifier = randomXModifier;

            int randomYModifier = random.Next(5, 40);
            yModifier = randomYModifier;
        }

        public void Update()
        {
            if (active)
            {
                posY += speed * Program.GetDeltaTime();
                Engine.Draw(star, Position.X - PlayerPos.X, Position.Y + PlayerPos.Y);

                if (posY > 1300)
                {
                    // Randomize POSY
                    posY = -50;
                    star = stars[random.Next(0, stars.Count)];

                    // Randomize POSX
                    posX = random.Next(0, 1920);

                    // Randomize speed
                    speed = random.Next(100, 800);

                    // Randomize X modifiers
                    xModifier = random.Next(5, 40);
                    yModifier = random.Next(5, 40);
                }
            }
        }
    }
}
