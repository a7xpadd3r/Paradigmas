using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class UI
    {
        // Shippys
        private static int currentShip = 0;
        private static int shippys = 3;
        private static Texture currentShippy = ShipsTextures.GetShipTextures(currentShip)[4];
        private static Texture initialShippy = currentShippy;
        private static NumbersToSprites score2sprites = new NumbersToSprites();
        private static NumbersToSprites ammo2sprites = new NumbersToSprites();
        public static int ShippysLeft => shippys;

        // Score
        public static double score = 0;
        public static double ammo = 0;
        private static Vector2 shippysRenderPos = new Vector2(20, 50);
        private static readonly float shippysOffset = 50;

        // Weapon selection
        private static Animation weapBox = new Animation("WeaponBox", 0.06f, UITextures.GetUITextures(5));
        private static List<iWeapon> PlayerWeapons = new List<iWeapon>();
        private static iWeapon CurrentWeapon = null;

        public static void UpdateAmmo(double howMuch)
        {
            ammo = howMuch;
        }

        public static void UpdateScore(double addpoints)
        {
            score += addpoints;
        }

        public static void UpdateCurrentWeapons(iWeapon updateCurrent)
        {
            CurrentWeapon = updateCurrent;
        }

        public static void UpdateWeapons(List<iWeapon> list)
        {
            PlayerWeapons = list;
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
            // Draw score & ammo
            score2sprites.RenderNumbers(score, new Vector2(35, 20), new Vector2(1.5f, 1.5f));
            if (ammo > 0) ammo2sprites.RenderNumbers(ammo, new Vector2(1850, 100), new Vector2(1.5f, 1.5f), true);

            // Draw lifes left
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

            // Draw inventory & selected weapon
            if (PlayerWeapons.Count != 0)
            {
                weapBox.Update();
                float drawX = 1830;
                foreach (iWeapon weap in PlayerWeapons)
                {
                    string theWeap = weap.ToString(); // Janky stuff but does the job.
                    var invAnimations = ProyectilesTextures.GetProyectileAnim(0);
                    switch (theWeap)
                    {
                        case "Game.wBlueRail":
                            invAnimations = ProyectilesTextures.GetProyectileAnim(0);
                            Engine.Draw(invAnimations.CurrentTexture, drawX, 20, 1.3f, 1.3f, 0, 17.5f, -3);
                            //Engine.Draw(ItemsTextures.GetItemTexture(3), drawX, 20, 1.3f, 1.3f, 0, 17.5f, -10);
                            break;
                        case "Game.wRedDiamond":
                            invAnimations = ProyectilesTextures.GetProyectileAnim(1);
                            Engine.Draw(invAnimations.CurrentTexture, drawX, 20, 1.3f, 1.3f, 0, 17.5f, -3);
                            //Engine.Draw(ItemsTextures.GetItemTexture(4), drawX, 20, 1.3f, 1.3f, 0, 17.5f, -10);
                            break;
                        case "Game.wGreenCrast":
                            invAnimations = ProyectilesTextures.GetProyectileAnim(3);
                            Engine.Draw(invAnimations.CurrentTexture, drawX, 20, 1.3f, 1.3f, 0, 17.5f, -3);
                            //Engine.Draw(ItemsTextures.GetItemTexture(5), drawX, 20, 1.3f, 1.3f, 0, 17.5f, -10);
                            break;
                        case "Game.wHeatTrail":
                            invAnimations = ProyectilesTextures.GetProyectileAnim(4);
                            Engine.Draw(invAnimations.CurrentTexture, drawX, 20, 1.3f, 1.3f, 0, 17.5f, -9);
                            //Engine.Draw(ItemsTextures.GetItemTexture(6), drawX, 20, 1.3f, 1.3f, 0, 17.5f, -10);
                            break;
                    }
                    if (CurrentWeapon == weap)
                    {
                        weapBox.Update();
                        Engine.Draw(weapBox.CurrentTexture, drawX - 28.5f, 20, 2, 2); // Draw the indicator.
                    }
                    drawX -= 90;
                }
            }
        }
    }
}
