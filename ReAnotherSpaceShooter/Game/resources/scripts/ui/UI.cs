using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class UI
    {
        private int currentlifes = 0;
        private ShipData currentship;
        private float separation = 60;
        private Vector2 renderposition = new Vector2(80, 120 );

        // Sprites
        private NumbersToSprites score2sprites = new NumbersToSprites();
        private NumbersToSprites ammo2sprites = new NumbersToSprites();
        private List<Texture> weaplist = Textures.GetUITextures(UITextures.WeaponList);
        private Texture currentdamagetexture;

        // Status
        public double score = 0;
        public double ammo = 0;

        // Weapon selection
        private Animation weapBox = new Animation("WeaponBox", 0.015f, Textures.GetUITextures(UITextures.SlotBox));
        private List<iWeapon> PlayerWeapons = new List<iWeapon>();
        private iWeapon CurrentWeapon;

        public UI(int selection) 
        {
            switch (selection)
            {
                case 0: currentship = ShipsProperties.ElCapitan; break;
                case 1: currentship = ShipsProperties.SonicShip; break;
                case 2: currentship = ShipsProperties.SkullFlower; break;
                default: currentship = ShipsProperties.ElCapitan; break;
            }

            currentdamagetexture = currentship.ShipAnim.GetFrameTexture(0);
            PlayerWeapons.Add(fWeapons.CreateWeapon(WeaponTypes.BlueRail));
            CurrentWeapon = PlayerWeapons[0];
        }
        public void Update()
        {
            score2sprites.RenderNumbers(score, new Vector2(35, 20), new Vector2(1.5f, 1.5f));
            if (ammo > 0) ammo2sprites.RenderNumbers(ammo, new Vector2(1850, 100), new Vector2(1.5f, 1.5f), true);
            if (currentlifes > 0 && currentship != null)
            {
                for (int i = 0; i < currentlifes; i++)
                {
                    Vector2 newrpos = new Vector2(renderposition.X + separation * i, renderposition.Y);
                    if (i == currentlifes -1) Renderer.DrawCenter(currentdamagetexture, newrpos, new Vector2(0.5f, 0.5f));
                    else Renderer.DrawCenter(currentship.ShipAnim.GetFrameTexture(0), newrpos, new Vector2(0.5f, 0.5f));
                }
            }
            if (PlayerWeapons.Count > 0)
            {
                float drawX = 1830;
                foreach (iWeapon weap in PlayerWeapons)
                {
                    string theWeap = weap.ToString(); // Janky stuff but does the job.
                    switch (theWeap)
                    {
                        case "Game.wBlueRail":   Engine.Draw(weaplist[0], drawX, 20, 1.3f, 1.3f, 0, 17.5f, -3); break;
                        case "Game.wRedDiamond": Engine.Draw(weaplist[1], drawX, 20, 1.3f, 1.3f, 0, 17.5f, -3); break;
                        case "Game.wGreenCrast": Engine.Draw(weaplist[2], drawX, 20, 1.3f, 1.3f, 0, 17.5f, -3); break;
                        case "Game.wHeatTrail":  Engine.Draw(weaplist[3], drawX, 20, 1.3f, 1.3f, 0, 17.5f, -9); break;
                        case "Game.wOrbWeaver":  Engine.Draw(weaplist[4], drawX - 2, 20, 0.7f, 0.7f, 0, 17.5f, -9); break;
                        case "Game.wGamma":      Engine.Draw(weaplist[5], drawX + 12, 29, 1.3f, 1.3f, 0, 17.5f, -9); break;
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
        public void UpdateLifes(int newLifes) { currentlifes = newLifes; }
        public void UpdateAmmo(double howMuch) { ammo = howMuch; }
        public void UpdateScore(double addpoints) { score += addpoints; }
        public void UpdateCurrentWeapon(iWeapon updateCurrent) { CurrentWeapon = updateCurrent; }
        public void UpdateWeapons(List<iWeapon> list) { PlayerWeapons = list; }
        public void UpdateShipLifeTexture(Texture status) { currentdamagetexture = status; } 
    }
}
