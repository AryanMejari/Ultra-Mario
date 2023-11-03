using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;

    private Rigidbody2D rb;
    private bool isGrounded; // Add a variable to check if the player is on the ground

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get input for horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Calculate the movement vector (only left and right)
        Vector2 movement = new Vector2(moveHorizontal, 0.0f) * moveSpeed;

        // Apply the movement to the Rigidbody2D
        rb.velocity = new Vector2(movement.x, rb.velocity.y);

        // Check if the player is grounded (you should set this based on your ground detection logic)
        // For the example, we'll use a simple check based on the player's Y position
        isGrounded = transform.position.y <= 0.1f;

        // Check if the player can jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Apply an upward force for jumping
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
