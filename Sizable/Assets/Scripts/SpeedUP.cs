using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUP : MonoBehaviour
{
    public Player_Move playerMove;
    public int tier;

    private CircleCollider2D cc;
    private Transform tf;
    public ParticleSystem collect;

    public float ST1 = 1.2f;
    public float ST2 = 1.5f;
    public float ST3 = 1.8f;

    public float timeElapsed = 0f;
    public float lerpDuration;
    float startValue = 0;
    float endValue = 10;
    float valueToLerp;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove.currentSpeedTier == 0)
        {
            playerMove.speedTierBonus = 1f;
        }
        else if (playerMove.currentSpeedTier == 1)
        {
            playerMove.speedTierBonus = ST1;
        }
        else if (playerMove.currentSpeedTier == 2)
        {
            playerMove.speedTierBonus = ST2;
        }
        else if (playerMove.currentSpeedTier == 3)
        {
            playerMove.speedTierBonus = ST3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerMove.currentSpeedTier += tier;
            playerMove.speedUpgrade = true;
            timeElapsed = 0f;
            StartCoroutine(Lerp());
            cc.enabled = false;
            tf.localScale = new Vector2(0f, 0f);
            Collect();
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
        playerMove.speedUpgrade = false;
        playerMove.currentSpeedTier -= tier;
        tf.localScale = new Vector2(1f, 1f);
        cc.enabled = true;
        timeElapsed = 0f;
    }

    void Collect()
    {
        collect.Play();
    }
}
