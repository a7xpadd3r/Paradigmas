using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class CreditsScene : IScene
    {
        private string texturePath;          
        private float currentTime;      
        private float timeToBack;

        public CreditsScene(string texturePath, float timeToBack)
        {          
            this.texturePath = texturePath;
            this.timeToBack = timeToBack;         
        }
        public void Update()
        {
            GameObjectManager.ClearGameObjects();
            currentTime += Time.DeltaTime;

            if (currentTime >= timeToBack)
            {
                currentTime = 0;
                GameManager.Instance.ChangeScene(Scenes.MainMenu);
            }
           
        }

        public void Initialize()
        {

        }
       
        public void Render()
        {
            Engine.Draw(texturePath);          
        }

           
        public void Finish()
        {

        }
    }
}
