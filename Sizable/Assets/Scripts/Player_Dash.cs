using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dash : MonoBehaviour
{
    public Player_Move playerMove;
    private Rigidbody2D rb;

    public float dashSpeed;
    public bool isDashing = false;
    public float dashTime;
    public float cooldown;
    private bool canDash = true;

    public float timeElapsed = 0f;
    public float lerpDuration;
    float startValue = 0;
    float endValue = 10;
    float valueToLerp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lerpDuration = dashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("f") && canDash && playerMove.mediumSize)
        {
            StartCoroutine(Lerp());
            rb.velocity = new Vector2(rb.velocity.x, 5f);
            isDashing = true;
            canDash = false;
            playerMove.canMove = false;
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
        playerMove.canMove = true;
        isDashing = false;
        lerpDuration = cooldown;
        StartCoroutine(SecondLerp());
    }
    IEnumerator SecondLerp()
    {
        timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        valueToLerp = endValue;
        canDash = true;
        lerpDuration = dashTime;
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = new Vector2(dashSpeed * playerMove.direction, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" && isDashing)
        {
            isDashing = false;
            playerMove.canMove = true;
        }
    }
}
