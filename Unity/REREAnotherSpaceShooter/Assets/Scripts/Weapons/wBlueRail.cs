using UnityEngine;

public class wBlueRail : BaseWeapons
{
    public override void Fire(Vector2 spawnPoint)
    {
        if (this.canShoot)
        {
            this.canShoot = false;
            BaseProjectile pooledprojectile = this.pool.Get();
            pooledprojectile.Reset(spawnPoint, PoolCall);
        }
    }

    public override void ExtraParameters()
    {
        this.ThisType = fWeapons.WeaponTypes.BlueRail;
    }
}