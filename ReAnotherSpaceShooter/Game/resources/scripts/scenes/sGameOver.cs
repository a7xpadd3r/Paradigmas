using System;

namespace Game
{
    public class sGameOver
    {
        private Texture scenerender = Textures.splashwin;
        public Action OnExitScene;

        public sGameOver(bool didyouwin)
        {
            if (!didyouwin) scenerender = Textures.splashlose;
            else scenerender = Textures.splashwin;
        }

        public void Update()
        {
            if (Engine.GetKey(Keys.ESCAPE))
            {
                OnExitScene?.Invoke();
            }
            Engine.Draw(scenerender);
        }
    }
}
