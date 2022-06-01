using System;
using System.Numerics;

namespace Game
{
    public abstract class ShipObject : GameObject, iShipAnimations
    {
        //private Vector2 Position = new Vector2(ShipPosX, ShipPosY);
        public ShipConfig ShipConfiguration { get; set; }
        public Animation ShipAnim { get;  set; }
        public Animation ShipPropellersAnim { get; set; }
        public Animation SmokeDamageAnim { get; set; }
        public Animation ShieldAnim { get; set; }
        public Vector2 RenderPosition { get; set; }
        public float Rotation = 0;
        public bool IsShielding { get; set; }

        // Others
        Animation Status = new Animation("Status", 0, Effects.GetEffectTextures(5), false, true, false);

        public override void Render()
        {
            // Draw sprites in this order: Ship -> Propellers -> Shield -> Smoke
            Engine.Draw(ShipAnim.CurrentTexture, RenderPosition.X, RenderPosition.Y, 1, 1, Rotation);
            Engine.Draw(ShipPropellersAnim.CurrentTexture, RenderPosition.X + ShipConfiguration.ShipPropellersPosition().X, RenderPosition.Y + ShipConfiguration.ShipPropellersPosition().Y, 1, 1, Rotation);

            // Updates for each animations
            ShipAnim.Update(); ShipPropellersAnim.Update(); SmokeDamageAnim.Update();

            if (IsShielding)
            {
                Engine.Draw(ShieldAnim.CurrentTexture, RenderPosition.X, RenderPosition.Y, 4, 4, Rotation, 0, -10);
                ShieldAnim.Update();
            }
            Engine.Draw(SmokeDamageAnim.CurrentTexture, RenderPosition.X, RenderPosition.Y, 1.7f, 1.7f, Rotation, 55, 65);

            if (debug)
            {
                if (life >= 7) { Status.ChangeFrame(0); Engine.Draw(Status.CurrentTexture, RenderPosition.X, RenderPosition.Y, 0.3f, 0.3f, 0, 20, 20); }
                if (life <= 7 && life > 3) { Status.ChangeFrame(1); Engine.Draw(Status.CurrentTexture, RenderPosition.X, RenderPosition.Y, 0.3f, 0.3f, 0, 20, 20); }
                if (life <= 3) { Status.ChangeFrame(2); Engine.Draw(Status.CurrentTexture, RenderPosition.X, RenderPosition.Y, 0.3f, 0.3f, 0, 20, 20); }
            }
        }

        public void UpdateShipPosition(Vector2 newPosition)
        {
            RenderPosition = newPosition;
        }
    }
}
