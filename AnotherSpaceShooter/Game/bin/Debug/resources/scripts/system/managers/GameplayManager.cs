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

        public void InitializeGame()
        {
            Textures.InitializeTextures();

            // Initialize menues
            mMain = new MainMenu(); // Interface?
            AllStar();
            ChangeScene(Scenes.MainMenu); // Interface?
            OnPlayerDeath += PlayerDeath;
        }

        public void PlayerDeath()
        {

            MainMenu.changeScene = 6;
            goManager.WipeGameObjects();
            CollisionManager.WipeColliders();

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
            LifesOnScreen();

            StarsManager.UpdateFront();

            currentScene.Update(); // Interface?
        }

        private static void LifesOnScreen()
        {
            /*
            if (playerShip != null && currentLifes > 0)
            {
                for (int i = 0; i < currentLifes; i++)
                {
                    if (i == currentLifes - 1)
                        Engine.Draw(Player.GetIntegrityTexture(), lifesPositionsOnScreen[i], lifesPositionsOnScreen[5], 0.4f, 0.4f);
                    else if (i != currentLifes) 
                        Engine.Draw(playerShip.ShipAnim().GetFrameTexture(playerShip.ShipAnim().AnimationLongitude() - 1), lifesPositionsOnScreen[i], lifesPositionsOnScreen[5], 0.4f, 0.4f);
                }
            }*/
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