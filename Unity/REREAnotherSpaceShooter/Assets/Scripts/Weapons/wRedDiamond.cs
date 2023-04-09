using UnityEngine;

public class wRedDiamond : BaseWeapons
{
    [SerializeField] private int WeaponOriginalAmmo = 30;

    public override void ExtraParameters()
    {
        this.ammo = this.WeaponOriginalAmmo;
        this.ThisType = fWeapons.WeaponTypes.RedDiamond;
    }
}