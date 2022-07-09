using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game
{
    public class Animation
    {
        // Stats
        private string name;
        private float speed;
        private float currentTime = 0;
        private int currentFrame = 0;
        private bool isLooping;
        private int loopsleft = 0;
        private int originalloops = 0;
        private bool manualFrames = false;
        private bool playonstart = true;
        public bool IsManualFrame => manualFrames;
        private List<Texture> textures = new List<Texture>();
        public Texture CurrentTexture => textures[currentFrame];
        public int CurrentFrame => currentFrame;

        // Events
        public Action OnAnimationLooped;
        public Action OnAnimationFinished;
        public Vector2 TextureSize => new Vector2(CurrentTexture.Width, CurrentTexture.Height);

        public int AnimationLongitude() { return textures.Count; }
        public Texture GetFrameTexture(int whatFrame) { return textures[whatFrame]; }

        public Animation(string name, float speed, List<Texture> textures = null, bool isLooping = true, int maxloops = -1, bool setManualFrames = false, bool playOnStart = true)
        {
            this.playonstart = playOnStart;
            this.name = name;
            this.speed = speed;
            this.isLooping = isLooping;
            this.currentFrame = 0;
            this.originalloops = maxloops;
            this.loopsleft = maxloops;
            this.manualFrames = setManualFrames;

            if (textures != null) this.textures = textures;
        }

        public void Play()
        {
            this.loopsleft = this.originalloops;
            this.currentFrame = 0;
            this.currentTime = 0;
            this.playonstart = true;
        }

        public void ChangeFrame(int newFrame)
        {
            if (newFrame <= textures.Count)
                this.currentFrame = newFrame;
            else if (newFrame > textures.Count)
                Console.WriteLine("AVISO: '{0}' Evento ChangeFrame - no posee frame {1}, ya que su último frame es {2}", name, newFrame, textures.Count);
        }

        public void Update()
        {
            if (!manualFrames && playonstart && (loopsleft > 0 || loopsleft == -1))
            {
                currentTime += Program.GetDeltaTime;
                if (currentTime >= speed)
                {
                    currentFrame++;
                    currentTime = 0;

                    if (currentFrame >= textures.Count)
                    {
                        if (isLooping && loopsleft > 0) { currentFrame = 0; loopsleft--; OnAnimationLooped?.Invoke(); }
                        else if (isLooping && loopsleft == -1) { currentFrame = 0; OnAnimationLooped?.Invoke(); }
                        else { currentFrame = textures.Count -1; OnAnimationFinished?.Invoke(); /*isLooping = false;*/ }
                    }
                }
            }
        }
    }
}
