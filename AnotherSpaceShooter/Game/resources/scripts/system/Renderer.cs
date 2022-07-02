using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Renderer
    {
        public void RenderTexture(Texture texture, Transform transform)
        {
            Engine.DrawTransform(texture, transform);
        }
    }
}
