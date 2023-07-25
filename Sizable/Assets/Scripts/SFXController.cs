using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public Player_Move pm;

    public AudioSource jumpSF;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.jump)
        {
            //jumpSF.Play();
        }
        else
        {
            //jumpSF.Stop();
        }
    }
}
