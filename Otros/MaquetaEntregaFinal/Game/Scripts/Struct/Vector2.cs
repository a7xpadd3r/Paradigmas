using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public struct Vector2
    {
        private float x;
        private float y;
        
        public static Vector2 Zero => new Vector2(0, 0);
        public static Vector2 One => new Vector2(1, 1);
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 Left => new Vector2(-1, 0);
        public static Vector2 Up => new Vector2(0, 1);
        public static Vector2 Down => new Vector2(0, -1);
        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.x + vector2.x, vector1.y + vector2.y);
        }
        public static Vector2 operator -(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X - vector2.X, vector1.Y - vector2.Y);
        }

        public static Vector2 operator *(Vector2 vector1, float float1)
        {
            return new Vector2(vector1.x * float1, vector1.y * float1);
        }

        public static bool operator ==(Vector2 vector1, Vector2 vector2)
        {
            return vector1.x == vector2.x && vector1.y == vector2.y;
        }

        public static bool operator !=(Vector2 vector1, Vector2 vector2)
        {
            return vector1.x != vector2.x || vector1.y != vector2.y;
        }
        public override string ToString()
        {
            return $"Vector2({x},{y})";
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 vector &&
                   x == vector.x &&
                   y == vector.y &&
                   X == vector.X &&
                   Y == vector.Y &&
                   EqualityComparer<Vector2>.Default.Equals(Right, Vector2.Right);
        }

        public override int GetHashCode()
        {
            int hashCode = -981856501;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Right.GetHashCode();
            return hashCode;
        }
    }
}
