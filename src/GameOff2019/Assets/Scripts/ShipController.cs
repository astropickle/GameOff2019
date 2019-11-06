using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public float thrusterForce = 2f;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddForceAtPosition(transform.up, transform.position - new Vector3(-1, -1, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddForceAtPosition(transform.up, transform.position - new Vector3(1, -1, 0));
        }
    }
}
