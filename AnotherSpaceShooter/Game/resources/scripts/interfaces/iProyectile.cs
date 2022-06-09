using System.Numerics;

namespace Game
{
    public interface iProyectile
    {
        Transform transform { get; set; }
        Animation animation { get; set; }
        Vector2 texturesize { get; set; }

        float speed { get; }
        void Update();
        void Render();
        void Reset();       // Pool?
    }
}
