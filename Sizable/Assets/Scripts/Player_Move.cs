using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    private bool isMoving;
    public float currentXSpeed;
    public float currentYSpeed;
    private Rigidbody2D rb;
    public bool canMove;

    private Transform transform;
    public DeathManager deathManager;
    private Vector3 respawnPoint;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private bool jump;
    private bool stopJump;
    public float jumpHeight;

    public float timeElapsed = 0f;
    public float lerpDuration;
    float startValue = 0;
    float endValue = 10;
    float valueToLerp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        currentXSpeed = rb.velocity.x;
        currentYSpeed = rb.velocity.y;
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        currentXSpeed = rb.velocity.x;
        currentYSpeed = rb.velocity.y;
        
        if (isTouchingGround)
        {
            respawnPoint = transform.position;
        }

        if (Input.GetKeyDown("space") && isTouchingGround)
        {
            jump = true;
        }

        if (Input.GetKeyUp("space") && !isTouchingGround && rb.velocity.y >= 0)
        {
            stopJump = true;
        }

        if (rb.velocity.x <= 0.7f && !isMoving && rb.velocity.x >= -0.7f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (deathManager.done)
        {
            rb.velocity = new Vector2(0f, 0f);
            transform.position = respawnPoint;
            deathManager.done = false;
            StartCoroutine(Lerp());
        }

        // Size Changing

        if (Input.GetKey("z"))
        {
            transform.localScale = new Vector2(0.5f, 0.5f);
            groundCheckRadius = 0.35f;
            jumpHeight = 540f;
            speed = 9f;
            maxSpeed = 10.5f;
        }
        if (Input.GetKey("x"))
        {
            transform.localScale = new Vector2(1f, 1f);
            groundCheckRadius = 0.7f;
            jumpHeight = 750f;
            speed = 6f;
            maxSpeed = 15f;
        }
        if (Input.GetKey("c"))
        {
            transform.localScale = new Vector2(2f, 2f);
            groundCheckRadius = 1.4f;
            jumpHeight = 960f;
            speed = 2f;
            maxSpeed = 7f;
        }
    }
    IEnumerator Lerp()
    {
        timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        valueToLerp = endValue;
        deathManager.death = false;
        timeElapsed = 0f;
    }

    void FixedUpdate()
    {
        if (currentXSpeed >= maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }

        if (currentXSpeed <= -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(new Vector2(-speed, 0), ForceMode2D.Force);
            isMoving = true;
        }
        else if (Input.GetKey("d"))
        {
            rb.AddForce(new Vector2(speed, 0), ForceMode2D.Force);
            isMoving = true;
        }
        else
        {
            rb.AddForce(new Vector2(-currentXSpeed, 0), ForceMode2D.Force);
            isMoving = false;
        }

        if (jump)
        {
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Force);
            jump = false;
        }
        else if (stopJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
            stopJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Void")
        {
            deathManager.isDead = true;
        }
    }
}
