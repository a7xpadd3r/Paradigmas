using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum Ships { ElCapitan }
    public enum Background { Stars, Planets }
    public class Textures
    {
        // Textures
        private Texture notexture = new Texture("resources/gfx/notexture.png");

        // Lists
        private List<Texture> notexturelist = new List<Texture>();
        private List<Texture> stars = new List<Texture>();
        private List<Texture> planets = new List<Texture>();

        public void InitializeTextures()
        {
            // Create textures list
            notexturelist.Add(new Texture("resources/gfx/notexture.png"));
            
            // Stars
            for (int i = 0; i < 4; i++) stars.Add(new Texture("resources/gfx/background/star/var1_star" + i + ".png"));
            planets.Add(new Texture("resources/gfx/background/deco1.png"));
        }

        public Texture GetShipTexture(Ships whichone)
        {
            Texture value = notexture;
            switch (whichone)
            {
                case Ships.ElCapitan:
                    value = new Texture("resources/gfx/ship/elcapitan/ElCapitan-3.png");
                    break;

                default:
                    value = notexture;
                    break;
            }
            return value;
        }

        public List<Texture> GetBackgroundSprite(Background wichone)
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
