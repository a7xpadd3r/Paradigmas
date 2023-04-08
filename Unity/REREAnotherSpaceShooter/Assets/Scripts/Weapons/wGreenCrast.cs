using UnityEngine;

public class wGreenCrast : BaseWeapons
{
    [SerializeField] private int WeaponOriginalAmmo = 50;

    public override void ExtraParameters()
    {
        this.ammo = this.WeaponOriginalAmmo;
    }
}