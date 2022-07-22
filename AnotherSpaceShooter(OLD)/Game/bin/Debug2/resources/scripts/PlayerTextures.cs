using System;
using System.Collections.Generic;

namespace Game
{
    public class PlayerTextures // Main calls this to set textures
    {
        private static List<Texture> ShipSkin = new List<Texture>();
        private static List<Texture> ShipPropellers = new List<Texture>();
        private static Texture pDummyText = new Texture("resources/ships/nullship.png");

        private static List<Texture> ElCapitan = new List<Texture>();
        private static List<Texture> ElCapitanPropellers = new List<Texture>();

        public static void SetTextureFrames()
        {
            Console.WriteLine("Get animations event.");
            // El Capitán textures
            for (int i = 1; i < 5; i++)
                ElCapitan.Add(new Texture("resources/ships/elcapitan/ElCapitan-" + i + ".png"));

            ElCapitanPropellers.Add(new Texture("resources/ships/elcapitan/propeller/elcapitan_propeller1.png"));
            ElCapitanPropellers.Add(new Texture("resources/ships/elcapitan/propeller/elcapitan_propeller2.png"));
            ElCapitanPropellers.Add(new Texture("resources/ships/elcapitan/propeller/elcapitan_propeller3.png"));
            ElCapitanPropellers.Add(new Texture("resources/ships/elcapitan/propeller/elcapitan_propeller4.png"));
            ElCapitanPropellers.Add(new Texture("resources/ships/elcapitan/propeller/elcapitan_propeller3.png"));
            ElCapitanPropellers.Add(new Texture("resources/ships/elcapitan/propeller/elcapitan_propeller2.png"));

            // Set the selected skin
            switch (PlayerConfig.skin)
            {
                case 1:
                    ShipSkin = ElCapitan;
                    ShipPropellers = ElCapitanPropellers;
                    break;
            }
        }

        public static List<Texture> SkinSet()
        {
            return ShipSkin;
        }

        public static List<Texture> PropellerSet()
        {
            return ShipPropellers;
        }

        public static Texture PlayerDummyTexture()
        {
            return pDummyText;
        }
    }
}
