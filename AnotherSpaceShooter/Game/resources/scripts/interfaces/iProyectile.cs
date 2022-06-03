using System.Numerics;

namespace Game
{
    public interface iProyectile
    {
        WeaponTypes Type { get; }
        Vector2 Position { get; set; }
        float Speed { get; }
        float Damage { get; }
        void Reset();  // Pool?
    }
}
