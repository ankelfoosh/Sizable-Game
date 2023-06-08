using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float minGravSpeed;
    private bool isMoving;
    public float currentXSpeed;
    public float currentYSpeed;
    private Rigidbody2D rb;
    public bool canMove;

    public int direction = 1;
    public bool canChange = true;
    public bool smallSize = false;
    public bool mediumSize = true;
    public bool largeSize = false;

    private Transform transform;
    public DeathManager deathManager;
    private Vector3 respawnPoint;

    public Transform spawnCheck;
    public float spawnCheckRadius;
    public LayerMask spawnLayer;
    public bool isTouchingNospawn;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isTouchingGround;
    private bool jump;
    private bool stopJump;
    public float jumpHeight;

    public bool doubleJumpAbility = false;
    private bool canDoubleJump;
    public float doubleJumpHeight;
    public float doubleJumpTierBonus;
    public int doubleJumpTierMax;

    public bool speedUpgrade;
    public float speedTierBonus = 1f;
    public int currentSpeedTier;

    public float DJTier1 = 0.7f;
    public float DJTier2 = 1f;
    public float DJTier3 = 1.3f;
    public int currentDJTier;

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
        doubleJumpTierBonus = 0;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingNospawn = Physics2D.OverlapCircle(spawnCheck.position, spawnCheckRadius, spawnLayer);
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        currentXSpeed = rb.velocity.x;
        currentYSpeed = rb.velocity.y;

        // Double jump statements (boring)

        if (currentDJTier == 0)
        {
            doubleJumpTierBonus = 0;
        }
        else if (currentDJTier == 1)
        {
            doubleJumpTierBonus = DJTier1;
        }
        else if (currentDJTier == 2)
        {
            doubleJumpTierBonus = DJTier2;
        }
        else if (currentDJTier == 3)
        {
            doubleJumpTierBonus = DJTier3;
        }

        if (currentDJTier > doubleJumpTierMax)
        {
            currentDJTier = doubleJumpTierMax;
        }
        
        if (isTouchingGround && !isTouchingNospawn)
        {
            respawnPoint = transform.position;
        }

        if (!smallSize)
        {
            if (Input.GetKeyDown("space") && isTouchingGround)
            {
                jump = true;
            }

            if (Input.GetKeyUp("space") && !isTouchingGround && rb.velocity.y >= 5)
            {
                stopJump = true;
            }
        }

        if (!smallSize)
        {
            if (!isTouchingGround && doubleJumpAbility)
            {
                if (canDoubleJump && Input.GetKeyDown("space"))
                {
                    rb.velocity = new Vector2(rb.velocity.x, doubleJumpHeight * doubleJumpTierBonus);
                    canDoubleJump = false;
                }
            }
            else if (doubleJumpAbility && isTouchingGround)
            {
                canDoubleJump = true;
            }
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
        if (canChange)
        {
            if (Input.GetKey("z"))
            {
                smallSize = true;
                mediumSize = false;
                largeSize = false;
                transform.localScale = new Vector2(0.5f, 0.5f);
                groundCheckRadius = 0.05f;
                spawnCheckRadius = 0.05f;
                jumpHeight = 10f;
                speed = 9f;
                maxSpeed = 10.5f;
            }
            if (Input.GetKey("x"))
            {
                smallSize = false;
                mediumSize = true;
                largeSize = false;
                transform.localScale = new Vector2(1f, 1f);
                groundCheckRadius = 0.1f;
                spawnCheckRadius = 0.1f;
                jumpHeight = 15f;
                speed = 6f;
                maxSpeed = 15f;
            }
            if (Input.GetKey("c"))
            {
                smallSize = false;
                mediumSize = false;
                largeSize = true;
                transform.localScale = new Vector2(2f, 2f);
                groundCheckRadius = 0.1f;
                spawnCheckRadius = 0.1f;
                jumpHeight = 20f;
                speed = 2f;
                maxSpeed = 7f;
            }
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
        if (canMove)
        {
            if (currentYSpeed <= minGravSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, minGravSpeed);
            }

            if (currentXSpeed >= maxSpeed * speedTierBonus)
            {
                rb.velocity = new Vector2(maxSpeed * speedTierBonus, rb.velocity.y);
            }

            if (currentXSpeed <= -maxSpeed * speedTierBonus)
            {
                rb.velocity = new Vector2(-maxSpeed * speedTierBonus, rb.velocity.y);
            }

            if (Input.GetKey("a"))
            {
                rb.AddForce(new Vector2(-speed, 0), ForceMode2D.Force);
                isMoving = true;
                direction = -1;
            }
            else if (Input.GetKey("d"))
            {
                rb.AddForce(new Vector2(speed, 0), ForceMode2D.Force);
                isMoving = true;
                direction = 1;
            }
            else
            {
                rb.AddForce(new Vector2(-currentXSpeed, 0), ForceMode2D.Force);
                isMoving = false;
            }

            if (jump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                jump = false;
            }
            else if (stopJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 5);
                stopJump = false;
            }
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
