using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class TransformData
    {
        //Campos
        private Vector2 position;
        private float scale;
        private float rotation;


        //Propiedades
        public Vector2 Position { get => position; set => position = value; }
        public float Scale { get => scale; set => scale = value; }
        public float Rotation { get => rotation; set => rotation = value; }
    }
}
