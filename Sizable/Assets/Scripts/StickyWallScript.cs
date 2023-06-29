using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyWallScript : MonoBehaviour
{
    public Transform stickCheck;
    public float stickCheckRadius;
    public LayerMask stickLayer;
    public bool isTouchingStick;

    private Rigidbody2D rb;
    public Player_Move playerMove;
    private AntiGravGround aGG;

    private bool jump;
    private float horizontalSpeed;
    public float maxHorizontalSpeed;
    public float verticalSpeed;
    public float charge;
    public float jumpTime;

    public RectTransform bar;
    public GameObject chargeBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aGG = GetComponent<AntiGravGround>();
        bar.transform.localPosition = new Vector3(0f, 0f, 0f);
        chargeBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingStick = Physics2D.OverlapCircle(stickCheck.position, stickCheckRadius, stickLayer);

        if (isTouchingStick && playerMove.charsize != Player_Move.charSizes.small)
        {
            chargeBar.SetActive(true);
            playerMove.canChange = false;
            playerMove.canMove = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0f, 0f);

            if (Input.GetKey("a") && charge >= -maxHorizontalSpeed)
            {
                charge -= jumpTime * Time.deltaTime;
                bar.transform.localPosition = new Vector3(8.582500001f * charge, 0f, 0f);

                if (charge <= -maxHorizontalSpeed)
                {
                    charge = -maxHorizontalSpeed;
                }
            }
            else if (Input.GetKey("d") && charge <= maxHorizontalSpeed)
            {
                charge += jumpTime * Time.deltaTime;
                bar.transform.localPosition = new Vector3(8.582500001f * charge, 0f, 0f);

                if (charge >= maxHorizontalSpeed)
                {
                    charge = maxHorizontalSpeed;
                }
            }
        }
        else if (!aGG.touch)
        {
            bar.transform.localPosition = new Vector3(0f, 0f, 0f);
            chargeBar.SetActive(false);
            rb.gravityScale = 4f;
        }

        if (Input.GetKeyDown("space") && isTouchingStick && playerMove.charsize != Player_Move.charSizes.small)
        {
            horizontalSpeed = charge;
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (isTouchingStick && jump)
        {
            rb.gravityScale = 4f;
            rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
            charge = 0f;
            playerMove.canMove = true;
            playerMove.canChange = true;
            jump = false;
        }
    }
}
