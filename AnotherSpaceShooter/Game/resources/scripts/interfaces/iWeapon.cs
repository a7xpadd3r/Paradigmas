using System.Numerics;

namespace Game
{
    public interface iWeapon
    {
        iGetWeapon Owner { get; }
        void NewOwner(iGetWeapon owner);
        void Fire();
        void Update(float delta, Vector2 currentPosition);
    }
}
