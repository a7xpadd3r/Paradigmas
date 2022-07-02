using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum ShipsAnimations { ElCapitan }
    public enum ShipPropeller { Red }
    public enum Background { Stars, Planets }
    public class Textures
    {
        // Textures
        private static Texture notexture = new Texture("resources/gfx/notexture.png");

        // Lists
        private static List<Texture> notexturelist = new List<Texture>();

        private static List<Texture> elcapitan = new List<Texture>();

        private static List<Texture> propellersset1 = new List<Texture>();

        private static List<Texture> stars = new List<Texture>();
        private static List<Texture> planets = new List<Texture>();

        public void InitializeTextures()
        {
            // Create textures list
            notexturelist.Add(new Texture("resources/gfx/notexture.png"));

            // Ships
            elcapitan.Add(new Texture("resources/gfx/ship/elcapitan/ElCapitan-3.png"));

            // Stars
            for (int i = 0; i < 4; i++) stars.Add(new Texture("resources/gfx/background/star/var1_star" + i + ".png"));
            planets.Add(new Texture("resources/gfx/background/deco1.png"));
        }

        public static Animation GetShipAnimation(ShipsAnimations whichone)
        {
            Animation value = new Animation("null", 0, notexturelist, false, true);
            switch (whichone)
            {
                case ShipsAnimations.ElCapitan:
                    value = new Animation("ElCapitanAnim", 0, elcapitan, false, true);
                    break;

                default:
                    value = new Animation("null", 0, notexturelist, false, true);
                    break;
            }
            return value;
        }

        public static Animation GetPropellerAnimation(ShipPropeller whichone)
        {
            Animation value = new Animation("null", 0, notexturelist, false, true);

            switch (whichone)
            {
                case ShipPropeller.Red:
                    break;

                default:
                    value = new Animation("null", 0, notexturelist, false, true);
                    break;
            }

            return value;
        }

        public static List<Texture> GetBackgroundSprite(Background wichone)
        {
            List<Texture> value = notexturelist;
            switch (wichone)
            {
                case Background.Stars:
                    value = stars;
                    break;
                case Background.Planets:
                    value = planets;
                    break;

                default:
                    value = notexturelist;
                    break;
            }
            return value;
        }
    }
}
