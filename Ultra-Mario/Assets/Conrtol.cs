using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using UnityEngine.SceneManagement; // Import the SceneManagement namespace

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip coinCollectSound;
    private Rigidbody2D rb;
    private bool isGrounded;
    private AudioSource audioSource;
    private int points = 0; // Added to keep track of points
    public TextMeshProUGUI pointsText; // Reference to a TextMeshPro Text component

    private bool isGameOver = false; // To prevent further input after the game is over

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        UpdatePointsText();
    }

    private void Update()
    {
        if (isGameOver) return; // Don't accept input when the game is over

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0.0f) * moveSpeed;
        rb.velocity = new Vector2(movement.x, rb.velocity.y);

        CheckGroundedStatus(); // Added to check if the player is grounded

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void CheckGroundedStatus()
    {
        isGrounded = transform.position.y <= 0.1f; // Check if the player's y-position is close to the ground
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Gumba")
        {
            PlayDeathSound();
            StartCoroutine(RestartLevelAfterDelay(2.0f)); // Restart the level with a delay of 2 seconds
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            PlayCoinCollectSound();
            IncreasePoints(1); // Increase points when the player collects a coin
            Destroy(other.gameObject); // Destroy the collected coin
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

    private void IncreasePoints(int amount)
    {
        points += amount;
        UpdatePointsText();
    }

    private void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + points.ToString();
        }
    }

    private IEnumerator RestartLevelAfterDelay(float delay)
    {
        isGameOver = true; // Prevent further input

        yield return new WaitForSeconds(delay);

        // Reload the current scene to restart the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

