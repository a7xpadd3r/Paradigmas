using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum Scenes
    {
        MainMenu,
        Credits,
        Level1,
        Level2,
        LevelBoss,
        GameOver,
        Win,
        Controls

    }
    public class GameManager
    {
        private static GameManager instance;

        private IScene currentScene;

        private MainMenu mainMenu;

        private CreditsScene credits;

        private Level1 level1;
        private Level2 level2;

        private LevelBoss levelBoss;


        private EndGame winScreen;

        private EndGame gameOverScreen;

        private Controls controlsScreen;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }

                return instance;
            }
        }

        private GameManager()
        {

        }

        public void Initialize()
        {
            mainMenu = new MainMenu("Textures/Screens/MenuPrincipal.png");
            controlsScreen = new Controls("Textures/Screens/Controls.png", 5f);
            credits = new CreditsScene("Textures/Screens/Credits.png", 5f);
            level1 = new Level1();
            level2 = new Level2();
            levelBoss = new LevelBoss();       
            level1.Initialize();         
            level2.Initialize();
            levelBoss.Initialize();
            gameOverScreen = new EndGame("Textures/Screens/GameOverScene.png", 5f);
            winScreen = new EndGame("Textures/Screens/WinScene.png", 5f);
            ChangeScene(Scenes.MainMenu);
        }

        public void Update()
        {
            currentScene.Update();
            GameObjectManager.Update();
        }

        public void Render()
        {
            currentScene.Render();
            GameObjectManager.Render();
        }

        public void ChangeScene(Scenes newScene)
        {
            if (currentScene != null)
                currentScene.Finish();

            GameObjectManager.ClearGameObjects();

            switch (newScene)
            {
                case Scenes.MainMenu:
                    currentScene = mainMenu;
                    break;
                case Scenes.Credits:
                    currentScene = credits;
                    break;
                case Scenes.Level1:
                    currentScene = level1;
                    break;
                case Scenes.Level2:
                    currentScene = level2;
                    break;
                case Scenes.LevelBoss:
                    currentScene = levelBoss;
                    break;
                case Scenes.GameOver:
                    currentScene = gameOverScreen;
                    break;
                case Scenes.Win:
                    currentScene = winScreen;
                    break;
                case Scenes.Controls:
                    currentScene = controlsScreen;
                    break;
                default:
                    break;
            }
            currentScene.Initialize();
        }

        public void ExitGame()
        {
            Environment.Exit(1);
        }
    }
}

