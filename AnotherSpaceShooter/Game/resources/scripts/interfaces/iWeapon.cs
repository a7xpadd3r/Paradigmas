using System.Numerics;

namespace Game
{
    public interface iWeapon
    {
        iGetWeapon Owner { get; }
        WeaponTypes Type { get; }
        int CurrentAmmo { get; }
        void NewOwner(iGetWeapon owner);
        void Fire();
        void AddAmmo();
        void Update(float delta, Vector2 currentPosition);
    }
}
