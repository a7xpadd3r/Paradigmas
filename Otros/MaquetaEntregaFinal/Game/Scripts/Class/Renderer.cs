using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Renderer
    {
        //Campos
        public Transform transform;

        //Propiedades
        public Texture Texture { get; set; }
        

        //Metodos
        public void Render(Texture texture, Transform transform, float Radius)
        {
            Engine.Draw(texture, transform.Position.X, transform.Position.Y, transform.Scale.X, transform.Scale.Y, transform.Angle, Radius);
        }
    }
}
