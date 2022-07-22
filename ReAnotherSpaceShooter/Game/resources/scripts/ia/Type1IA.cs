using System;
using System.Numerics;

namespace Game
{
    public class Type1IA : iIA
    {
        // To the Ship
        public Vector2 IAPosition => new Vector2(posX, posY);

        // Directions
        private AxisX dirX = AxisX.Left;
        private AxisY dirY = AxisY.Down;
        private MovementType movType = MovementType.Lineal;

        // General position
        private float posX;
        private float posY;

        // Wave movement
        private float cycle = 0;
        private float wspeed = 15;
        public float Speed { get; set; }

        // Bounds
        private Vector2 Bounds = new Vector2(1920 + 100, 1080 + 100);
        private Vector2 NegativeBounds = new Vector2(-100, -100);
        public Action OutOfBounds;

        public void Update(float delta)
        {
            if (movType == MovementType.WaveY || movType == MovementType.WaveX)
            {
                this.cycle += wspeed * delta;
                if (movType == MovementType.WaveX)
                {
                    // Wave movement for X
                    if (this.dirX == AxisX.Right) this.posX += this.Speed * delta * (float)Math.Sin(this.cycle);
                    else if (this.dirX == AxisX.Left) this.posX -= this.Speed * delta * (float)Math.Sin(this.cycle);

                    // Lineal movement for Y
                    if (this.dirY == AxisY.Down) this.posY += this.Speed * delta;
                    else if (this.dirY == AxisY.Up) this.posY -= this.Speed * delta;
                }
                else
                {
                    // Lineal movement for X
                    if (this.dirX == AxisX.Right) this.posX += this.Speed * delta;
                    else if (this.dirX == AxisX.Left) this.posX -= this.Speed * delta;

                    // Wave movement for Y
                    if (this.dirY == AxisY.Down) this.posY += this.Speed * delta * (float)Math.Sin(this.cycle);
                    else if (this.dirY == AxisY.Up) this.posY -= this.Speed * delta * (float)Math.Sin(this.cycle);
                }
            }

            // No wave
            else
            {
                if (this.dirX == AxisX.Right) this.posX += this.Speed * delta;
                else if (this.dirX == AxisX.Left) this.posX -= this.Speed * delta;
                if (this.dirY == AxisY.Down) this.posY += this.Speed * delta;
                else if (this.dirY == AxisY.Up) this.posY -= this.Speed * delta;
            }

            // Bounds
            if (this.posX > this.Bounds.X) this.dirX = AxisX.Left;
            else if (this.posX < this.NegativeBounds.X) this.dirX = AxisX.Right;
            if (this.posY > this.Bounds.Y) this.dirY = AxisY.Up;
            else if (this.posY < this.NegativeBounds.Y) this.dirY = AxisY.Down;
        }

        // Lineal Diamond form
        public Type1IA(float newSpeed, Vector2 startingPos, MovementType movementtype = MovementType.Lineal, float minX = -100,float maxX = 2020, AxisY moveY = AxisY.None,float minY = -100, float maxY = 1180)
        {
            this.Speed = newSpeed;
            this.posX = startingPos.X;
            this.posY = startingPos.Y;
            this.NegativeBounds = new Vector2(minX, minY);
            this.movType = movementtype;
            this.dirY = moveY;

            if (movementtype == MovementType.WaveY && moveY == AxisY.None) this.dirY = AxisY.Down;

            if (posX < Bounds.X / 2) dirX = AxisX.Left;
            else dirX = AxisX.Right;
        }
    }
}
