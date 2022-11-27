using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Animator animator;
    public PlayerAnimState playerAnimState;

    [Header("HpSystem")]
    public HealthBarController health;
    public LifeCounterController life;
    public DeathPlaneController deathPlane;

    public Joystick leftJoystick;
    [Range(0.1f, 1.0f)]
    public float verticalThreshold;

    private SoundManager soundManager;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = FindObjectOfType<PlayerHealth>().GetComponent<HealthBarController>();
        life = FindObjectOfType<LifeCounterController>();
        deathPlane = FindObjectOfType<DeathPlaneController>();
        soundManager = FindObjectOfType<SoundManager>();
        leftJoystick = (Application.isMobilePlatform) ? GameObject.Find("LeftStick").GetComponent<Joystick>() : null;
    }

    void Update()
    {
        if(health.value <= 0)
        {
            life.LoseLife();
            if(life.value > 0)
            {
                health.ReserHP();
                deathPlane.ReSpawn(gameObject);
                //Play Sound
                soundManager.PlaySoundFX(SoundFX.HURT, Channel.PLAYER_HURT_FX);
            }

        }

        if(life.value<=0)
        {
            //Load End Scene
            SceneManager.LoadScene(1);
   
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hit = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        isGrounded = hit;
        Move();
        Jump();
        AirCheck();
    }

    private void Move()
    {
        var x = Input.GetAxisRaw("Horizontal") + ((Application.isMobilePlatform) ? leftJoystick.Horizontal : 0.0f);

        if(x != 0.0f)
        {
            Flip(x);

            x = ((x > 0.0) ? 1.0f : -1.0f); // if x is greater than 0 is = 1 else if is less than 0 is -1
            rb.AddForce(Vector2.right * x * horizontalForce * ((isGrounded) ? 1.0f : airFactor));

            var clampXVel = Mathf.Clamp(rb.velocity.x, -horizontalSpeed, horizontalSpeed);
            rb.velocity = new Vector2(clampXVel, rb.velocity.y);

            ChangeAnimation(PlayerAnimState.RUN);
        }

        if ((isGrounded) && (x == 0))
        {
            ChangeAnimation(PlayerAnimState.IDLE);
        }
    }

    private void Jump()
    {
        var y = Input.GetAxis("Jump") + ((Application.isMobilePlatform) ? leftJoystick.Vertical : 0.0f);

        if ((isGrounded) && y > verticalThreshold)
        {
            rb.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
            soundManager.PlaySoundFX(SoundFX.JUMP, Channel.PLAYER_FX);
        }
    }

    public void Flip(float x)
    {
        if (x != 0.0f)
        {
            transform.localScale = new Vector3((x > 0.0f) ? 1.0f : -1.0f, 1.0f, 1.0f);
        }
    }

    private void ChangeAnimation(PlayerAnimState animState)
    {
        // Change the Animation to RUN
        //state = animState;
        playerAnimState = animState;
        animator.SetInteger("AnimState", (int)playerAnimState);
    }

    private void AirCheck()
    {
        if (!isGrounded)
        {
            ChangeAnimation(PlayerAnimState.JUMP);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundPoint.position,groundRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            health.TakeDamage(20);
            if (life.value > 0)
            {
                soundManager.PlaySoundFX(SoundFX.HURT,Channel.PLAYER_HURT_FX);
            }
            // Update the life value 
            // Play Take Damage sound
        }
    }
}
