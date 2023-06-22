using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherfallUP : MonoBehaviour
{
    public Player_Move playerMove;
    public FeatherfallUPTimer timer;
    public bool done = false;
    public int tier;

    private CircleCollider2D cc;
    private Transform tf;
    public ParticleSystem collect;

    public float FT1 = 0.8f;
    public float FT2 = 0.5f;
    public float FT3 = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cc.enabled)
        {
            done = false;
        }
        else
        {
            done = true;
        }

        if (timer.timerDone && done)
        {
            tf.localScale = new Vector2(1f, 1f);
            cc.enabled = true;
        }

        if (playerMove.currentFeatherTier == 0)
        {
            playerMove.featherTierBonus = 1f;
        }
        else if (playerMove.currentFeatherTier == 1)
        {
            playerMove.featherTierBonus = FT1;
        }
        else if (playerMove.currentFeatherTier == 2)
        {
            playerMove.featherTierBonus = FT2;
        }
        else if (playerMove.currentFeatherTier == 3)
        {
            playerMove.featherTierBonus = FT3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerMove.currentFeatherTier += tier;
            playerMove.featherUpgrade = true;
            timer.timerCalled = true;
            cc.enabled = false;
            tf.localScale = new Vector2(0f, 0f);
            Collect();
        }
    }

    void Collect()
    {
        collect.Play();
    }
}
