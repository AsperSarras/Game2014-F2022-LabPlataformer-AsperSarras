using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float horizontalForce;
    public float horizontalSpeed;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        var x = Input.GetAxisRaw("Horizontal");

        if(x != 0.0f)
        {
            x = ((x > 0.0) ? 1.0f : -1.0f); // if x is greater than 0 is = 1 else if is less than 0 is -1
            rb.AddForce(Vector2.right * x * horizontalForce);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, horizontalSpeed);
        }


    }

    private void Jump()
    {

    }
}
