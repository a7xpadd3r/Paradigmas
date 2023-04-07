using UnityEngine;

public class GreenCrast : BaseProjectile
{
    public override void Collision()
    {
        print("collision?");
    }

    public override void Start()
    {
        if (this.rBody == null) this.rBody = GetComponent<Rigidbody2D>();
        this.rBody.AddForceAtPosition(Vector2.up * (this.speed * this.startingSpeedMultiplier), this.transform.position);
    }

}