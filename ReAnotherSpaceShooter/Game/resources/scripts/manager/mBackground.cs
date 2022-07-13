using System;
using System.Collections.Generic;

namespace Game
{
    public static class mBackgroud
    {
        private static List<Star> CurrentStarsBack { get; } = new List<Star>();
        private static List<Star> CurrentStarsFront { get; } = new List<Star>();

        public static void UpdateBack(float delta)
        {
            for (int i = 0; i < CurrentStarsBack.Count; i++)
            {
                if (CurrentStarsBack[i].Active)
                {
                    Star star = CurrentStarsBack[i];
                    star.Update(delta);
                }
            }
        }
        public static void UpdateFront(float delta)
        {
            for (int i = 0; i < CurrentStarsFront.Count; i++)
            {
                if (CurrentStarsFront[i].Active)
                {
                    Star star = CurrentStarsFront[i];
                    star.Update(delta);
                }
            }
        }
        public static void AddBackStar(Star newStar)
        {
            // Check if exists
            if (CurrentStarsBack.Contains(newStar)) return;
            CurrentStarsBack.Add(newStar);
        }

        public static void AddFrontStar(Star newStar)
        {
            // Check if exists
            if (CurrentStarsFront.Contains(newStar)) return;
            CurrentStarsFront.Add(newStar);
        }
    }
}
