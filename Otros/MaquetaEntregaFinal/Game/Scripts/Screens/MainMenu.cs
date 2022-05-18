using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class MainMenu : IScene
    {
        private Button playButton;
        private Button controlsButton;
        private Button creditsButton;
        private Button exitButton;
        private Button selectedButton;
        private string backgroundTexturePath;
        private float currentInputDelayTime;
        private float inputDelayTime = 0.2f;

        public MainMenu(string backgroundTexturePath)
        {
            this.backgroundTexturePath = backgroundTexturePath;
        }

        public void Initialize()
        {
            playButton = new Button(new Vector2(400, 300), 1f, "Textures/Buttons/Play/PlayButtonNormalTexture.png", "Textures/Buttons/Play/PlayButtonSelected.png");
            creditsButton = new Button(new Vector2(400, 350), 1f, "Textures/Buttons/Credits/CreditsButtonNormalTexture.png", "Textures/Buttons/Credits/CreditsSelected.png");
            exitButton = new Button(new Vector2(400, 400), 1f, "Textures/Buttons/Quit/QuitButtonNormalTexture.png", "Textures/Buttons/Quit/QuitSelected.png");
            controlsButton = new Button(new Vector2(400, 450), 1f, "Textures/Buttons/Controls/ControlsButtonNormalTexture.png", "Textures/Buttons/Controls/ControlsButtonSelected.png");
            playButton.AssingButtons(creditsButton, controlsButton);
            creditsButton.AssingButtons(exitButton, playButton);
            exitButton.AssingButtons(controlsButton, creditsButton);
            controlsButton.AssingButtons(playButton, exitButton);

            SelectButton(playButton);
        }

        public void Update()
        {
            currentInputDelayTime -= Time.DeltaTime;

            playButton.Update();
            creditsButton.Update();
            exitButton.Update();
            controlsButton.Update();

            if (Engine.GetKey(Keys.W) && currentInputDelayTime <= 0)
            {
                currentInputDelayTime = inputDelayTime;
                SelectButton(selectedButton.PreviousButton);
            }

            if (Engine.GetKey(Keys.S) && currentInputDelayTime <= 0)
            {
                currentInputDelayTime = inputDelayTime;
                SelectButton(selectedButton.NextButton);
            }

            if (Engine.GetKey(Keys.RETURN) && currentInputDelayTime <= 0)
            {
                currentInputDelayTime = inputDelayTime;
                PressSelectButton();
            }
        }

        public void Render()
        {
            Engine.Draw(backgroundTexturePath);

            playButton.Render();
            creditsButton.Render();
            exitButton.Render();
            controlsButton.Render();
        }

        public void PressSelectButton()
        {
            if (selectedButton != null)
            {
                if (selectedButton == playButton)
                {                
                    GameManager.Instance.ChangeScene(Scenes.Level1);
                }
                else if (selectedButton == creditsButton)
                {
                    GameManager.Instance.ChangeScene(Scenes.Credits);
                }
                else if (selectedButton == exitButton)
                {
                    GameManager.Instance.ExitGame();
                }
                else if (selectedButton == controlsButton)
                {
                    GameManager.Instance.ChangeScene(Scenes.Controls);
                }
            }
        }

        public void SelectButton(Button button)
        {
            if (selectedButton != null)
            {
                selectedButton.Unselect();
            }

            selectedButton = button;
            selectedButton.Select();
        }
        public void Finish()
        {
            GameObjectManager.ClearGameObjects();
        }
    }
}

