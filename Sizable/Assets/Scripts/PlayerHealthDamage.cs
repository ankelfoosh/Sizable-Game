using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDamage : MonoBehaviour
{
    public Player_Move pm;
    public Player_Dash pd;
    public DeathManager dm;

    public Transform resetPoint;
    public Transform startPoint;
    public Transform tf;

    public float curHealth;
    public float maxHealth;
    public bool touchedVoid = false;
    private bool hit = false;

    private bool invincible;

    public bool resetPointFound = false;

    public float timeElapsed = 0f;
    public float lerpDuration;
    float startValue = 0;
    float endValue = 10;
    float valueToLerp;

    //SFX

    public AudioSource takeDamageSF;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (touchedVoid)
        {
            curHealth -= maxHealth * 0.50f;
            touchedVoid = false;
        }

        if (curHealth <= 0f)
        {
            dm.isDead = true;
            curHealth = maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ResetPoint")
        {
            resetPointFound = true;
        }

        if (collision.tag == "Enemy" && !invincible && !pd.dashed)
        {
            takeDamageSF.Play();
            Debug.Log("Enemy Touched");
            StartCoroutine(Lerp());

            curHealth -= 5f;
            pm.rb.velocity = new Vector2(36f * -pm.direction, 5f);
            invincible = true;
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
        invincible = false;
        timeElapsed = 0f;
    }
}
