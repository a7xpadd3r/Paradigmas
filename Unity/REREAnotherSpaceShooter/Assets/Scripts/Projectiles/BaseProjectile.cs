using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] public float damage = 1;
    [SerializeField] public float speed = 1;
    [SerializeField] public float startingSpeedMultiplier = 1;
    [SerializeField] public float life = 1;
    [SerializeField] public Rigidbody2D rBody;


    public Vector2 position = Vector2.zero;
    public float delta = 0;

    public abstract void Start();
    public abstract void Collision();

    private void Update()
    {
        this.delta = Time.deltaTime;

        if (this.life > 0) this.life -= delta;
        if (this.life <= 0) Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        this.rBody.AddForce(Vector2.up * (speed * this.delta), ForceMode2D.Impulse);
    }
}
