using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class SwitchScript : MonoBehaviour
{
    public Light2D indicatorLight;
    public Rigidbody2D rb;
    public bool active = false;

    public string offColorHex = "4FFF7F";
    public string onColorHex = "FF4F4F";

    Color offHex;
    Color onHex;

    public Transform playerCheck;
    public float playerCheckRadius;
    public LayerMask playerLayer;
    public bool isTouchingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        ColorUtility.TryParseHtmlString(offColorHex, out offHex);
        ColorUtility.TryParseHtmlString(onColorHex, out onHex);
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingPlayer = Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, playerLayer);

        if (isTouchingPlayer)
        {
            if (Input.GetKeyDown("e") && !active)
            {
                active = true;
            }
            else if (Input.GetKeyDown("e") && active)
            {
                active = false;
            }
        }

        if (active)
        {
            rb.rotation = -50f;
            indicatorLight.intensity = 15f;
        }
        else if (!active)
        {
            rb.rotation = 50f;
            indicatorLight.intensity = 0.6f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
