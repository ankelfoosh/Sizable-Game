using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public bool isDead;
    public bool death;
    public bool done;
    public Animator anim;
    
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
        if (isDead)
        {
            StartCoroutine(Lerp());
            isDead = false;
            death = true;
        }

        anim.SetBool("Dead", death);
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
        done = true;
        timeElapsed = 0f;
    }
}
