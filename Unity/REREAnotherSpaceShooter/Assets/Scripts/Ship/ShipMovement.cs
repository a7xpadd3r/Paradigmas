using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField, Range(0.01f, 50)] private float speed = 2;
    //[SerializeField, Range(0.02f, 100)] private float maxSpeed = 4;

    private Rigidbody2D rBody;
    private Vector2 direction = Vector2.zero;

    // Start is called before the first frame update
    private void Start()
    {
        this.rBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (this.direction != Vector2.zero)
        {
            this.rBody.AddForce(this.direction * (Time.deltaTime * this.speed), ForceMode2D.Impulse);
            this.direction = Vector2.zero;
        }
    }

    public void Move(Vector2 newDirection)
    {
        this.direction = newDirection;
    }
}
