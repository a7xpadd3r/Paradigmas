using UnityEngine;
using static fWeapons;

public class wBlueRail : BaseWeapons
{
    [SerializeField] private BlueRail prefab;

    // Weapon stats
    private bool canShoot = true;
    private float currentRecoil = 0;

    public WeaponTypes ThisType => WeaponTypes.BlueRail;
    public int CurrentAmmo => ammo;

    public void AddAmmo(int howMuch)
    {
        this.ammo += howMuch;
    }

    public override void Fire(Vector2 spawnPoint)
    {
        if (this.canShoot)
        {
            this.canShoot = false; 
            BlueRail projectile = Instantiate(this.prefab, spawnPoint, new Quaternion());
            projectile.position = spawnPoint;
        }
    }

    public override void AmmoGrab(int howMuch)
    {
        this.ammo += howMuch;
    }

    public override void UpdateThis(float delta)
    {
        if (!this.canShoot)
            this.currentRecoil += delta;

        if (this.currentRecoil >= this.recoilTime && !this.canShoot)
        {
            this.currentRecoil = 0;
            this.canShoot = true;
        }
    }
}
