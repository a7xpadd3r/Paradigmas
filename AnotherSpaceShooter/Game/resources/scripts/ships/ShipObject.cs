using System;
using System.Numerics;

namespace Game
{
    public abstract class ShipObject : GameObject, iShipAnimations
    {
        public ShipConfig ShipConfiguration { get; set; }
        public Animation ShipAnim { get;  set; }
        public Animation ShipPropellersAnim { get; set; }
        public Animation SmokeDamageAnim { get; set; }
        public Animation ShieldAnim { get; set; }
        public Vector2 RenderPosition { get; set; }
        public float Rotation = 0;
        public int ShipStatus = 4; // Ships should always have 4 statuses and 1 extra blank to use when the ship is destroyed.
        public bool IsShielding { get; set; }
        public bool RenderSmoke = false;

        // Blinkin when appearing
        public bool BlinkingEnded = false;
        private bool blinking = true;
        private bool draw = true;
        private float invFramesDuration = 0.08f;
        private float currentInv = 0;
        private int howManyBlinks = 15;
        private int currentBlinks = 0;

        // Dot indicator
        Animation Status = new Animation("Status", 0, Effects.GetEffectTextures(5), false, true);

        public override void Render()
        {
            if (owner == "Player") // ONLY Player blinks when appears
            {
                if (blinking && currentInv < invFramesDuration)
                {
                    currentInv += Program.GetDeltaTime();
                }

                if (blinking && currentInv >= invFramesDuration && currentBlinks < howManyBlinks)
                {
                    currentBlinks++;
                    currentInv = 0;
                    draw = true;
                }

                if (currentBlinks >= howManyBlinks) { blinking = false; draw = true; BlinkingEnded = true; }

                if (draw)
                {
                    Engine.Draw(ShipAnim.CurrentTexture, RenderPosition.X, RenderPosition.Y, 1, 1, Rotation);
                    Engine.Draw(ShipPropellersAnim.CurrentTexture, RenderPosition.X + ShipConfiguration.ShipPropellersPosition().X, RenderPosition.Y + ShipConfiguration.ShipPropellersPosition().Y, 1, 1, Rotation);
                    draw = false;
                }
            }

            if (owner != "Player")
            {
                Engine.Draw(ShipAnim.CurrentTexture, RenderPosition.X, RenderPosition.Y, 1, 1, Rotation);
                Engine.Draw(ShipPropellersAnim.CurrentTexture, RenderPosition.X + ShipConfiguration.ShipPropellersPosition().X, RenderPosition.Y + ShipConfiguration.ShipPropellersPosition().Y, 1, 1, Rotation);
            }

            // Updates for each animations
            ShipAnim.Update(); ShipPropellersAnim.Update(); SmokeDamageAnim.Update();

            if (IsShielding)
            {
                Engine.Draw(ShieldAnim.CurrentTexture, RenderPosition.X, RenderPosition.Y, 4, 4, Rotation, 0, -10);
                ShieldAnim.Update();
            }

            if (RenderSmoke) Engine.Draw(SmokeDamageAnim.CurrentTexture, RenderPosition.X, RenderPosition.Y, 1.7f, 1.7f, Rotation, 55, 65);

            if (debug) // Show little dots indicating life status
            {
                if (life >= 7) { Status.ChangeFrame(0); Engine.Draw(Status.CurrentTexture, RenderPosition.X, RenderPosition.Y, 0.3f, 0.3f, 0, 20, 20); }
                if (life <= 7 && life > 3) { Status.ChangeFrame(1); Engine.Draw(Status.CurrentTexture, RenderPosition.X, RenderPosition.Y, 0.3f, 0.3f, 0, 20, 20); }
                if (life <= 3) { Status.ChangeFrame(2); Engine.Draw(Status.CurrentTexture, RenderPosition.X, RenderPosition.Y, 0.3f, 0.3f, 0, 20, 20); }
            }
        }

        public void OnSmokeEnded()
        {
            RenderSmoke = false;
        }

        public void UpdateShipPosition(Vector2 newPosition)
        {
            RenderPosition = newPosition;
        }

        public void ResetBlinking(int newBlinksAmount = 15)
        {
            blinking = true;
            BlinkingEnded = false;
            currentInv = 0;
            currentBlinks = 0;
            howManyBlinks = newBlinksAmount;
        }
    }
}
