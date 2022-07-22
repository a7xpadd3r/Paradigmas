using System;
using System.Numerics;

namespace Game
{
    public enum AxisX { None, Left, Right}
    public enum AxisY { None, Up, Down }
    public enum MovementType { Lineal, WaveX, WaveY }
    public interface iIA
    {
        float Speed { get; set; }
        void Update(float delta);
    }
}
