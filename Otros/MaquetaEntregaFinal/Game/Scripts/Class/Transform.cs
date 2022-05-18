using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Transform
    {
        //Propiedades
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public float Angle { get; set; } = 0;

        public Transform(Vector2 position, Vector2 scale, float angle)
        {
            Position = position;
            Scale = scale;
            Angle = angle;
        }
    }
}
