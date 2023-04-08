using System;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] public float damage = 1;
    [SerializeField] public float speed = 1;
    [SerializeField] public float startingSpeedMultiplier = 1;
    [SerializeField, Range(0.5f, 5)] public float life = 1;
    [SerializeField] public Rigidbody2D rBody;

    [HideInInspector] public Action<BaseProjectile> projectileRemove;
    [HideInInspector] public Vector2 position = Vector2.zero;
    [HideInInspector] public float delta = 0;

    public virtual void Reset(Vector2 newPosition, Action<BaseProjectile> action)
    {
        this.life = 2;
        this.transform.position = newPosition;
        this.gameObject.SetActive(true);
        this.projectileRemove = action;

        if (this.rBody == null)
            this.rBody = GetComponent<Rigidbody2D>();

        if (this.rBody != null)
            this.rBody.AddForceAtPosition(Vector2.up * (this.speed * this.startingSpeedMultiplier), this.transform.position);
    }

    public abstract void Start();
    public abstract void Collision();

    private void Update()
    {
        this.delta = Time.deltaTime;

        if (this.life > 0) 
            this.life -= delta;

        if (this.life <= 0)
            this.projectileRemove.Invoke(this);
    }

    private void FixedUpdate()
    {
        this.rBody.AddForce(Vector2.up * (speed * this.delta), ForceMode2D.Impulse);
    }
}
