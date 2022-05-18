using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Button
    {
        //Campos
        private Vector2 position;
        private float scale;
        private string normalTexturePath;
        private string selectTexturePath;
        private bool isSelect;


        //Propiedades
        public Button NextButton { get; private set; }
        public Button PreviousButton { get; private set; }

        //Constructor
        public Button(Vector2 initialPosition, float scale, string normalTexturePath, string selectTexturePath)
        {
            Initialize(initialPosition, scale, normalTexturePath, selectTexturePath);
        }

        public void Initialize(Vector2 initialPosition, float scale, string normalTexturePath, string selectTexturePath)
        {
            this.position = initialPosition;
            this.scale = scale;
            this.normalTexturePath = normalTexturePath;
            this.selectTexturePath = selectTexturePath;
        }

        public void AssingButtons(Button nextButton, Button previousButton)
        {
            this.NextButton = nextButton;
            this.PreviousButton = previousButton;
        }

        public void Unselect()
        {
            isSelect = false;
        }

        public void Select()
        {
            isSelect = true;
        }

        public void Update()
        {

        }

        public void Render()
        {
            Engine.Draw(isSelect ? selectTexturePath : normalTexturePath, position.X, position.Y, scale, scale);
        }
    }
}
