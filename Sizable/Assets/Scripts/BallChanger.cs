using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallChanger : MonoBehaviour
{
    public SaveData saveData;

    public Dropdown ballSelector;
    public SpriteRenderer srB;
    public SpriteRenderer sr;

    private int ballValue;

    public Sprite regularB;
    public Sprite ringB;
    public Sprite linesB;
    public Sprite loadingB;
    public Sprite particlesB;
    public Sprite securityB;

    public Sprite regular;
    public Sprite ring;
    public Sprite lines;
    public Sprite loading;
    public Sprite particles;
    public Sprite security;

    // Start is called before the first frame update
    void Start()
    {
        ballSelector.value = saveData.currentBall;
    }

    // Update is called once per frame
    void Update()
    {
        ballValue = ballSelector.value;

        if (ballValue == 0)
        {
            saveData.currentBall = 0;
        }
        else if (ballValue == 1)
        {
            saveData.currentBall = 1;
        }
        else if (ballValue == 2)
        {
            saveData.currentBall = 2;
        }
        else if (ballValue == 3)
        {
            saveData.currentBall = 3;
        }
        else if (ballValue == 4)
        {
            saveData.currentBall = 4;
        }
        else if (ballValue == 5)
        {
            saveData.currentBall = 5;
        }

        // Saves

        if (saveData.currentBall == 0)
        {
            sr.sprite = regular;
            srB.sprite = regularB;
        }
        else if (saveData.currentBall == 1)
        {
            sr.sprite = ring;
            srB.sprite = ringB;
        }
        else if (saveData.currentBall == 2)
        {
            sr.sprite = lines;
            srB.sprite = linesB;
        }
        else if (saveData.currentBall == 3)
        {
            sr.sprite = loading;
            srB.sprite = loadingB;
        }
        else if (saveData.currentBall == 4)
        {
            sr.sprite = particles;
            srB.sprite = particlesB;
        }
        else if (saveData.currentBall == 5)
        {
            sr.sprite = security;
            srB.sprite = securityB;
        }
    }
}
