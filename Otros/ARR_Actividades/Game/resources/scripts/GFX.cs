using System;
using System.Collections.Generic;

namespace Game
{
    public class GFX
    {
        // Animations
        private static Animation a_smoke1;

        // Textures
        private static List<Texture> t_smoke1 = new List<Texture>();

        public static Animation GetAnimation(string whatAnimation)
        {
            Animation whatToReturn = a_smoke1;
            switch (whatAnimation)
            {
                case "smoke1":
                    whatToReturn = a_smoke1;
                    Console.WriteLine("GFX --> '{0}' animation wanted.");
                    break;
            }
            return whatToReturn;
        }

        public static List<Texture> GetTextures(string whatList)
        {
            List<Texture> whatToReturn = new List<Texture>();
            switch (whatList)
            {
                case "smoke1":
                    whatToReturn = t_smoke1;
                    break;
            }
            return whatToReturn;
        }

        private static void CreateAnimations()
        {
            a_smoke1 = new Animation("smoke1", 0.5f, t_smoke1, false,false);
            Console.WriteLine("GFX --> Animations created.");
        }

        public static void GetEffectsTextures()
        {
            Console.WriteLine("GFX --> Get some textures for effects.");

            for (int i = 0; i < 5; i++)
            {
                t_smoke1.Add(new Texture("resources/gfx/effect/smoke1-" + i + ".png"));
                Console.WriteLine("resources/gfx/effect/smoke1-" + i + ".png");
            }
            t_smoke1.Add(new Texture("resources/gfx/blank.png"));

            Console.WriteLine("GFX --> Finished getting some textures for effects.");
            CreateAnimations();
        }

    }
}