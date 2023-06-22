using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUPTimer : MonoBehaviour
{
    public Player_Move playerMove;

    public bool timerCalled = false;
    public bool timerDone = false;

    public float time;
    public float timer = -210f;

    public float currentScale;

    public RectTransform bar;

    public float duration;
    private float durTime = 1f;
    public float timeElapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Timer handler

        if (timerCalled)
        {
            timerDone = false;
        }

        if (timeElapsed < duration && timerCalled)
        {
            timeElapsed += durTime * Time.deltaTime;
        }
        else
        {
            timerCalled = false;
            timeElapsed = 0f;
        }

        // Tier handler

        if (timeElapsed == 0f && playerMove.currentJumpTier >= 2)
        {
            timerCalled = true;
            playerMove.currentJumpTier -= 1;
        }
        else if (timeElapsed == 0f && playerMove.currentJumpTier == 1)
        {
            playerMove.jumpUpgrade = false;
            timerCalled = false;
            timerDone = true;
            playerMove.currentJumpTier -= 1;
        }

        // Bar controller

        if (timer > -210f)
        {
            timer -= time * Time.deltaTime;
            currentScale -= 0.1f * Time.deltaTime;
            bar.localPosition = new Vector2(timer, 0);
            bar.localScale = new Vector2(currentScale, 1);
        }
        else
        {
            timer = -210f;
            currentScale = 0f;
            bar.localPosition = new Vector2(timer, 0);
            bar.localScale = new Vector2(currentScale, 1);
        }
    }
}
