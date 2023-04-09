using System.Collections.Generic;
using UnityEngine;

public abstract class ShipLogic : MonoBehaviour
{
    // Basic
    [SerializeField, Range(1, 100)] private float speed = 50;
    [HideInInspector] public Vector2 direction = Vector2.zero;
    public Rigidbody2D rBody;

    // Shield
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private float shieldDuration = 2;
    private float currentShieldTime = 0;
    private bool isShielding = false;

    public bool IsShielding => isShielding;

    public abstract void Start();
    public abstract void Update();
    public abstract void Fire(Vector2 spawnPoint);
    public abstract void SwapWeapon(float direction);   // Previous -1 / Next 1.

    public virtual void FixedUpdate()
    {
        if (this.direction != Vector2.zero && this.rBody != null)
        {
            this.rBody.AddForce(this.direction * (Time.deltaTime * this.speed), ForceMode2D.Impulse);
            this.direction = Vector2.zero;
        }
    }

    public virtual void Move(Vector2 newDirection)
    {
        this.direction = newDirection;
    }

    public virtual void GetWeapon(fWeapons.WeaponTypes type)
    {
        print($"new weapon event= {type}");
    }

    public virtual void ShieldUpdateDelta(float delta)
    {
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



    public void TESTSHIELD()
    {
        this.currentShieldTime = this.shieldDuration;
    }
}