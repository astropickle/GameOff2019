using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float fullJumpHeightInUnits = 1.8f;
    public float baseRunVelocity = 2f;
    public float fallGravityMultiplier = 2.5f;
    public float lowJumpGravityMultiplier = 2f;

    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider;

    private bool jumpButtonIsPressed = false;
    private bool isJumping = false;
    private float jumpVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        // TODO: Do fancy maths to caltulate the exact required velocity to jump X units.
        jumpVelocity = fullJumpHeightInUnits;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            jumpButtonIsPressed = true;
        }
        else
        {
            jumpButtonIsPressed = false;
        }

        // Check if the player needs to jump
        if (isJumping == false && jumpButtonIsPressed)
        {
            jump();
        }

        // Check if the player is falling
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallGravityMultiplier - 1) * Time.deltaTime;
        }

        // Check if the player is moving upwards
        else if (rb2d.velocity.y > 0 && jumpButtonIsPressed == false)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpGravityMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // TODO: Replace this run with something better
        rb2d.velocity = new Vector2(baseRunVelocity, rb2d.velocity.y);
    }

    private void jump()
    {
        rb2d.AddForce(new Vector2(0, jumpVelocity), ForceMode2D.Impulse);

        isJumping = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }
}
