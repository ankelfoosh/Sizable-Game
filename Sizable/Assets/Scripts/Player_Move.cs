using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player_Move : MonoBehaviour
{
    public SaveData saveData;
    public ParticleSystem trail;
    public Transform trailTransform;

    private bool moveTrail = false;
    private bool isTrailing = false;

    // Sprites
    public SpriteRenderer srB;
    public SpriteRenderer sr;

    public Sprite regularB;
    public Sprite ringB;
    public Sprite linesB;
    public Sprite loadingB;
    public Sprite particlesB;
    public Sprite securityB;

    public Sprite regular;
    public Sprite ring;
    public Sprite lines;
    public Sprite loading;
    public Sprite particles;
    public Sprite security;

    public string currentHex;

    public Color curHex;

    public Light2D ballLight;

    // End Sprites

    public float speed;
    public float maxSpeed;
    public float minGravSpeed;
    private bool isMoving = false;
    public float currentXSpeed;
    public float currentYSpeed;
    public Rigidbody2D rb;
    public Rigidbody2D spriteRB;
    public Transform spriteTF;
    public bool canMove = true;

    public int direction = 1;
    public bool canChange = true;
    public bool smallSize = false;
    public bool mediumSize = true;
    public bool largeSize = false;

    private Transform transform;
    public DeathManager deathManager;
    public PlayerHealthDamage phd;
    public Player_Dash pd;
    public SuperJump sj;
    public StickyWallScript stickScript;
    public Vector3 respawnPoint;

    public Transform spawnCheck;
    public float spawnCheckRadius;
    public LayerMask spawnLayer;
    public bool isTouchingNospawn = false;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isTouchingGround = false;
    public bool jump = false;
    private bool stopJump = false;
    public float jumpStopSpeed = 4f;
    public float jumpHeight;
    public float jumpHeightB;

    private bool inAir;

    public Transform groundBCheck;
    public float groundBCheckRadius;
    public LayerMask groundBLayer;
    public bool isTouchingGroundB = false;

    private AntiGravGround aGG;
    public PistonBoost pistonBoost;

    public bool doubleJumpAbility = false;
    private bool canDoubleJump = false;
    private bool doubleJump = false;
    public float doubleJumpHeight;
    public float doubleJumpTierBonus;
    public int doubleJumpTierMax;

    public bool speedUpgrade = false;
    public float speedTierBonus = 1f;
    public int currentSpeedTier;

    public bool jumpUpgrade = false;
    public float jumpTierBonus = 1f;
    public int currentJumpTier;

    public bool featherUpgrade = false;
    public float featherTierBonus = 1f;
    public int currentFeatherTier;

    public float DJTier1 = 0.7f;
    public float DJTier2 = 1f;
    public float DJTier3 = 1.3f;
    public int currentDJTier;

    public enum charSizes
    {
        small,
        medium,
        large
    }

    public charSizes charsize = charSizes.medium;

    public LevelManager levelManager;

    public float timeElapsed = 0f;
    public float lerpDuration;
    float startValue = 0;
    float endValue = 10;
    float valueToLerp;

    //SFX

    public AudioSource jumpSF;
    public AudioSource landSF;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        aGG = GetComponent<AntiGravGround>();
        currentXSpeed = rb.velocity.x;
        currentYSpeed = rb.velocity.y;
        doubleJumpTierBonus = 0;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (saveData.currentBall == 0)
        {
            srB.sprite = regularB;
            sr.sprite = regular;
        }
        else if (saveData.currentBall == 1)
        {
            srB.sprite = ringB;
            sr.sprite = ring;
        }
        else if (saveData.currentBall == 2)
        {
            srB.sprite = linesB;
            sr.sprite = lines;
        }
        else if (saveData.currentBall == 3)
        {
            srB.sprite = loadingB;
            sr.sprite = loading;
        }
        else if (saveData.currentBall == 4)
        {
            srB.sprite = particlesB;
            sr.sprite = particles;
        }
        else if (saveData.currentBall == 5)
        {
            srB.sprite = securityB;
            sr.sprite = security;
        }

        if (saveData.currentColor == 0)
        {
            currentHex = saveData.redHex;
            SetGlobalLightColor(currentHex);
        }
        else if (saveData.currentColor == 1)
        {
            currentHex = saveData.orangeHex;
            SetGlobalLightColor(currentHex);
        }
        else if (saveData.currentColor == 2)
        {
            currentHex = saveData.yellowHex;
            SetGlobalLightColor(currentHex);
        }
        else if (saveData.currentColor == 3)
        {
            currentHex = saveData.limeHex;
            SetGlobalLightColor(currentHex);
        }
        else if (saveData.currentColor == 4)
        {
            currentHex = saveData.greenHex;
            SetGlobalLightColor(currentHex);
        }
        else if (saveData.currentColor == 5)
        {
            currentHex = saveData.lightBlueHex;
            SetGlobalLightColor(currentHex);
        }
        else if (saveData.currentColor == 6)
        {
            currentHex = saveData.blueHex;
            SetGlobalLightColor(currentHex);
        }
        else if (saveData.currentColor == 7)
        {
            currentHex = saveData.darkBlueHex;
            SetGlobalLightColor(currentHex);
        }
        else if (saveData.currentColor == 8)
        {
            currentHex = saveData.purpleHex;
            SetGlobalLightColor(currentHex);
        }
        else if (saveData.currentColor == 9)
        {
            currentHex = saveData.magentaHex;
            SetGlobalLightColor(currentHex);
        }
        else if (saveData.currentColor == 10)
        {
            currentHex = saveData.pinkHex;
            SetGlobalLightColor(currentHex);
        }
        else if (saveData.currentColor == 11)
        {
            currentHex = saveData.whiteHex;
            SetGlobalLightColor(currentHex);
        }

        isTouchingNospawn = Physics2D.OverlapCircle(spawnCheck.position, spawnCheckRadius, spawnLayer);
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTouchingGroundB = Physics2D.OverlapCircle(groundBCheck.position, groundBCheckRadius, groundBLayer);
        currentXSpeed = rb.velocity.x;
        currentYSpeed = rb.velocity.y;

        if (Input.GetKeyDown("a") || Input.GetKeyDown("d"))
        {
            isTrailing = true;
        }
        else
        {

        }

        if (pistonBoost.isTouchingPiston)
        {
            speedTierBonus = 1f;
        }

        if (isTouchingGround && !jump && !pd.isDashing && !sj.isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

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

        if (charsize != charSizes.small)
        {
            minGravSpeed = -38f * featherTierBonus;

            if (Input.GetKeyDown("space") && isTouchingGround || Input.GetKeyDown("space") && isTouchingGroundB || Input.GetKeyDown("space") && pistonBoost.isTouchingPiston)
            {
                jumpSF.Play();
                jump = true;
            }

            if (!aGG.touch)
            {
                if (Input.GetKeyUp("space") && !isTouchingGround && !isTouchingGroundB && rb.velocity.y >= 5 || Input.GetKeyUp("space") && !pistonBoost.isTouchingPiston && rb.velocity.y >= 5)
                {
                    stopJump = true;
                }
            }
            else if (aGG.touch)
            {
                if (Input.GetKeyUp("space") && !isTouchingGround && !isTouchingGroundB && rb.velocity.y <= -5 || Input.GetKeyUp("space") && !pistonBoost.isTouchingPiston && rb.velocity.y <= -5)
                {
                    stopJump = true;
                }
            }

            if (!isTouchingGround && !isTouchingGroundB && !stickScript.isTouchingStick && !pistonBoost.isTouchingPiston && doubleJumpAbility)
            {
                if (canDoubleJump && Input.GetKeyDown("space"))
                {
                    doubleJump = true;
                    canDoubleJump = false;
                }
            }
            else if (doubleJumpAbility && isTouchingGround || doubleJumpAbility && isTouchingGroundB || doubleJumpAbility && stickScript.isTouchingStick || doubleJumpAbility && pistonBoost.isTouchingPiston)
            {
                canDoubleJump = true;
            }
        }
        else
        {
            minGravSpeed = -18f * featherTierBonus;
        }

        if (!isTouchingGround)
        {
            inAir = true;
        }

        if (isTouchingGround && rb.velocity.y <= 0f && inAir || isTouchingGroundB && rb.velocity.y <= 0f && inAir)
        {
            landSF.Play();
            inAir = false;
        }

        if (rb.velocity.x <= 0.7f && !isMoving && rb.velocity.x >= -0.7f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (deathManager.done)
        {
            rb.velocity = new Vector2(0f, 0f);

            if (phd.resetPointFound)
            {
                phd.tf.position = phd.resetPoint.position;
            }
            else
            {
                phd.tf.position = phd.startPoint.position;
            }

            deathManager.done = false;
            StartCoroutine(Lerp());
        }

        // Size Changing
        if (canChange)
        {
            if (Input.GetKey("z"))
            {
                charsize = charSizes.small;
                transform.localScale = new Vector2(0.5f, 0.5f);
                groundCheckRadius = 0.05f;
                spawnCheckRadius = 0.05f;
                jumpHeight = 10f;
                jumpHeightB = -10f;
                speed = 9f;
                maxSpeed = 10.5f;
            }
            if (Input.GetKey("x"))
            {
                charsize = charSizes.medium;
                transform.localScale = new Vector2(1f, 1f);
                groundCheckRadius = 0.1f;
                spawnCheckRadius = 0.1f;
                jumpHeight = 17f;
                jumpHeightB = -17f;
                speed = 6f;
                maxSpeed = 15f;
            }
            if (Input.GetKey("c"))
            {
                charsize = charSizes.large;
                transform.localScale = new Vector2(2f, 2f);
                groundCheckRadius = 0.1f;
                spawnCheckRadius = 0.1f;
                jumpHeight = 21f;
                jumpHeightB = -21f;
                speed = 2f;
                maxSpeed = 8f;
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
            if (currentYSpeed <= minGravSpeed && !aGG.touch)
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
                rb.AddForce(new Vector2(-speed * speedTierBonus, 0), ForceMode2D.Force);
                isMoving = true;
                direction = -1;
            }
            else if (Input.GetKey("d"))
            {
                rb.AddForce(new Vector2(speed * speedTierBonus, 0), ForceMode2D.Force);
                isMoving = true;
                direction = 1;
            }
            else
            {
                rb.AddForce(new Vector2(-currentXSpeed, 0), ForceMode2D.Force);
                isMoving = false;
            }

            if (doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, (doubleJumpHeight * doubleJumpTierBonus) * jumpTierBonus);
                doubleJump = false;
            }
            else if (jump)
            {
                inAir = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight * jumpTierBonus);
                jump = false;
            }
            else if (stopJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpStopSpeed);
                stopJump = false;
            }

            if (rb.gravityScale > 0)
            {
                spriteRB.rotation -= rb.velocity.x * (2f / transform.localScale.x);
            }
            else
            {
                spriteRB.rotation += rb.velocity.x * (2f / transform.localScale.x);
            }

            // Trail stuff

            if (isTrailing)
            {
                moveTrail = true;
                isTrailing = false;
            }
        }

        if (rb.velocity.x > 0.1f && moveTrail || rb.velocity.x < -0.1f && moveTrail)
        {
            Debug.Log("Started Trail");
            trail.Play();
            moveTrail = false;
        }
        else if (rb.velocity.x == 0f && !moveTrail)
        {
            Debug.Log("Stopped Trail");
            trail.Stop();
        }
        if (rb.velocity.x > 0.1f && moveTrail || rb.velocity.x < -0.1f && moveTrail)
        {
            Debug.Log("Started Trail");
            trail.Play();
            moveTrail = false;
        }

        spriteTF.localPosition = transform.localPosition - transform.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Void")
        {
            rb.velocity = new Vector2(0f, 0f);
            transform.position = respawnPoint;
            phd.touchedVoid = true;
        }

        if (collision.tag == "Finish")
        {
            levelManager.active = true;
        }
    }

    private void SetGlobalLightColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out curHex))
        {
            ballLight.color = curHex;
        }
        else
        {
            Debug.LogError("Invaled Hex Color");
        }
    }
}
