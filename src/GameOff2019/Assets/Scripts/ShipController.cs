using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject leftThrusterPosition;
    public  GameObject rightThrusterPosition;

    private float thrusterForce = 2f;
    private float angularDrag = 0.05f;
    private float increasedAngularDrag = 2f;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool leftThrusterActive = false;
    private bool rightThrusterActive = false;
    private bool playerIsOffScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input for thrusters
        leftThrusterActive = Input.GetAxis("Left Thruster") > 0;
        rightThrusterActive = Input.GetAxis("Right Thruster") > 0;

        // Add force for thrusters
        // TODO: Optimize this, its pointless to set bools to false every single loop
        if (leftThrusterActive)
        {
            rb2d.AddForceAtPosition(transform.up * thrusterForce, leftThrusterPosition.transform.position);
            animator.SetBool("Left Thruster Active", true);
        }
        else
        {
            animator.SetBool("Left Thruster Active", false);
        }

        if (rightThrusterActive)
        {
            rb2d.AddForceAtPosition(transform.up * thrusterForce, rightThrusterPosition.transform.position);
            animator.SetBool("Right Thruster Active", true);
        }
        else
        {
            animator.SetBool("Right Thruster Active", false);
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
            playerIsOffScreen = true;
        }

        // Bottom
        else if (transform.position.y < -GameManager.instance.halfScreenHeight - spriteRenderer.bounds.extents.y)
        {
            playerIsOffScreen = true;
        }

        // Right
        if (transform.position.x > GameManager.instance.halfScreenWidth + spriteRenderer.bounds.extents.x)
        {
            playerIsOffScreen = true;
        }

        // Left
        else if (transform.position.x < -GameManager.instance.halfScreenWidth - spriteRenderer.bounds.extents.x)
        {
            playerIsOffScreen = true;
        }

        if (playerIsOffScreen)
        {
            GameManager.instance.GameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Exit")
        {
            GameManager.instance.LevelComplete();
        }
    }
}
