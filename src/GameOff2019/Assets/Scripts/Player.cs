using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider;

    private bool isJumping = false;
    private float jumpVelocity = 15f;
    private float fallGravityMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool jumpButtonIsPressed = false;

        if (Input.GetKey("space"))
        {
            jumpButtonIsPressed = true;
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
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {

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
