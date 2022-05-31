

using System;
using System.Numerics;

namespace Game
{
    class MainMenu : InterfaceScenes
    {

        //buttons
        private Button start;
        private Vector2 startPos = new Vector2(830, 550);

        private Button controls;
        private Vector2 controlsPos = new Vector2(830, 650);

        private Button credits;
        private Vector2 creditsPos = new Vector2(830, 750);

        private Button exit;
        private Vector2 exitPos = new Vector2(830, 850);

        //private Button playbutton;
        //private Vector2 playPos = new Vector2(300, 600);

        private float delay = 0.2f;
        private float currentDelay = 0;
        private bool allowInput = true;
        private Button selected;
        private Action KeyInput;
        
        public static int ChangeScene => changeScene;
        public static int changeScene;
        int BackScene;

        // This stuff needs to be moved
        ManagerLevel1 theManagerLevel1 = new ManagerLevel1();

        public MainMenu()
        {

        }

        public void Start()
        {
            start = new Button(startPos, 1, UI.GetUITextures(0));
            controls = new Button(controlsPos, 1, UI.GetUITextures(1));
            credits = new Button(creditsPos, 1, UI.GetUITextures(2));
            exit = new Button(exitPos, 1, UI.GetUITextures(3));

            start.AssignButtons(exit, controls);
            controls.AssignButtons(start, credits);
            credits.AssignButtons(controls, exit);
            exit.AssignButtons(credits, start);

            selected = start;
            KeyInput += ResetInputDelay;
            //DummyEnemy.OnDead += EnemyDeath; Needs to be reworked

            selected = start;
            selected.Selected();
        }

        private void ResetInputDelay()
        {
            currentDelay = 0;
            allowInput = false;
        }

        public void MainMenuKeys()
        {

            if (!allowInput) currentDelay += Program.GetDeltaTime();
            if (currentDelay >= delay && !allowInput)
            {
                currentDelay = 0;
                allowInput = true;
            }

            if (Engine.GetKey(Keys.W) && allowInput)
            {
                allowInput = false;
                Selection(selected.PreviousButton);
            }

            if (Engine.GetKey(Keys.S) && allowInput)
            {
                allowInput = false;
                Selection(selected.NextButton);
            }

            if (Engine.GetKey(Keys.RETURN) && allowInput)
            {
                allowInput = false;
                SelectionSelect();
            }

        }

        public void EnemyDeath()
        {
            changeScene = 5;
        }

        public void SceneKey()
        {

            if (!allowInput) currentDelay += Program.GetDeltaTime();
            if (currentDelay >= delay && !allowInput)
            {
                currentDelay = 0;
                allowInput = true;
            }

            if (Engine.GetKey(Keys.ESCAPE) && allowInput)
            {
                allowInput = false;
                BackScene = 1;
            }
        }

        public void Update()
        {
            switch (changeScene)
            {
                case 0:
                    MainMenuKeys();
                    Engine.Draw("resources/gfx/scenes/MainMenu.png");
                    start.Update(); controls.Update(); credits.Update(); exit.Update();
                    break;
                case 1:     
                    break;
                case 2:
                    SceneKey();
                    if (BackScene!=1)
                    {
                        Controls.DrawScene();
                    }
                    else
                    {
                        changeScene = 0;
                        BackScene = 0;
                    }
                    
                    break;
                case 3:
                    SceneKey();
                    if (BackScene != 1)
                    {
                        Credits.DrawScene();
                    }
                    else
                    {
                        changeScene = 0;
                        BackScene = 0;
                    }
                    break;
                case 4:
                    GameplayManager.Instance.ExitGame();
                    break;
                case 5:
                    SceneKey();
                    if (BackScene != 1)
                    {
                        Engine.Draw("resources/gfx/scenes/YouWin.png");
                    }
                    else
                    {
                        changeScene = 0;
                        BackScene = 0;
                    }
                    break;
                case 6:
                    SceneKey();
                    if (BackScene != 1)
                    {
                        Engine.Draw("resources/gfx/scenes/GameOver.png");
                    }
                    else
                    {
                        changeScene = 0;
                        BackScene = 0;
                    }
                    break;

            }
        }
        public void Render()
        {
            start.Update();
        }


        public void SelectionSelect()
        {
            if (selected != null)
            {
                if (selected == start) {
                    theManagerLevel1.Gameplay();
                    changeScene = 1;
                }
                if (selected == controls) {
                    changeScene = 2;                   
                }

                if (selected == credits)
                {
                    changeScene = 3;
                }

                if (selected == exit) {
                    changeScene = 4;
                }
            }
        }

        public void Selection(Button current)
        {
            if (selected != null)
            {
                selected.Unselected();
            }

            selected = current;
            selected.Selected();
        }

        public void Finish()
        {
            Console.WriteLine("Clear everything?");
        }

    }
}
