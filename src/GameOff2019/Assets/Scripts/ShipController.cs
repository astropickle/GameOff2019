using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public float thrusterForce = 2f;
    public float angularDrag = 0.05f;
    public float increasedAngularDrag = 2f;

    public GameObject leftThrusterPosition;
    public  GameObject rightThrusterPosition;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private bool leftThrusterActive = false;
    private bool rightThrusterActive = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input for thrusters
        leftThrusterActive = Input.GetAxis("Left Thruster") > 0;
        rightThrusterActive = Input.GetAxis("Right Thruster") > 0;

        // Add force for thrusters
        if (leftThrusterActive)
        {
            rb2d.AddForceAtPosition(transform.up, leftThrusterPosition.transform.position);
        }

        if (rightThrusterActive)
        {
            rb2d.AddForceAtPosition(transform.up, rightThrusterPosition.transform.position);
        }

        // Increase angular drag if both thrusters active
        rb2d.angularDrag = (leftThrusterActive && rightThrusterActive ? increasedAngularDrag : angularDrag);
    }

    void FixedUpdate()
    {
        CheckIfPlayerIsOffScreen();
    }

    private void CheckIfPlayerIsOffScreen()
    {
        // Top
        if (transform.position.y > GameManager.instance.halfScreenHeight + spriteRenderer.bounds.extents.y)
        {
            transform.position = new Vector3(transform.position.x, -GameManager.instance.halfScreenHeight - spriteRenderer.bounds.extents.y);
        }

        // Bottom
        else if (transform.position.y < -GameManager.instance.halfScreenHeight - spriteRenderer.bounds.extents.y)
        {
            transform.position = new Vector3(transform.position.x, GameManager.instance.halfScreenHeight + spriteRenderer.bounds.extents.y);
        }

        // Right
        if (transform.position.x > GameManager.instance.halfScreenWidth + spriteRenderer.bounds.extents.x)
        {
            transform.position = new Vector3(-GameManager.instance.halfScreenWidth - spriteRenderer.bounds.extents.x, transform.position.y);
        }

        // Left
        else if (transform.position.x < -GameManager.instance.halfScreenWidth - spriteRenderer.bounds.extents.x)
        {
            transform.position = new Vector3(GameManager.instance.halfScreenWidth + spriteRenderer.bounds.extents.x, transform.position.y);
        }
    }
}
