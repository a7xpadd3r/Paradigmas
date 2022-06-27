using System;

namespace Game
{
    public class Program
    {
        private static float deltaTime;
        protected static DateTime lastFrameTime = DateTime.Now;

        static void Main(string[] args)
        {
            Engine.Initialize();

            // Start stuff
            GameplayManager.Instance.InitializeGame();

            while (true)
            {
                Engine.Clear();
                DeltaTime();
                GameplayManager.Instance.ManagerUpdate();

                //Engine.Draw("resources/gfx/30x30.png", 500, 500, 1, 1, 0);
                //Engine.Draw("resources/gfx/30x30.png", 500, 500, 1, 1, 90f,15f,15f);

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