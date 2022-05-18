using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class VictoryScene
    {
        //Campos
        private string backgroundTexturePath;


        //Constructor
        public VictoryScene(string backgroundTexturePath)
        {
            this.backgroundTexturePath = backgroundTexturePath;
        }


        //Metodos

        //Initialize
        public void Initialize()
        {

        }


        //Update
        public void Update()
        {
            if (Engine.GetKey(Keys.ESCAPE))
            {
                GameManager.Instance.ChangeScene(Scenes.MainMenuScene);
            }
        }


        //Render
        public void Render()
        {
            Engine.Draw(backgroundTexturePath);
        }
    }
}
