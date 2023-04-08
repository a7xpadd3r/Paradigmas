using UnityEngine;
using UnityEngine.Pool;
using static fWeapons;

public abstract class BaseWeapons : MonoBehaviour
{
    [SerializeField] private float recoilTime = 0.4f;
    public WeaponTypes ThisType = WeaponTypes.BlueRail;

    public BaseProjectile prefab;
    public ObjectPool<BaseProjectile> pool;

    public bool canShoot = true;
    public float currentRecoil = 0;
    public int ammo = 0;

    public int CurrentAmmo => ammo;
    public float RecoilTime => recoilTime;

    public virtual void Fire(Vector2 spawnPosition)
    {
        if (this.canShoot && this.ammo > 0)
        {
            this.ammo--;
            this.canShoot = false;
            BaseProjectile pooledprojectile = this.pool.Get();
            pooledprojectile.Reset(spawnPosition, PoolCall);
        }
    }

    // Fill the pool
    public virtual void Initialize()
    {
        if (this.pool == null)
        {
            this.pool = new ObjectPool<BaseProjectile>(() =>
            {
                return Instantiate(this.prefab);
            }, projectile =>
            {
                projectile.gameObject.SetActive(true);
            }, projectile =>
            {
                projectile.gameObject.SetActive(false);
            }, projectile =>
            {
                Destroy(projectile.gameObject);
            }, false, 10, 30);
        }

        ExtraParameters();
    }

    public virtual void ExtraParameters() { }

    public void PoolCall(BaseProjectile projectile)
    {
        this.pool.Release(projectile);
    }

    public virtual void UpdateThis(float delta)
    {
        if (!this.canShoot)
            this.currentRecoil += delta;

        if (this.currentRecoil >= this.RecoilTime && !this.canShoot)
        {
            this.currentRecoil = 0;
            this.canShoot = true;
        }
    }

    public virtual void AmmoGrab(int howMuch)
    {
        this.ammo += howMuch;
    }
}
