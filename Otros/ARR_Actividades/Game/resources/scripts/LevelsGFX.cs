using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.resources.scripts
{
    struct AnimationsPack
    {
        public static Animation var1_stars;
        public static List<Texture> var1_starsTextures = new List<Texture>();
    }

    public class LevelsGFX
    {
        static Random rndTexture = new Random();
        public static void GatherAnimations()
        {
            for (int i = 0; i < 4; i++)
                AnimationsPack.var1_starsTextures.Add(new Texture("resources/gfx/environment/a_var1_stars/var1_star" + i + ".png"));

        }

        public static Texture GetRandomStarFrame()
        {
            int selection = rndTexture.Next(0, AnimationsPack.var1_starsTextures.Count);
            Texture value = AnimationsPack.var1_starsTextures[selection];
            return value;
        }
    }

}