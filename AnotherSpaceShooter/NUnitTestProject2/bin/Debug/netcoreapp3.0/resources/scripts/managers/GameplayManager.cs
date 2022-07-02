using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;

namespace Game
{
    public enum Scenes { MainMenu, Level1, Defeat, Victory }
    public class GameplayManager
    {
        private static GameplayManager instance;
        private InterfaceScenes currentScene = null; // Interface?

        private MainMenu mMain;
        //private Player thePlayer = new Player(); Not needed anymore.

        // GameObject manager
        public GameObjectManager goManager = new GameObjectManager();
        // Others
        public EffectsManager eManager = new EffectsManager();
        

        public static GameplayManager Instance
        {
            get { if (instance == null) instance = new GameplayManager(); return instance; }
        }

        private static int currentLifes = 3;
        private readonly static float[] lifesPositionsOnScreen = { 30, 90, 150, 210, 270, 50 }; // Positions in X - X - X and Y 

        public static Action OnPlayerDeath;
        public static Action OnPlayerWin;

        public void InitializeGame()
        {
            Textures.InitializeTextures();

            // Initialize menues
            mMain = new MainMenu(); // Interface?
            AllStar();
            ChangeScene(Scenes.MainMenu); // Interface?
            OnPlayerDeath += PlayerDeath;
            OnPlayerWin += PlayerWin;
        }

        public void PlayerDeath()
        {
            goManager.WipeGameObjects();
            MainMenu.changeScene = 6;
        }

        public void PlayerWin()
        {
            goManager.WipeGameObjects();
            MainMenu.changeScene = 5;
        }

        // Interface?
        public void ChangeScene(Scenes toWhat) // Interface?
        {
            if (currentScene != null) currentScene.Finish();

            currentScene = mMain;
            currentScene.Start();
        }

        public void InitializeGameplayManager()
        {
            //playerShip = thePlayer.GetShip();
            //thePlayer.OnShipDestroyed += LifeLost;
        }

        private protected static void LifeLost()
        {
            --currentLifes;
            if (currentLifes == 0) OnPlayerDeath?.Invoke();

        }

        public void ManagerUpdate()
        {
            StarsManager.UpdateBack();

            ManagerLevel1.Update();
            goManager.Update(); //  Needs to be changed for non-static stuff
            eManager.Update();
            CollisionManager.Update();

            StarsManager.UpdateFront();

            currentScene.Update(); // Interface?
        }

        private static void AllStar()
        {
            for (int i = 0; i < 30; i++)
                StarsManager.AddBackStar(new Star());
            for (int i = 0; i < 30; i++)
                StarsManager.AddFrontStar(new Star());
        }

        public void ExitGame()
        {
            Environment.Exit(1);
        }

    }
}