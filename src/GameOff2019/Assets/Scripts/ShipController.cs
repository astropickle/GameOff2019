using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public float thrusterForce = 2f;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    void FixedUpdate()
    {
        // Check if player has gone off the screen 
        //// Top
        //if (transform.position.y > 12f)
        //{
        //    transform.position = new Vector3(transform.position.x, (-transform.position.y) - spriteRenderer.bounds.size.y);
        //}
        //// Bottom
        //else if (transform.position.y < -12f)
        //{
        //    transform.position = new Vector3(transform.position.x, (-transform.position.y) + spriteRenderer.bounds.size.y);
        //}
        //// Left
        //else if (transform.position.x < -20f)
        //{
        //    transform.position = new Vector3(-transform.position.x + spriteRenderer.bounds.size.x, transform.position.y);
        //}
        //// Right
        //else if (transform.position.x > 20f)
        //{
        //    transform.position = new Vector3(-transform.position.x - spriteRenderer.bounds.size.x, transform.position.y);
        //}

        //if (transform.position.y > 12f || transform.position.y < -12f)
        //{
        //    transform.position = new Vector3(transform.position.x, (-transform.position.y) + -(Math.Sign(transform.position.y) * (spriteRenderer.bounds.size.y)));
        //}
    }
}
