using System;
using System.Numerics;

namespace Game
{
    public abstract class ShipObject : GameObject, iShipAnimations
    {
        public static float ShipPosX = 0;
        public static float ShipPosY = 0;
        private Vector2 Position = new Vector2(ShipPosX, ShipPosY);
        public Animation ShipAnim { get;  set; }
        public Animation ShipPropellersAnim { get; set; }
        public Animation SmokeDamageAnim { get; set; }
        public Animation ShieldAnim { get; set; }

        public override void Render()
        {
                // Update animations
                Engine.Draw(ShipAnim.CurrentTexture, Position.X, Position.Y);

                // Updates for each animations
                ShipAnim.Update(); ShipPropellersAnim.Update(); SmokeDamageAnim.Update(); ShieldAnim.Update();
            Console.WriteLine("asd");
        }

        public void UpdateShipPosition(Vector2 newPosition)
        {
            ShipPosX = newPosition.X;
            ShipPosY = newPosition.Y;
        }
    }
}
