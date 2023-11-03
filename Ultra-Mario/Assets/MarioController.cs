using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float moveSpeed = 5f; // Added movement speed
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;

    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input

        // Handle horizontal movement
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimer < jumpTime)
            {
                rb.velocity = new Vector2(horizontalInput * moveSpeed, jumpForce);

                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpTimer = 0;
        }
    }
}
