using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class sMainMenu : iScenes
    {
        // Buttons
        private Button start;
        private Vector2 startPos = new Vector2(830, 550);
        private Button controls;
        private Vector2 controlsPos = new Vector2(830, 650);
        private Button credits;
        private Vector2 creditsPos = new Vector2(830, 750);
        private Button exit;
        private Vector2 exitPos = new Vector2(830, 850);

        // Input
        private float delay = 0.25f;
        private float currentDelay = 0;
        private bool allowInput = true;
        private Button selected;
        private Action KeyInput;

        // Scene
        public static int ChangeScene => changeScene;
        public static int changeScene;
        int BackScene;

        // Others
        public Action OnStartGame;
        public Action<int> OnShipChange;
        private ShipData currentshipdata;
        private float t = 0;

        // Ship selection & textures
        private int currentship = 0;
        private readonly List<Texture> elcapitan = Textures.GetUITextures(UITextures.ElCapitanShip);
        private readonly List<Texture> sonic = Textures.GetUITextures(UITextures.SonicShip);
        private readonly List<Texture> skullflower = Textures.GetUITextures(UITextures.SkullFlowerShip);
        private readonly Animation redpropeller = Textures.GetPropellerAnimation(ShipPropeller.Red);
        private readonly Animation bluepropeller = Textures.GetPropellerAnimation(ShipPropeller.Blue);

        public void Start()
        {
            start = new Button(startPos, 1, Textures.GetUITextures(UITextures.PlayButton));
            controls = new Button(controlsPos, 1, Textures.GetUITextures(UITextures.ControlsButton));
            credits = new Button(creditsPos, 1, Textures.GetUITextures(UITextures.CreditsButton));
            exit = new Button(exitPos, 1, Textures.GetUITextures(UITextures.ExitButton));

            start.AssignButtons(exit, controls);
            controls.AssignButtons(start, credits);
            credits.AssignButtons(controls, exit);
            exit.AssignButtons(credits, start);

            selected = start;
            selected.Selected();
            KeyInput += ResetInputDelay;

            allowInput = false;
            selected = start;
            currentDelay = 0;
            changeScene = 0;
            allowInput = false;
        }
        private void ResetInputDelay()
        {
            currentDelay = 0;
            allowInput = false;
        }
        public void MainMenuKeys()
        {
            if (!allowInput) currentDelay += t;
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

            if (Engine.GetKey(Keys.A) && allowInput && changeScene == 0)
            {
                allowInput = false;
                if (currentship == 0) currentship = 2; else currentship--;
            }

            if (Engine.GetKey(Keys.D) && allowInput && changeScene == 0)
            {
                allowInput = false;
                if (currentship == 2) currentship = 0; else currentship++;
            }
        }
        public void SceneKey()
        {
            if (!allowInput) currentDelay += t;
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
        public void Update(float delta)
        {
            t = delta;
            switch (changeScene)
            {
                case 0:
                    MainMenuKeys();
                    Engine.Draw(Textures.splashmainmenu);

                    switch (currentship)
                    {
                        case 0:
                            {
                                redpropeller.Update();
                                Renderer.DrawCenter(elcapitan[0], new Vector2(290, 830), new Vector2(0.65f, 0.65f));
                                Renderer.DrawCenter(redpropeller.CurrentTexture, new Vector2(238, 827), new Vector2(1f, 1f));
                                if (currentshipdata != ShipsProperties.ElCapitan) { currentshipdata = ShipsProperties.ElCapitan; OnShipChange?.Invoke(0); }
                            } break;

                        case 1:
                            {
                                bluepropeller.Update();
                                Renderer.DrawCenter(sonic[0], new Vector2(290, 830), new Vector2(0.65f, 0.65f));
                                Renderer.DrawCenter(bluepropeller.CurrentTexture, new Vector2(200, 827), new Vector2(1f, 1f));
                                Renderer.DrawCenter(bluepropeller.CurrentTexture, new Vector2(276, 827), new Vector2(1f, 1f));
                                if (currentshipdata != ShipsProperties.SonicShip) { currentshipdata = ShipsProperties.SonicShip; OnShipChange?.Invoke(1); }
                            } break;
                        case 2:
                            {   
                                redpropeller.Update(); bluepropeller.Update();
                                Renderer.DrawCenter(skullflower[0], new Vector2(290, 830), new Vector2(0.65f, 0.65f));
                                Renderer.DrawCenter(bluepropeller.CurrentTexture, new Vector2(200, 840), new Vector2(1f, 1f));
                                Renderer.DrawCenter(redpropeller.CurrentTexture, new Vector2(238, 840), new Vector2(1f, 1f));
                                Renderer.DrawCenter(bluepropeller.CurrentTexture, new Vector2(276, 840), new Vector2(1f, 1f));
                                if (currentshipdata != ShipsProperties.SkullFlower) { currentshipdata = ShipsProperties.SkullFlower; OnShipChange?.Invoke(2); }
                            } break;
                    }

                    start.Update(); controls.Update(); credits.Update(); exit.Update();
                    break;
                case 1: break;
                case 2:
                    SceneKey();
                    if (BackScene != 1) Engine.Draw(Textures.splashcontrols);
                    else { changeScene = 0; BackScene = 0;} break;
                case 3:
                    SceneKey();
                    if (BackScene != 1) Engine.Draw(Textures.splashcredits);
                    else { changeScene = 0; BackScene = 0;} break;
                case 4: Environment.Exit(1); break;
                case 5:
                    SceneKey();
                    if (BackScene != 1) Engine.Draw(Textures.splashwin);
                    else { changeScene = 0; BackScene = 0;} break;
                case 6:
                    SceneKey();
                    if (BackScene != 1) Engine.Draw(Textures.splashlose);
                    else { changeScene = 0; BackScene = 0;} break;
            }
        }
        public void SelectionSelect()
        {
            if (selected != null)
            {
                if (selected == start) { changeScene = 1; OnStartGame?.Invoke(); }
                if (selected == controls) { changeScene = 2; }
                if (selected == credits) { changeScene = 3; }
                if (selected == exit) { changeScene = 4; }
            }
        }
        public void Selection(Button current)
        {
            if (selected != null) selected.Unselected();

            selected = current;
            selected.Selected();
        }
    }
}
