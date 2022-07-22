using System.Numerics;

namespace Game
{
    public class PlayerStalker
    {
        // Input position
        private float posX = 0;
        private float posY = 0;

        // Player position
        private float playerX = 0;
        private float newPlayerX = 0;
        private float playerY = 0;
        private float newPlayerY = 0;

        // Timer
        private float time = 0.1f;
        private float currentTime = 0.1f;
        private Vector2 PlayerPos => new Vector2(Player.Position.X, Player.Position.Y);
        public Vector2 ReturnPos => new Vector2(posX, posY);

        public void Update(float speed)
        {
            if (currentTime <= time)
            {
                playerX = PlayerPos.X;
                playerY = PlayerPos.Y;
                currentTime += Program.GetDeltaTime();
            }

            if (currentTime > time)
            {
                newPlayerY = PlayerPos.Y;
                newPlayerX = PlayerPos.X;
                currentTime = 0;
            }

            // Update position following the player movements
            if (newPlayerX < playerX) posX -= speed * Program.GetDeltaTime();
            else if (newPlayerX > playerX) posX += speed * Program.GetDeltaTime();
            if (newPlayerY < playerY) posY -= speed / 25 * Program.GetDeltaTime();
            else if (newPlayerY > playerY) posY += speed * Program.GetDeltaTime();
        }
    }
}
