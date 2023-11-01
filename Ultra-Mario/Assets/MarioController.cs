using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f; // Adjust the jump force as needed
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get horizontal input (left and right arrow keys or A/D keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the movement vector
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Apply the movement to the Rigidbody
        rb.velocity = movement;

        // Check for a jump input (space bar)
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Apply an upward force to perform a jump
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
