using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHeightUP : MonoBehaviour
{
    public Player_Move playerMove;
    public JumpUPTimer timer;
    public bool done = false;
    public int tier;

    private CircleCollider2D cc;
    private Transform tf;
    public ParticleSystem collect;

    public float JHT1 = 1.2f;
    public float JHT2 = 1.5f;
    public float JHT3 = 1.8f;

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
            playerMove.jumpUpgrade = false;
            playerMove.currentJumpTier -= playerMove.currentJumpTier;
            tf.localScale = new Vector2(1f, 1f);
            cc.enabled = true;
        }

        if (playerMove.currentJumpTier == 0)
        {
            playerMove.jumpTierBonus = 1f;
        }
        else if (playerMove.currentJumpTier == 1)
        {
            playerMove.jumpTierBonus = JHT1;
        }
        else if (playerMove.currentJumpTier == 2)
        {
            playerMove.jumpTierBonus = JHT2;
        }
        else if (playerMove.currentJumpTier == 3)
        {
            playerMove.jumpTierBonus = JHT3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerMove.currentJumpTier += tier;
            playerMove.jumpUpgrade = true;
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
