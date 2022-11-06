using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float horizontalForce;
    public float horizontalSpeed;
    public float verticalForce;
    public float airFactor;
    public Transform groundPoint;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hit = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        isGrounded = hit;
        Move();
        Jump();
    }

    private void Move()
    {
        var x = Input.GetAxisRaw("Horizontal");

        if(x != 0.0f)
        {
            Flip(x);

            x = ((x > 0.0) ? 1.0f : -1.0f); // if x is greater than 0 is = 1 else if is less than 0 is -1
            rb.AddForce(Vector2.right * x * horizontalForce * ((isGrounded) ? 1.0f : airFactor));

            var clampXVel = Mathf.Clamp(rb.velocity.x, -horizontalSpeed, horizontalSpeed);
            rb.velocity = new Vector2(clampXVel, rb.velocity.y);
        }
    }

    private void Jump()
    {
        var y = Input.GetAxis("Jump");

        if((isGrounded) && y > 0.0f)
        {
            rb.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
        }
    }

    public void Flip(float x)
    {
        if (x != 0.0f)
        {
            transform.localScale = new Vector3((x > 0.0f) ? 1.0f : -1.0f, 1.0f, 1.0f);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundPoint.position,groundRadius);
    }
}
