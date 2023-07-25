using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonBoost : MonoBehaviour
{
    public Player_Move playerMove;
    public SuperJump superJump;
    private AntiGravGround aGG;

    private Rigidbody2D rb;
    public float multiplier;

    public Transform pistonCheck;
    public float pistonCheckRadius;
    public LayerMask pistonLayer;
    public bool isTouchingPiston = false;
    private bool trig = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aGG = GetComponent<AntiGravGround>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingPiston = Physics2D.OverlapCircle(pistonCheck.position, pistonCheckRadius, pistonLayer);

        if (isTouchingPiston && !aGG.touch)
        {
            trig = true;
            playerMove.jumpHeight = multiplier;
            superJump.jumpTime = (multiplier * 1.12f) * 0.8f;
            superJump.maxJumpHeight = multiplier * 1.12f;
            superJump.scaleMultiplier = ((multiplier * 1.12f) / (multiplier * 1.12f)) / (multiplier * 1.12f);
        }
        else if (isTouchingPiston && aGG.touch)
        {
            trig = true;
            playerMove.jumpHeight = -multiplier;
            superJump.jumpTime = (-multiplier * 1.12f) * 0.8f;
            superJump.maxJumpHeight = -multiplier * 1.12f;
            superJump.scaleMultiplier = ((-multiplier * 1.12f) / (-multiplier * 1.12f)) / (-multiplier * 1.12f);
        }
        else if (!isTouchingPiston && !aGG.isTouchingSwitchA && !aGG.isTouchingSwitchB && trig)
        {
            superJump.jumpTime = 20f;
            superJump.maxJumpHeight = 25f;
            superJump.scaleMultiplier = 0.04f;
            
            if (playerMove.charsize == Player_Move.charSizes.medium)
            {
                playerMove.jumpHeight = 17f;
                playerMove.jumpHeightB = -17f;
            }
            else if (playerMove.charsize == Player_Move.charSizes.large)
            {
                playerMove.jumpHeight = 21f;
                playerMove.jumpHeightB = -21f;
            }

            trig = false;
        }
    }
}
