using System;
using System.Numerics;

namespace Game
{
    public class Renderer
    {
        // Rendering at the center
        public static void DrawCenter(Texture whattexture, Transform where)
        {
            Engine.Draw(whattexture, where.Position.X, where.Position.Y, where.Scale.X, where.Scale.Y, where.Rotation, whattexture.Width / 2, whattexture.Height / 2);
        }
        public static void DrawCenter(Texture whattexture, Transform where, Vector2 offset)
        {
            Engine.Draw(whattexture, where.Position.X, where.Position.Y, where.Scale.X, where.Scale.Y, where.Rotation, offset.X, offset.Y);
        }
        public static void DrawCenter(Texture whattexture, Vector2 where)
        {
            Engine.Draw(whattexture, where.X, where.Y, 1, 1, 0, whattexture.Width / 2, whattexture.Height / 2);
        }
        public static void DrawCenter(Texture whattexture, Vector2 where, Vector2 howbig, float rotation = 0)
        {
            Engine.Draw(whattexture, where.X, where.Y, howbig.X, howbig.Y, rotation, whattexture.Width / 2, whattexture.Height / 2);
        }
        public static void DrawCenter(Texture whattexture, DoubleVector2 where)
        {
            Engine.Draw(whattexture, where.Position.X, where.Position.Y, where.Scale.X, where.Scale.Y, 0, whattexture.Width / 2, whattexture.Height / 2);
        }

        // Rendering with custom offset
        public static void DrawSize(Texture whattexture, Transform where)
        {
            Engine.Draw(whattexture, where.Position.X, where.Position.Y, where.Scale.X, where.Scale.Y, where.Rotation, whattexture.Width / 2, whattexture.Height / 2);
        }
        public static void DrawSize(Texture whattexture, Transform where, Vector2 offset)
        {
            Engine.Draw(whattexture, where.Position.X, where.Position.Y, where.Scale.X, where.Scale.Y, where.Rotation, offset.X, offset.Y);
        }
        public static void DrawSize(Texture whattexture, DoubleVector2 where)
        {
            Engine.Draw(whattexture, where.Position.X, where.Position.Y, where.Scale.X, where.Scale.Y);
        }

        public static void Draw(Texture whattexture, Vector2 start, Vector2 scale, float angle = 0)
        {
            Engine.Draw(whattexture, start.X, start.Y, scale.X, scale.Y, angle, whattexture.Width /2);
        }
    }
}
