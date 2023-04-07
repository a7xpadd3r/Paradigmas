using UnityEngine;

public class BlueRail : BaseProjectile
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

    /*
    public override void Update()
    {
        this.delta = Time.deltaTime;

        if (this.life > 0) this.life -= delta;
        if (this.life <= 0) Destroy(this.gameObject);
    }

    public override void FixedUpdate()
    {
        this.rBody.AddForce(Vector2.up * (speed * this.delta), ForceMode2D.Impulse);
    }*/
}
