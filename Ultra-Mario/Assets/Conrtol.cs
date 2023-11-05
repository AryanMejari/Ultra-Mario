using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public AudioClip jumpSound; // The jump sound to be played
    public AudioClip deathSound; // The sound to play when colliding with "Gumba"
    public AudioClip coinCollectSound; // The sound to play when collecting a coin
    private Rigidbody2D rb;
    private bool isGrounded;
    private AudioSource audioSource; // Reference to the AudioSource component

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // Initialize the AudioSource component
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0.0f) * moveSpeed;
        rb.velocity = new Vector2(movement.x, rb.velocity.y);

        isGrounded = transform.position.y <= 0.1f;

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        PlayJumpSound();
    }

    private void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    // Detect collisions with "Gumba"
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Gumba")
        {
            PlayDeathSound();
        }
    }

    // Detect collisions with GameObjects tagged as "Coin"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            PlayCoinCollectSound();
        }
    }

    private void PlayDeathSound()
    {
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    private void PlayCoinCollectSound()
    {
        if (coinCollectSound != null)
        {
            audioSource.PlayOneShot(coinCollectSound);
        }
    }
}