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
    // private float linearDrag = 0.05f;
    // private float increasedLinearDrag = 0.5f;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float leftThrusterInput;
    private float rightThrusterInput;
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
        leftThrusterInput = Input.GetAxis("Left Thruster");
        rightThrusterInput = Input.GetAxis("Right Thruster");

        // Increase angular drag if both thrusters active
        rb2d.angularDrag = (leftThrusterInput > 0 && rightThrusterInput > 0 ? increasedAngularDrag : angularDrag);

        // Increase linear drag if both thursters active
        // rb2d.drag = (leftThrusterInput > 0 && rightThrusterInput > 0 ? increasedLinearDrag : linearDrag);

        // Handle left thruster input
        if (leftThrusterInput > 0)
        {
            rb2d.AddForceAtPosition(transform.up * (thrusterForce * leftThrusterInput), leftThrusterPosition.transform.position);
        }
        animator.SetBool("Left Thruster Active", leftThrusterInput > 0);

        // Handle right thruster input
        if (rightThrusterInput > 0)
        {
            rb2d.AddForceAtPosition(transform.up * (thrusterForce * rightThrusterInput), rightThrusterPosition.transform.position);
        }
        animator.SetBool("Right Thruster Active", rightThrusterInput > 0);

        // Let the game manager know the player has started the current level
        if ((leftThrusterInput > 0 || rightThrusterInput > 0) && GameManager.instance.hasStartedCurrentLevel == false)
        {
            GameManager.instance.hasStartedCurrentLevel = true;
        }
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

    // Collision detection
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Exit")
        {
            GameManager.instance.LevelComplete();
        }
    }
}
