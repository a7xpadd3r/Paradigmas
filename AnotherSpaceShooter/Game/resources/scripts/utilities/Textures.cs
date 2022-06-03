using System;
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
                case 2:
                    results = Ship3Textures;
                    break;

                case 4:
                    results = GenericShipTextures;
                    break;
            }
            return results;
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
        private static List<Texture> Proyectile1Textures = new List<Texture>();
        private static List<Texture> Proyectile2Textures = new List<Texture>();
        private static List<Texture> Proyectile3Textures = new List<Texture>();
        private static List<Texture> Proyectile4Textures = new List<Texture>();

        public static void InitializeProyectilesTextures()
        {
            Proyectile1Textures.Add(new Texture("resources/gfx/proyectiles/p_proyectile1.png"));
            Proyectile2Textures.Add(new Texture("resources/gfx/proyectiles/p_proyectile2.png"));
            Proyectile3Textures.Add(new Texture("resources/gfx/proyectiles/p_proyectile3.png"));
            Proyectile4Textures.Add(new Texture("resources/gfx/proyectiles/e_proyectile1.png"));
        }

        public static List<Texture> GetProyectileTextures(int selection)
        {
            List<Texture> textures = new List<Texture>();
            switch (selection)
            {
                case 0:
                    textures = Proyectile1Textures;
                    break;
                case 1:
                    textures = Proyectile2Textures;
                    break;
                case 2:
                    textures = Proyectile3Textures;
                    break;
                case 3:
                    textures = Proyectile4Textures;
                    break;
            }
            return textures;
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

            for (int i = 0; i < 10; i++)
            {
                numbers.Add(new Texture("resources/gfx/ui/numbers/" + i + ".png"));
            }
            numbers.Add(new Texture("resources/gfx/ui/numbers/dot.png"));
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
            }
            return textures;
        }

    }

    public static class ItemsTextures
    {
        private static Texture wrench = new Texture("resources/gfx/items/wrench.png");
        private static Texture shield = new Texture("resources/gfx/items/shield.png");
        private static Texture special = new Texture("resources/gfx/items/special.png");
        private static Texture blueRail = new Texture("resources/gfx/proyectiles/p_proyectile1.png");
        private static Texture redDiamond = new Texture("resources/gfx/proyectiles/p_proyectile2.png");
        private static Texture greenCrast = new Texture("resources/gfx/proyectiles/p_proyectile3.png");

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
            }

            return value;
        }

    }
}