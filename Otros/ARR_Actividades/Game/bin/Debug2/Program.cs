using System.Collections.Generic;
using Game.resources.scripts;
using System;


namespace Game
{
    public class Program
    {
        public static float deltaTime;
        protected static DateTime lastFrameTime = DateTime.Now;

        static void Main(string[] args)
        {
            Engine.Initialize();

            PlayerTextures.SetTextureFrames();
            SFX.PlayMusic();
            Player.PlayerAnimStart();

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
            Weapons.Update();
        }

        static void Draw()
        {
            Engine.Clear();

            Player.PlayerDraw();
            Weapons.BulletsManagment();
            GameplayManager.Update();

            Engine.Show();
        }

        static void DeltaTime()
        {
            TimeSpan deltaSpan = DateTime.Now - lastFrameTime;
            deltaTime = (float)deltaSpan.TotalSeconds;
            lastFrameTime = DateTime.Now;
        }
    }



}