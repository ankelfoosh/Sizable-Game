using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravGround : MonoBehaviour
{
    public Player_Move playerMove;
    private PistonBoost pb;
    private Rigidbody2D rb;
    public Transform tf;
    public bool touch = false;

    public Transform switchACheck;
    public float switchACheckRadius;
    public LayerMask switchALayer;
    public bool isTouchingSwitchA;

    public Transform switchBCheck;
    public float switchBCheckRadius;
    public LayerMask switchBLayer;
    public bool isTouchingSwitchB;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pb = GetComponent<PistonBoost>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingSwitchA = Physics2D.OverlapCircle(switchACheck.position, switchACheckRadius, switchALayer);
        isTouchingSwitchB = Physics2D.OverlapCircle(switchBCheck.position, switchBCheckRadius, switchBLayer);

        if (isTouchingSwitchA)
        {
            rb.gravityScale = -4f;
            playerMove.minGravSpeed = 38f;
            if (!pb.isTouchingPiston)
            {
                playerMove.jumpHeight = playerMove.jumpHeightB;
            }
            else if (pb.isTouchingPiston)
            {
            }
            touch = true;
        }
        else if (isTouchingSwitchB)
        {
            rb.gravityScale = 4f;
            playerMove.minGravSpeed = -38f;
            touch = false;
        }
    }
}
