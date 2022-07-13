using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class Star
    {
        private static List<Texture> stars = Textures.GetBackgroundSprite(Background.Stars);
        private Texture star = stars[3];
        private static Random random = new Random();
        bool active = true;

        // Movement stuff
        private float speed = 600;
        private float posX = 0;
        private float posY = -50;
        private Vector2 Position => new Vector2(posX, posY);
        private Vector2 PlayerPos => new Vector2(GameManager.PlayerPosition.X / xModifier, ((GameManager.PlayerPosition.Y / yModifier) * -2));

        // This adds movement with the player
        private float xModifier = 1;
        private float yModifier = 1;

        // Timer for X/Y position
        private float playerX = 0;
        private float newPlayerX = 0;
        private float playerY = 0;
        private float newPlayerY = 0;
        private float time = 0.1f;
        private float currentTime = 0.1f;

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

            int randomXModifier = random.Next(5, 15);
            xModifier = randomXModifier;

            int randomYModifier = random.Next(3, 5);
            yModifier = randomYModifier;
        }

        public void Update(float delta)
        {
            if (active)
            {
                posY += speed * delta;
                Engine.Draw(star, Position.X, Position.Y);

                if (currentTime <= time)
                {
                    playerX = PlayerPos.X;
                    playerY = PlayerPos.Y;
                    currentTime += delta;
                }

                if (currentTime > time)
                {
                    newPlayerY = PlayerPos.Y;
                    newPlayerX = PlayerPos.X;
                    currentTime = 0;
                }

                // Update position following the player movements
                if (newPlayerX < playerX) posX -= speed / xModifier * delta;
                else if (newPlayerX > playerX) posX += speed / xModifier * delta;
                if (newPlayerY < playerY) posY += speed / yModifier * delta;
                else if (newPlayerY > playerY) posY -= speed / yModifier * delta;

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
                    xModifier = random.Next(5, 15);
                    yModifier = random.Next(3, 5);
                }
            }
        }
    }
}
