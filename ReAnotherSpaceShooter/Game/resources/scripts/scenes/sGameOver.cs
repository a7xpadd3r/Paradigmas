using System;

namespace Game
{
    public class sGameOver
    {
        private Texture scenerender = Textures.splashwin;
        public Action OnExitScene;

        public sGameOver(bool didyouwin)
        {
            if (!didyouwin) { scenerender = Textures.splashlose; GameManager.PlayMusic(MusicTracks.Lose); }
            else { scenerender = Textures.splashwin; GameManager.PlayMusic(MusicTracks.Win); }
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
