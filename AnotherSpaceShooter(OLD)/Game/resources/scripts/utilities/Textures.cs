﻿using System;
using System.Collections.Generic;

namespace Game
{
    public enum Ships { ElCapitan, Ship2, Ship3 }

    public class Textures
    {
        public static void InitializeTextures()
        {

            UITextures.InitializeUITextures();
            ShipsTextures.InitializeShipsTextures();
            PropellersTextures.InitializePropellersTextures();
            ProyectilesTextures.InitializeProyectilesTextures();
            Effects.InitializeEffects();

            Console.WriteLine("Texturas inicializadas.");
        }
    }

    public static class ShipsTextures
    {
        // Player ships
        private static List<Texture> ElCapitanTextures = new List<Texture>();
        private static List<Texture> Ship2Textures = new List<Texture>();
        private static List<Texture> Ship3Textures = new List<Texture>();

        // Enemy ships
        private static List<Texture> GenericShipTextures = new List<Texture>();
        private static List<Texture> eMosquitoe = new List<Texture>();

        // Animations (for those ships that doesn't need damage states)
        private static Animation aMosquitoe = new Animation("Mosquitoe Ship Anim", 0.09f, eMosquitoe);


        public static void InitializeShipsTextures()
        {
            // Empty image
            ElCapitanTextures.Add(new Texture("resources/gfx/blank.png"));
            Ship2Textures.Add(new Texture("resources/gfx/blank.png"));
            Ship3Textures.Add(new Texture("resources/gfx/blank.png"));

            // El Capitán textures
            for (int i = 1; i < 5; i++)
                ElCapitanTextures.Add(new Texture("resources/gfx/ships/elcapitan/ElCapitan-" + i + ".png"));

            // Ship2 textures
            // Ship3 textures

            // Generic ship textures
            GenericShipTextures.Add(new Texture("resources/gfx/ships/dummyenemy/de1.png"));

            // Enemy textures
            for (int i = 0; i < 6; i++) eMosquitoe.Add(new Texture("resources/gfx/ships/enemy/mosquitoe/mosquitoe-" + i + ".png"));
            for (int i = 5; i > 0; i--) eMosquitoe.Add(new Texture("resources/gfx/ships/enemy/mosquitoe/mosquitoe-" + i + ".png"));
        }

        public static List<Texture> GetShipTextures(int selection)
        {
            List<Texture> results = new List<Texture>();
            switch (selection)
            {
                case 0:
                    results = ElCapitanTextures;
                    break;
                case 1:
                    results = Ship2Textures;
                    break;

                case 4:
                    results = GenericShipTextures;
                    break;
                case 5:
                    results = eMosquitoe;
                    break;
            }
            return results;
        }

        public static Animation GetShipAnimation(EnemyTypes selection)
        {
            Animation result;
            switch (selection)
            {
                case EnemyTypes.Dummy:
                    result = aMosquitoe;
                    break;
                case EnemyTypes.Mosquitoe:
                    result = aMosquitoe;
                    break;
                default:
                    result = aMosquitoe;
                    break;
            }
            return result;
        }
    }

    public static class PropellersTextures
    {
        // Player propellers
        private static List<Texture> ElCapitanPropellersTextures = new List<Texture>();
        private static List<Texture> Ship2PropellersTextures = new List<Texture>();
        private static List<Texture> Ship3PropellersTextures = new List<Texture>();

        // Enemy propellers
        private static List<Texture> GenericPropellers = new List<Texture>();

        public static void InitializePropellersTextures()
        {
            // El Capitán propellers
            ElCapitanPropellersTextures.Add(new Texture("resources/gfx/propellers/elcapitan_propeller1.png"));
            ElCapitanPropellersTextures.Add(new Texture("resources/gfx/propellers/elcapitan_propeller2.png"));
            ElCapitanPropellersTextures.Add(new Texture("resources/gfx/propellers/elcapitan_propeller3.png"));
            ElCapitanPropellersTextures.Add(new Texture("resources/gfx/propellers/elcapitan_propeller4.png"));
            ElCapitanPropellersTextures.Add(new Texture("resources/gfx/propellers/elcapitan_propeller3.png"));
            ElCapitanPropellersTextures.Add(new Texture("resources/gfx/propellers/elcapitan_propeller2.png"));

            // Ship2 propellers
            // Ship3 propellers

            // Generic propellers
        }

        public static List<Texture> GetPropellerTextures(int selection)
        {
            List<Texture> textures = new List<Texture>();
            switch (selection)
            {
                case 0:
                    textures = ElCapitanPropellersTextures;
                    break;
                case 1:
                    textures = Ship2PropellersTextures;
                    break;
                case 2:
                    textures = Ship3PropellersTextures;
                    break;

                case 3:
                    textures = GenericPropellers;
                    break;
            }
            return textures;
        }
    }

    public static class ProyectilesTextures
    {
        // Deprecated
        private static List<Texture> Proyectile1Textures = new List<Texture>();
        private static List<Texture> Proyectile2Textures = new List<Texture>();
        private static List<Texture> Proyectile3Textures = new List<Texture>();
        private static List<Texture> Proyectile4Textures = new List<Texture>();
        // Deprecated

        private static List<Texture> pBlueRail = new List<Texture>();
        private static Animation aBlueRail = new Animation("Blue Rail Anim", 0.04f, pBlueRail);                     // Animation

        private static List<Texture> pRedDiamond = new List<Texture>();
        private static Animation aRedDiamond = new Animation("Red Diamond Anim", 0.04f, pRedDiamond);           // Animation
        private static List<Texture> pRedDiamondBall = new List<Texture>();
        private static Animation aRedDiamondBall = new Animation("Red Diamond Ball Anim", 0.12f, pRedDiamondBall);  // Animation

        private static List<Texture> pGreenCrast = new List<Texture>();
        private static Animation aGreenCrast = new Animation("Green Crast Anim", 0.23f, pGreenCrast);  // Animation

        private static List<Texture> pHeatTrail = Effects.GetEffectTextures(7);
        private static Animation aHeatTrail = new Animation("Heat Trail Anim", 0.23f, pHeatTrail);  // Animation

        public static void InitializeProyectilesTextures()
        {
            Proyectile1Textures.Add(new Texture("resources/gfx/proyectiles/p_proyectile1.png"));
            Proyectile2Textures.Add(new Texture("resources/gfx/proyectiles/p_proyectile2.png"));
            Proyectile3Textures.Add(new Texture("resources/gfx/proyectiles/p_proyectile3.png"));

            for (int i = 0; i < 5; i++) pBlueRail.Add(new Texture("resources/gfx/proyectiles/bluerail/pbluerail-" + i + ".png"));

            for (int i = 0; i < 7; i++) pRedDiamond.Add(new Texture("resources/gfx/proyectiles/reddiamond/preddiamond_" + i + ".png"));
            for (int i = 5; i < 0; i--) pRedDiamond.Add(new Texture("resources/gfx/proyectiles/reddiamond/preddiamond_" + i + ".png"));
            for (int i = 0; i < 7; i++) pRedDiamondBall.Add(new Texture("resources/gfx/proyectiles/reddiamond/preddiamondball_" + i + ".png"));
            for (int i = 5; i < 0; i--) pRedDiamondBall.Add(new Texture("resources/gfx/proyectiles/reddiamond/preddiamondball_" + i + ".png"));

            for (int i = 0; i < 7; i++) pGreenCrast.Add(new Texture("resources/gfx/proyectiles/greencrast/pgreencrast_" + i + ".png"));
            for (int i = 5; i < 0; i--) pGreenCrast.Add(new Texture("resources/gfx/proyectiles/greencrast/pgreencrast_" + i + ".png"));
        }
        public static List<Texture> GetProyectileTextures(int selection)
        {
            List<Texture> textures = new List<Texture>();
            switch (selection)
            {
                case 0:
                    textures = pBlueRail;
                    break;
                case 1:
                    textures = pRedDiamond;
                    break;
                case 2:
                    textures = pGreenCrast;
                    break;
                case 3:
                    textures = pHeatTrail;
                    break;
            }
            return textures;
        }
        public static Animation GetProyectileAnim(int selection)
        {
            Animation value = aBlueRail;
            switch (selection)
            {
                case 0:
                    value = aBlueRail;
                    break;
                case 1:
                    value = aRedDiamond;
                    break;
                case 2:
                    value = aRedDiamondBall;
                    break;
                case 3:
                    value = aGreenCrast;
                    break;
                case 4:
                    value = aHeatTrail;
                    break;
            }
            return value;
        }
    }

    public static class Effects
    {
        private static List<Texture> smoke1 = new List<Texture>();
        private static List<Texture> stars1 = new List<Texture>();
        private static List<Texture> shieldGreen = new List<Texture>();
        private static List<Texture> shieldRed = new List<Texture>();
        private static List<Texture> proyectileBeam = new List<Texture>();
        private static List<Texture> indicatorCircle = new List<Texture>();
        private static List<Texture> itemWaiting = new List<Texture>();
        private static List<Texture> fire1 = new List<Texture>();

        public static void InitializeEffects()
        {
            for (int i = 0; i < 5; i++)
                smoke1.Add(new Texture("resources/gfx/effect/smoke1-" + i + ".png"));
            smoke1.Add(new Texture("resources/gfx/blank.png"));

            for (int i = 0; i < 4; i++)
                stars1.Add(new Texture("resources/gfx/environment/a_var1_stars/var1_star" + i + ".png"));

            for (int i = 0; i != 6; i++)
                shieldGreen.Add(new Texture("resources/gfx/effect/shieldgreen-" + i + ".png"));
            for (int i = 5; i != 1; i--)
                shieldGreen.Add(new Texture("resources/gfx/effect/shieldgreen-" + i + ".png"));

            for (int i = 0; i != 6; i++)
                shieldRed.Add(new Texture("resources/gfx/effect/shieldred-" + i + ".png"));
            for (int i = 5; i != 1; i--)
                shieldRed.Add(new Texture("resources/gfx/effect/shieldred-" + i + ".png"));

            for (int i = 4; i != 1; i--)
                proyectileBeam.Add(new Texture("resources/gfx/effect/proyectilebeam-" + i + ".png"));
            proyectileBeam.Add(new Texture("resources/gfx/blank.png"));

            indicatorCircle.Add(new Texture("resources/gfx/effect/hp_full.png"));
            indicatorCircle.Add(new Texture("resources/gfx/effect/hp_half.png"));
            indicatorCircle.Add(new Texture("resources/gfx/effect/hp_depleted.png"));

            for (int i = 0; i < 6; i++)
                itemWaiting.Add(new Texture("resources/gfx/effect/itemeffect-white-" + i + ".png"));

            for (int i = 4; i > 0; i--)
                itemWaiting.Add(new Texture("resources/gfx/effect/itemeffect-white-" + i + ".png"));

            for (int i = 0; i < 5; i++)
                fire1.Add(new Texture("resources/gfx/effect/fire1-" + i + ".png"));
        }

        public static List<Texture> GetEffectTextures(int selection)
        {
            List<Texture> textures = new List<Texture>();
            switch (selection)
            {
                case 0:
                    textures = stars1;
                    break;
                case 1:
                    textures = smoke1;
                    break;
                case 2:
                    textures = shieldGreen;
                    break;
                case 3:
                    textures = shieldRed;
                    break;
                case 4:
                    textures = proyectileBeam;
                    break;
                case 5:
                    textures = indicatorCircle;
                    break;
                case 6:
                    textures = itemWaiting;
                    break;
                case 7:
                    textures = fire1;
                    break;
            }
            return textures;
        }
    }

    public static class UITextures
    {
        //private static List<Texture> buttonProp = new List<Texture>();
        private static List<Texture> buttonPlay = new List<Texture>();
        private static List<Texture> buttonControls = new List<Texture>();
        private static List<Texture> buttonCredits = new List<Texture>();
        private static List<Texture> buttonExit = new List<Texture>();
        private static List<Texture> numbers = new List<Texture>();
        private static List<Texture> weapbox = new List<Texture>();

        public static void InitializeUITextures()
        {
            //buttonProp.Add(new Texture("resources/gfx/ui/button_propUnselected.png"));
            //buttonProp.Add(new Texture("resources/gfx/ui/button_propSelected.png"));

            buttonPlay.Add(new Texture("resources/gfx/ui/PlayButton.png"));
            buttonPlay.Add(new Texture("resources/gfx/ui/PlayButtonSelect.png"));

            buttonControls.Add(new Texture("resources/gfx/ui/ControlsButton.jpg"));
            buttonControls.Add(new Texture("resources/gfx/ui/ControlsButtonSelect.jpg"));

            buttonCredits.Add(new Texture("resources/gfx/ui/CreditsButton.png"));
            buttonCredits.Add(new Texture("resources/gfx/ui/CreditsButtonSelect.jpg"));

            buttonExit.Add(new Texture("resources/gfx/ui/ExitButton.jpg"));
            buttonExit.Add(new Texture("resources/gfx/ui/ExitButtonSelect.jpg"));

            for (int i = 0; i < 10; i++) numbers.Add(new Texture("resources/gfx/ui/numbers/" + i + ".png"));
            numbers.Add(new Texture("resources/gfx/ui/numbers/dot.png"));

            weapbox.Add(new Texture("resources/gfx/ui/weapbox/weapbox-0.png"));
            weapbox.Add(new Texture("resources/gfx/ui/weapbox/weapbox-0.png"));
            weapbox.Add(new Texture("resources/gfx/ui/weapbox/weapbox-0.png"));
            weapbox.Add(new Texture("resources/gfx/ui/weapbox/weapbox-0.png"));
            weapbox.Add(new Texture("resources/gfx/ui/weapbox/weapbox-0.png"));
            for (int i = 0; i < 11; i++) weapbox.Add(new Texture("resources/gfx/ui/weapbox/weapbox-" + i + ".png"));
            for (int i = 10; i > 0; i--) weapbox.Add(new Texture("resources/gfx/ui/weapbox/weapbox-" + i + ".png"));
        }

        public static List<Texture> GetUITextures(int selection)
        {
            List<Texture> textures = new List<Texture>();
            switch (selection)
            {
                /*case 0:
                    textures = buttonProp;
                    break;*/
                case 0:
                    textures = buttonPlay;
                    break;
                case 1:
                    textures = buttonControls;
                    break;
                case 2:
                    textures = buttonCredits;
                    break;
                case 3:
                    textures = buttonExit;
                    break;
                case 4:
                    textures = numbers;
                    break;
                case 5:
                    textures = weapbox;
                    break;
            }
            return textures;
        }

    }

    public static class ItemsTextures
    {
        private static Texture wrench = new Texture("resources/gfx/items/wrench.png");
        private static Texture shield = new Texture("resources/gfx/items/shield.png");
        private static Texture special = new Texture("resources/gfx/items/special.png");
        private static Texture blueRail = new Texture("resources/gfx/proyectiles/bluerail/pbluerail-0.png");
        private static Texture redDiamond = new Texture("resources/gfx/proyectiles/reddiamond/preddiamond_0.png");
        private static Texture greenCrast = new Texture("resources/gfx/proyectiles/greencrast/pgreencrast_0.png");
        private static Texture heatTrail = new Texture("resources/gfx/effect/fire1-0.png");

        public static Texture GetItemTexture(int selection)
        {
            Texture value = blueRail;

            switch (selection)
            {
                case 0:
                    value = wrench;
                    break;
                case 1:
                    value = shield;
                    break;
                case 2:
                    value = special;
                    break;
                case 3:
                    value = blueRail;
                    break;
                case 4:
                    value = redDiamond;
                    break;
                case 5:
                    value = greenCrast;
                    break;
                case 6:
                    value = heatTrail;
                    break;
            }
            return value;
        }
    }

    public static class OtherTextures
    {
        private static Texture collisionDot = new Texture("resources/gfx/collisiondot.png");
        private static Texture dummyship = new Texture("resources/gfx/ships/dummyship_128.png");
        
        public static Texture GetOtherTexture(int selection)
        {
            Texture value;
            switch (selection)
            {
                default:
                    value = collisionDot;
                    break;
                case 0:
                    value = collisionDot;
                    break;
                case 1:
                    value = dummyship;
                    break;
            }

            return value;
        }
    }
}