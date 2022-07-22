using System.Collections.Generic;
using System.Media;

namespace Game
{
    public static class SFX
    {
        public static List<SoundPlayer> music = new List<SoundPlayer>();

        public static void PlayMusic()
        {
            music.Add(new SoundPlayer("resources/sfx/fbattery_loop.wav"));
            music.Add(new SoundPlayer("resources/gulp.wav"));
            //music[0].PlayLooping();
        }
    }
}
