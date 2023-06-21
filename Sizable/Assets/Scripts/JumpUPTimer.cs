using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUPTimer : MonoBehaviour
{
    public bool timerCalled = false;
    public bool timerDone = false;

    public float timeElapsed = 0f;
    public float lerpDuration;
    float startValue = 0;
    float endValue = 10;
    float valueToLerp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerCalled)
        {
            if (timeElapsed == 0f)
            {
                StartCoroutine(Lerp());
                timerDone = false;
                timerCalled = false;
            }
            else
            {
                timeElapsed = 0f;
                timerDone = false;
                timerCalled = false;
            }
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
        timerDone = true;
        timeElapsed = 0f;
    }
}
