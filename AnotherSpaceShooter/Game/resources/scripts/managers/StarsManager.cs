using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class StarsManager
    {
        public static bool debug = false;
        private protected static List<Star> CurrentStarsBack { get; } = new List<Star>();
        private protected static List<Star> CurrentStarsFront { get; } = new List<Star>();
        public static List<Star> GetAllBackStars => CurrentStarsBack;
        public static List<Star> GetAllFrontStars => CurrentStarsFront;
        public static Vector2 PlayerPos = new Vector2(0, 0);

        public static void UpdateBack()
        {
            for (int i = 0; i < CurrentStarsBack.Count; i++)
            {
                if (CurrentStarsBack[i].Active)
                {
                    Star star = CurrentStarsBack[i];
                    star.Update();
                }
            }
        }

        public static void UpdateFront()
        {
            for (int i = 0; i < CurrentStarsFront.Count; i++)
            {
                if (CurrentStarsFront[i].Active)
                {
                    Star star = CurrentStarsFront[i];
                    star.Update();
                }
            }
        }

        public static void AddBackStar(Star newStar)
        {
            // Check if exists
            if (CurrentStarsBack.Contains(newStar)) return;
            CurrentStarsBack.Add(newStar);
            if (debug) Console.WriteLine("StarsManager --> Nueva estrella agregada c:.");
        }

        public static void AddFrontStar(Star newStar)
        {
            // Check if exists
            if (CurrentStarsFront.Contains(newStar)) return;
            CurrentStarsFront.Add(newStar);
            if (debug) Console.WriteLine("StarsManager --> Nueva estrella agregada c:.");
        }

        public static void RemoveBackStar(Star removeThis)
        {
            // Check if exists
            if (!CurrentStarsBack.Contains(removeThis)) return;
            if (debug) Console.WriteLine("StarsManager --> Estrella removida :c.");
            CurrentStarsBack.Remove(removeThis);
        }

        public static void RemoveFrontStar(Star removeThis)
        {
            // Check if exists
            if (!CurrentStarsFront.Contains(removeThis)) return;
            if (debug) Console.WriteLine("StarsManager --> Estrella removida :c.");
            CurrentStarsFront.Remove(removeThis);
        }

        public static void WipeAllStars()
        {
            CurrentStarsBack.Clear();
            CurrentStarsFront.Clear();
            Console.WriteLine("StarsManager --> Estrellas limpiadas :'c.");
        }
    }
}
