using System;
using System.Numerics;

namespace Game
{
    public class UI
    {
        public static double score = 0;
        private static int currentShip = 0;
        private static int shippys = 3;
        private static Texture currentShippy = ShipsTextures.GetShipTextures(currentShip)[4];
        private static Texture initialShippy = currentShippy;
        static NumbersToSprites n2s = new NumbersToSprites();

        private static Vector2 shippysRenderPos = new Vector2(20, 50);
        private static readonly float shippysOffset = 50;

        public static int ShippysLeft => shippys;

        public static void UpdateScore(double addpoints)
        {
            score += addpoints;
        }

        public static void UpdateLifesLeft()
        {
            shippys -= 1;
        }

        public static void UpdateUIShippy(Texture newTexture)
        {
            currentShippy = newTexture;
        }

        public static void Update()
        {
            n2s.RenderNumbers(score, new Vector2(35, 20), new Vector2(1.5f, 1.5f));

            if (shippys != 0)
            {
                float currentShippyOffset = 0;
                for (int i = 0; i < shippys; i++)
                {
                    if (i == shippys - 1)
                        Engine.Draw(currentShippy, shippysRenderPos.X + currentShippyOffset, shippysRenderPos.Y, 0.4f, 0.4f);

                    else
                        Engine.Draw(initialShippy, shippysRenderPos.X + currentShippyOffset, shippysRenderPos.Y, 0.4f, 0.4f);

                    currentShippyOffset += shippysOffset;
                }
            }
        }
    }
}
