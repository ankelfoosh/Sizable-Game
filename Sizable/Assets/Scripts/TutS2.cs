using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutS2 : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject t1;
    public GameObject t2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TutCP1")
        {
            t1.SetActive(false);
            t2.SetActive(true);
        }

        if (collision.tag == "TutCP2")
        {
            t1.SetActive(false);
            t2.SetActive(false);
        }
    }
}
