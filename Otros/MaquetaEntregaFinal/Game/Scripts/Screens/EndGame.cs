namespace Game
{
    class EndGame : IScene
    {
        private string texturePath;
        private float currentTime;
        private float timeToBack;

        public EndGame(string texturePath, float timeToBack)
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

        public void Render()
        {
            Engine.Draw(texturePath);
        }

        public void Initialize()
        {

        }

        public void Finish()
        {

        }
    }
}
