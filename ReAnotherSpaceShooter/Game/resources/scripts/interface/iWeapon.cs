using System.Numerics;

namespace Game
{
    public interface iWeapon
    {
        Vector2 BulletOut { get; set; }
        int CurrentAmmo { get; }
        int AddAmmoAmount { get; }
        WeaponTypes ThisType { get; }
        void Fire(string owner, Vector2 currentPosition);
        void AddAmmo();
        void Update(float delta);
    }
}
