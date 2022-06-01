using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GenericEffect
    {
        public Animation anim;
        public Vector2 renderPos;
        public Vector2 size;
        public Vector2 offset;
        public float rotation;

        public GenericEffect(Vector2 newRenderPos, Vector2 newSize, Vector2 newOffset, float newRotation,  string newName, List<Texture> newTextures, float newSpeed, bool newLoop, bool newManualFrames, bool newPlayOnStart)
        {
            this.anim = new Animation(newName, newSpeed, newTextures, newLoop, newManualFrames, newPlayOnStart);
            this.renderPos = newRenderPos;
            this.size = newSize;
            this.offset = newOffset;
            this.rotation = newRotation;
        }

        public void UpdateRender()
        {
            if (anim != null)
            {
                Engine.Draw(anim.CurrentTexture, renderPos.X, renderPos.Y, size.X, size.Y, rotation, offset.X, offset.Y);
                anim.Update();
            }
        }
    
    }
}
