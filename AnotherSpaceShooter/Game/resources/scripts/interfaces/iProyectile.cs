using System.Numerics;

namespace Game
{
    public interface iProyectile
    {
        Transform transform { get; set; }
        Animation animation { get; set; }
        float speed { get; }
        float damage { get; }
        bool active { get; }
        void Update();
        void Render();
        void Reset();       // Pool?
    }
}
