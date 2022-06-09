using System;
using System.Numerics;

namespace Game
{
    public abstract class ShipObject : GameObject, iShipAnimations
    {
        public ShipConfig ShipConfiguration { get; set; }
        public Animation ShipAnim { get;  set; }
        public Animation ShipPropellersAnim { get; set; }
        public Animation ShieldAnim { get; set; }
        //public Vector2 RenderPosition { get; set; }
        public Transform RenderTransform { get; set; }
        public Vector2 TextureSize => new Vector2(ShipAnim.CurrentTexture.Width, ShipAnim.CurrentTexture.Height);
        public float Rotation = 0;
        public int ShipStatus = 4; // Ships should always have 4 statuses and 1 extra blank to use when the ship is destroyed.
        public bool IsShielding { get; set; }

        // Blinkin when appearing
        public bool BlinkingEnded = false;
        private bool blinking = true;
        private bool draw = true;
        private float invFramesDuration = 0.08f;
        private float currentInv = 0;
        private int howManyBlinks = 15;
        private int currentBlinks = 0;

        //private Transform transform => new Transform(RenderPosition, new Vector2(1, 1));

        // Dot indicator
        Animation Status = new Animation("Status", 0, Effects.GetEffectTextures(5), false, true);

        public override void Render()
        {
            if (ready)
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
                        Engine.DrawTransform(ShipAnim.CurrentTexture, RenderTransform);
                        Engine.DrawTransform(ShipPropellersAnim.CurrentTexture, new Transform(Position - ShipConfiguration.ConfigShipPropellersPosition, new Vector2(1,1)));
                        //Engine.Draw(ShipAnim.CurrentTexture, RenderTransform.Position.X, RenderTransform.Position.Y, 1, 1, Rotation);
                        //Engine.Draw(ShipPropellersAnim.CurrentTexture, RenderTransform.Position.X + ShipConfiguration.ConfigShipPropellersPosition.X, RenderTransform.Position.Y + ShipConfiguration.ConfigShipPropellersPosition.Y, 1, 1, Rotation);
                        draw = false;
                    }
                }


                if (owner != "Player")
                {
                    //Engine.Draw(ShipAnim.CurrentTexture, RenderTransform.Position.X, RenderTransform.Position.Y, 1, 1, Rotation);
                    Engine.DrawTransform(ShipAnim.CurrentTexture, RenderTransform);
                    if (ShipPropellersAnim != null) Engine.Draw(ShipPropellersAnim.CurrentTexture, RenderTransform.Position.X + ShipConfiguration.ConfigShipPropellersPosition.X, RenderTransform.Position.Y + ShipConfiguration.ConfigShipPropellersPosition.Y, 1, 1, Rotation);
                }

                // Updates for each animations
                ShipAnim.Update(); if (ShipPropellersAnim != null) ShipPropellersAnim.Update();

                if (IsShielding)
                {
                    Engine.Draw(ShieldAnim.CurrentTexture, RenderTransform.Position.X, RenderTransform.Position.Y, 4, 4, Rotation, 0, -10);
                    ShieldAnim.Update();
                }

                if (debug) // Show little dots indicating life status
                {
                    if (currentLifes >= 7) { Status.ChangeFrame(0); Engine.Draw(Status.CurrentTexture, RenderTransform.Position.X, RenderTransform.Position.Y, 0.3f, 0.3f, 0, 20, 20); }
                    if (currentLifes <= 7 && currentLifes > 3) { Status.ChangeFrame(1); Engine.Draw(Status.CurrentTexture, RenderTransform.Position.X, RenderTransform.Position.Y, 0.3f, 0.3f, 0, 20, 20); }
                    if (currentLifes <= 3) { Status.ChangeFrame(2); Engine.Draw(Status.CurrentTexture, RenderTransform.Position.X, RenderTransform.Position.Y, 0.3f, 0.3f, 0, 20, 20); }
                }
            }
        }

        public void UpdateShipPosition(Transform newTransform)
        {
            RenderTransform = newTransform;
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
