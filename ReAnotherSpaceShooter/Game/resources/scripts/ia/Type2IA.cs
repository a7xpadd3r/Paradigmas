using System;
using System.Numerics;

namespace Game
{
    public class Type2IA : iIA
    {
        // To the Ship
        public Vector2 IAPosition => new Vector2(posX, posY);

        // General position
        private float posX;
        private float posY;
        private Random random = new Random();

        // Movement
        private Vector2 destination = new Vector2();
        private bool destinationreached = true;
        public bool CanShoot => destinationreached;
        private float tolerance = 50;

        // Timer
        private float currentMovementCD = 0;
        private float maxMovementCD = 1;

        public float Speed { get; set; }

        public void Update(float delta)
        {
            if (this.currentMovementCD < this.maxMovementCD && this.destinationreached) 
                this.currentMovementCD += delta;

            if (!InRange(this.posX, this.destination.X))
            {
                if (this.posX < this.destination.X) this.posX += this.Speed * delta;
                else this.posX -= this.Speed * delta;
            }

            if (!InRange(this.posY, this.destination.Y))
            {
                if (this.posY < this.destination.Y) this.posY += this.Speed * delta;
                else this.posY -= this.Speed * delta;
            }

            if (InRange(this.posX, this.destination.X) && InRange(this.posY, this.destination.Y))
                this.destinationreached = true;

            if (this.destinationreached && this.currentMovementCD >= this.maxMovementCD)
            {
                int hitchange = random.Next(0, 10);
                if (hitchange < 7) this.destination = new Vector2(random.Next(20, 1900), random.Next(20, 1060));
                else this.destination = GameManager.PlayerPosition;

                this.maxMovementCD = random.Next(0, 8);
                this.currentMovementCD = 0;
                this.destinationreached = false;
            }
        }

        // Lineal Diamond form
        public Type2IA(float newSpeed, Vector2 startingPos)
        {
            this.Speed = newSpeed;
            this.posX = startingPos.X;
            this.posY = startingPos.Y;
            this.destination = new Vector2(random.Next(20, 1900), random.Next(20, 1060));
        }
        private bool InRange(float input, float needed)
        {
            bool value = false;
            if (input < needed + this.tolerance && input > needed - this.tolerance) value = true;
            return value;
        }
    }
}
