using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    public Player_Move playerMove;
    public PistonBoost pistonBoost;
    private Rigidbody2D rb;

    public float jumpCharge;
    public float jumpTime;
    public float jumpHeight;
    public float maxJumpHeight;
    private bool isJumping;
    public float scaleMultiplier = 0.04f;

    public RectTransform bar;
    public GameObject chargeBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bar.localScale = new Vector2(0, 1);
        chargeBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove.charsize == Player_Move.charSizes.small)
        {
            if (Input.GetKey("space"))
            {
                chargeBar.SetActive(true);

                if (jumpCharge >= maxJumpHeight)
                {
                    jumpCharge = maxJumpHeight;
                }
                else
                {
                    jumpCharge += jumpTime * Time.deltaTime;
                    bar.localScale = new Vector2(scaleMultiplier * jumpCharge, 1);
                }
            }

            if (Input.GetKeyUp("space"))
            {
                jumpHeight = jumpCharge;
                bar.localScale = new Vector2(0, 1);
                chargeBar.SetActive(false);

                if (playerMove.isTouchingGround || pistonBoost.isTouchingPiston)
                {
                    isJumping = true;
                }

                jumpCharge = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            isJumping = false;
        }
    }
}
