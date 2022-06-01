using System;
using System.Collections.Generic;

namespace Game
{
    public class Animation
    {
        private string name;
        private float speed;
        private float currentTime = 0;
        private int currentFrame = 0;
        private bool isLooping;
        private bool manualFrames = false;
        private List<Texture> textures = new List<Texture>();
        public Texture CurrentTexture => textures[currentFrame];
        public Action OnAnimationFinished;

        public int AnimationLongitude()
        {
            return textures.Count;
        }

        public Texture GetFrameTexture(int whatFrame)
        {
            return textures[whatFrame];
        }

        public Animation(string name, float speed, List<Texture> textures = null, bool isLooping = true, bool setManualFrames = false, bool playOnStart = true)
        {
            this.name = name;
            this.speed = speed;
            this.isLooping = isLooping;
            this.currentFrame = 0;
            this.manualFrames = setManualFrames;

            if (textures != null)
                this.textures = textures;
        }

        public void Play()
        {
            this.currentFrame = 0;
            this.currentTime = 0;
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
            if (!manualFrames)
            {
                currentTime += Program.GetDeltaTime();

                if (currentTime >= speed)
                {
                    currentFrame++;
                    currentTime = 0;

                    if (currentFrame >= textures.Count)
                    {
                        OnAnimationFinished?.Invoke();
                        if (isLooping)
                            currentFrame = 0;
                        else
                            currentFrame = textures.Count - 1;
                    }
                }
            }
        }
    }
}
