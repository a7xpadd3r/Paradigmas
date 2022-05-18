using System.Collections.Generic;
using Game.resources.scripts;
using System;


namespace Game
{
    public class Program
    {
        private static float deltaTime;
        protected static DateTime lastFrameTime = DateTime.Now;
        private static List<GFX_GenericStar> stars1 = new List<GFX_GenericStar>();
        private static List<GFX_GenericStar> stars2 = new List<GFX_GenericStar>();

        static Random rnd = new Random();

        static int RndInt()
        {
            int randomreturn = rnd.Next(0, 500);
            return randomreturn;
        }

        static int RndSpeed()
        {
            int randomreturn = rnd.Next(-200, 200);
            return randomreturn;
        }

        public static int RndAnother()
        {
            int randomreturn = rnd.Next(-200, 2180);
            return randomreturn;
        }

        static void Main(string[] args)
        {
            GFX.GetEffectsTextures();
            Engine.Initialize();

            // Get stuff
            
            LevelsGFX.GatherAnimations();
            PlayerConfig.InitializePlayer();
            PlayerTextures.SetTextureFrames();
            SFX.PlayMusic();
            Player.PlayerAnimStart();

            // Testing some stars
            stars1.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars1.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars1.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars1.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars1.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars1.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars1.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars1.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars1.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars1.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars1.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));
            stars2.Add(new GFX_GenericStar(RndSpeed(), RndInt()));


            while (true)
            {
                DeltaTime();
                Update();
                Draw();
            }
        }

        static void Update()
        {
            Weapons.PlayerWeaponsManagment();
            Player.PlayerMovement();
            GameObjectManager.Update();
            CollisionManager.Update();
            Weapons.Update();
        }

        static void Draw()
        {
            Engine.Clear();

            // Background stars
            for (int i = 0; i < stars1.Count; i++)
                stars1[i].Update();

            DummyEnemy.Draw();
            Player.PlayerDraw();
            Weapons.BulletsManagment();
            GameplayManager.Update();

            // Foreground stars
            for (int i = 0; i < stars2.Count; i++)
                stars2[i].Update();

            

            Engine.Show();
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