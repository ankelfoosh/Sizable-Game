using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooberAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public NoticeRadius nr;
    public Player_Move pm;
    public Player_Dash pd;
    private Transform tf;

    private bool follow;
    public bool isMoving = false;

    public Animator anim;

    public Transform radiusCheck;
    public float radiusCheckRadius;
    public LayerMask radiusLayer;
    public bool isTouchingRadius;

    public Transform player;
    public float speed;
    //private Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isMoving", isMoving);
        isTouchingRadius = Physics2D.OverlapCircle(radiusCheck.position, radiusCheckRadius, radiusLayer);

        if (isTouchingRadius && nr.notice && follow)
        {
            isMoving = true;
            rb.velocity = new Vector2(0f, rb.velocity.y);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }

        if (player.position.x > tf.position.x)
        {
            tf.localScale = new Vector2(1, 1);
        }
        else if (player.position.x < tf.position.x)
        {
            tf.localScale = new Vector2(-1, 1);
        }

        if (pd.isDashing)
        {
            follow = false;
            rb.velocity = new Vector2(0f, 0f);
        }
        else
        {
            follow = true;
        }
    }
}
