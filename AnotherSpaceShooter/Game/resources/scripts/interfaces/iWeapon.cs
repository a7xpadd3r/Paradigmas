using System.Numerics;

namespace Game
{
    public interface iWeapon
    {
        iGetWeapon Owner { get; }
        void NewOwner(iGetWeapon owner);
        void Fire(Vector2 spawnPosition);
        void Update();
    }
}
