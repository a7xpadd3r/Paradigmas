using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : ShipLogic
{
    // Player Weapons
    [SerializeField] private List<BaseWeapons> enabledWeapons = new List<BaseWeapons>();
    [SerializeField] private List<BaseWeapons> disabledWeapons = new List<BaseWeapons>();
    private int currentWeaponIndex = 0;
    public int WeaponSelection => currentWeaponIndex;

    public override void Start()
    {
        this.rBody = GetComponent<Rigidbody2D>();

        if (this.enabledWeapons[this.WeaponSelection] != null && this.enabledWeapons[this.WeaponSelection].pool == null)
            this.enabledWeapons[this.WeaponSelection].Initialize();
    }

    public override void Update()
    {
        float delta = Time.deltaTime;

        // Update function for current weapon in use
        if (this.enabledWeapons[currentWeaponIndex] != null)
            this.enabledWeapons[currentWeaponIndex].UpdateThis(delta);

        ShieldUpdateDelta(delta);
    }

    public override void SwapWeapon(float direction)
    {
        if (this.enabledWeapons.Count > 1)
        {
            // Next weapon
            if (direction > 0)
            {
                if (this.currentWeaponIndex == this.enabledWeapons.Count - 1)
                    this.currentWeaponIndex = 0;

                else
                    this.currentWeaponIndex++;
            }

            // Previous weapon
            else
            {
                if (this.currentWeaponIndex == 0)
                    this.currentWeaponIndex = this.enabledWeapons.Count - 1;

                else
                    this.currentWeaponIndex--;
            }
        }

        if (this.enabledWeapons[this.WeaponSelection].pool == null)
            this.enabledWeapons[currentWeaponIndex].Initialize();
    }

    public override void Fire(Vector2 spawnPoint)
    {
        if (this.enabledWeapons.Count > 0 && this.enabledWeapons[currentWeaponIndex] != null)
            this.enabledWeapons[currentWeaponIndex].Fire(spawnPoint);
    }
}
