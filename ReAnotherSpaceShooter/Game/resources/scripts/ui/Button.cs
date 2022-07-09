using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    class Button
    {
        private Vector2 position;
        private float size;
        private List<Texture> buttonTextures = null;
        private bool selected = false;
        private bool ready = false;

        public Button PreviousButton { get; private set; }
        public Button NextButton { get; private set; }

        public Button(Vector2 newPos, float newSize, List<Texture> newTextures)
        {
            InitializeButton(newPos, newTextures, newSize);
        }

        public void InitializeButton(Vector2 position, List<Texture> textures, float size = 1)
        {
            this.position = position;
            this.size = size;
            this.buttonTextures = textures;
            this.ready = true;
        }

        public void AssignButtons(Button previous, Button next)
        {
            this.PreviousButton = previous;
            this.NextButton = next;
        }

        public void Selected()
        {
            selected = true;
        }

        public void Unselected()
        {
            selected = false;
        }

        public void Update()
        {
            if (ready) Engine.Draw(selected ? buttonTextures[1] : buttonTextures[0], position.X, position.Y, size, size);
        }
        
    }
}
