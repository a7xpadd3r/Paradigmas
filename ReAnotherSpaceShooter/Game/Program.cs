using System;
using System.Collections.Generic;

namespace Game
{
    public class Program
    {
        private static float deltaTime;
        protected static DateTime lastFrameTime = DateTime.Now;

        static void Main(string[] args)
        {
            Engine.Initialize();

            while(true)
            {
                Engine.Clear();
                DeltaTime();
                GameManager.Update();
                Engine.Show();
            }
        }

        private static void DeltaTime()
        {
            TimeSpan deltaSpan = DateTime.Now - lastFrameTime;
            deltaTime = (float)deltaSpan.TotalSeconds;
            lastFrameTime = DateTime.Now;
        }

        public static float GetDeltaTime()
        {
            return deltaTime;
        }
    }
}