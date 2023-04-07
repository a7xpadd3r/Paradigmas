using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLogic : MonoBehaviour
{
    // Status
    //[SerializeField] private bool hasWeapon = true;
    //private float currentLife = 10;
    //private float maxLife = 20;

    // Weapons
    private int currentWeaponIndex = 0;
    public BaseWeapons currentWeapon;
    public List<BaseWeapons> enabledWeapons = new List<BaseWeapons>();
    public List<BaseWeapons> disabledWeapons = new List<BaseWeapons>();

    // Shield
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private float shieldDuration = 2;
    private float currentShieldTime = 0;
    private bool isShielding = false;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;

        if (this.currentWeapon != null) this.currentWeapon.UpdateThis(delta);

        if (this.currentShieldTime > 0)
        {
            this.currentShieldTime -= delta;
            if (!this.shieldObject.activeSelf)
                this.shieldObject.SetActive(true);

            this.isShielding = true;
        }
        if (this.currentShieldTime <= 0 && this.shieldObject.activeSelf)
        {
            this.shieldObject.SetActive(false);
            this.isShielding = false;
        }
    }

    public void SwapWeapon(float direction)
    {
        if (enabledWeapons.Count > 1)
        {
            if (direction > 0)
            {
                if (this.currentWeaponIndex == this.enabledWeapons.Count - 1)
                    this.currentWeaponIndex = 0;

                else
                    this.currentWeaponIndex++;
            }

            else
            {
                if (this.currentWeaponIndex == 0)
                    this.currentWeaponIndex = this.enabledWeapons.Count - 1;

                else
                    this.currentWeaponIndex--;
            }

            this.currentWeapon = this.enabledWeapons[this.currentWeaponIndex];
        }
    }

    public void GetWeapon(fWeapons.WeaponTypes type)
    {
        print($"new weapon event= {type}");
    }

    public void TESTSHIELD()
    {
        this.currentShieldTime = this.shieldDuration;
    }
}