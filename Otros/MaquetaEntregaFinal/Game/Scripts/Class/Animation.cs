using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Animation
    {
        //Campos
        private bool isLoop;
        private float speed;
        private List<Texture> frames = new List<Texture>();
        private int currentFrameIndex;
        private float currentAnimationTime;
     

        //Propiedades
        public string Name { get; private set; }
        public Texture CurrentFrame => frames[currentFrameIndex];
      

        //Constructor
        public Animation(string name, List<Texture> frames, float speed = 0.1f, bool isLoop = true)
        {
            Name = name;
            this.speed = speed;
            this.isLoop = isLoop;
            this.frames = frames;
            this.currentFrameIndex = 0;
        }

        //Metodos
        public void Play()
        {
            this.currentFrameIndex = 0;
            this.currentAnimationTime = 0;
        }

        public void Update()
        {
            currentAnimationTime += Time.DeltaTime;

            if (currentAnimationTime >= speed)
            {
                currentFrameIndex++;
                currentAnimationTime = 0;

                if (currentFrameIndex >= frames.Count)
                {
                    if (isLoop)
                    {
                        currentFrameIndex = 0;
                    }
                    else
                    {
                        currentFrameIndex = frames.Count - 1;
                    }
                }
            }
        }
    }
}
