using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class MainManuScene
    {
        //Const
        private const float INPUT_DELAY_TIME = 0.2f;


        //Campos
        private string backgroundTexturePath;
        private Button playButton;
        private Button creditsButton;
        private Button quitButton;
        private Button selectedButton;
        private float currentInputDelayTime;


        //Constructor
        public MainManuScene(string backgroundTexturePath)
        {
            this.backgroundTexturePath = backgroundTexturePath;
        }


        //Metodos

        //Initialize
        public void Initialize()
        {
            playButton = new Button(new Vector2(400, 300), 1, 0, GetAnimationPlayButton());
            creditsButton = new Button(new Vector2(400, 375), 1, 0, GetAnimationCreditsButton());
            quitButton = new Button(new Vector2(400, 450), 1, 0, GetAnimationQuitButton());

            playButton.AssignButton(creditsButton, quitButton);
            creditsButton.AssignButton(quitButton, playButton);
            quitButton.AssignButton(playButton, creditsButton);

            SelectedButton(playButton);
        }


        //Update
        public void Update()
        {
            currentInputDelayTime += Time.DeltaTime;
           

            if ((Engine.GetKey(Keys.UP) || Engine.GetKey(Keys.W)) && currentInputDelayTime > INPUT_DELAY_TIME)
            {
                currentInputDelayTime = 0;
                SelectedButton(selectedButton.PreviousButton);
            }
            if ((Engine.GetKey(Keys.DOWN) || Engine.GetKey(Keys.S)) && currentInputDelayTime > INPUT_DELAY_TIME)
            {
                currentInputDelayTime = 0;
                SelectedButton(selectedButton.NetButton);
            }
            if (Engine.GetKey(Keys.NUMPADENTER) && currentInputDelayTime > INPUT_DELAY_TIME)
            {
                currentInputDelayTime = 0;
                PressSelectedButton();
            }

        }


        //Render
        public void Render()
        {
            Engine.Draw(backgroundTexturePath);          
        }


        //SelectedButton
        private void SelectedButton(Button button)
        {
            if (selectedButton != null)
            {
                selectedButton.Unselect();
            }
            selectedButton = button;
            selectedButton.Select();
        }


        //PressSelectedButton
        private void PressSelectedButton()
        {
            if (selectedButton != null)
            {
                if (selectedButton == playButton)
                {
                    GameManager.Instance.ChangeScene(Scenes.Level);
                }
                else if (selectedButton == creditsButton)
                {
                    GameManager.Instance.ChangeScene(Scenes.Credits);
                }
                else if (selectedButton == quitButton)
                {
                    GameManager.Instance.ExitGame();
                }
            }
        }

        private List<Animation> GetAnimationPlayButton()
        {
            //Se crea la Lista
            List<Animation> animations = new List<Animation>();


            //Normal Animation
            List<Texture> normalFrames = new List<Texture>();
            normalFrames.Add(Engine.GetTexture($"Textures/Buttons/Play/PlayButtonNormalTexture.png"));

            Animation normalAnimation = new Animation("Normal", normalFrames, 0.2f, true);
            animations.Add(normalAnimation);

            //Selected Animation
            List<Texture> selectedFrames = new List<Texture>();
            selectedFrames.Add(Engine.GetTexture($"Textures/Buttons/Play/PlayButtonSelected.png"));

            Animation selectedAnimation = new Animation("Selected", selectedFrames, 0.2f, true);
            animations.Add(selectedAnimation);


            return animations;
                  
        }

        private List<Animation> GetAnimationCreditsButton()
        {
            //Se crea la Lista
            List<Animation> animations = new List<Animation>();


            //Normal Animation
            List<Texture> normalFrames = new List<Texture>();
            normalFrames.Add(Engine.GetTexture($"Textures/Buttons/Credits/CreditsButtonNormalTexture.png"));

            Animation normalAnimation = new Animation("Normal", normalFrames, 0.2f, true);
            animations.Add(normalAnimation);

            //Selected Animation
            List<Texture> selectedFrames = new List<Texture>();
            selectedFrames.Add(Engine.GetTexture($"Textures/Buttons/Credits/CreditsSelected.png"));

            Animation selectedAnimation = new Animation("Selected", selectedFrames, 0.2f, true);
            animations.Add(selectedAnimation);


            return animations;
        }

        private List<Animation> GetAnimationQuitButton()
        {
            //Se crea la Lista
            List<Animation> animations = new List<Animation>();


            //Normal Animation
            List<Texture> normalFrames = new List<Texture>();
            normalFrames.Add(Engine.GetTexture($"Textures/Buttons/Quit/QuitButtonNormalTexture.png"));

            Animation normalAnimation = new Animation("Normal", normalFrames, 0.2f, true);
            animations.Add(normalAnimation);

            //Selected Animation
            List<Texture> selectedFrames = new List<Texture>();
            selectedFrames.Add(Engine.GetTexture($"Textures/Buttons/Quit/QuitSelected.png"));

            Animation selectedAnimation = new Animation("Selected", selectedFrames, 0.2f, true);
            animations.Add(selectedAnimation);


            return animations;
        }
    }
}
