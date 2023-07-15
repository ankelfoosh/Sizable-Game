using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager01 : MonoBehaviour
{
    public SwitchScript ss1;
    public SwitchScript ss2;
    public SwitchScript ss3;

    public Transform door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ss1.active && ss2.active && ss3.active)
        {
            door.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            door.localScale = new Vector3(1, 4.2f, 1);
        }
    }
}
