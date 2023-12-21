using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Player movement speed
    public float jumpForce = 10.0f; // Force applied when the player jumps
    public AudioClip jumpSound; // Sound played when jumping

    private Rigidbody2D rb; // Reference to the player's Rigidbody component
    private bool isGrounded; // Flag to check if the player is grounded
    private AudioSource audioSource; // Reference to the AudioSource component
    private int points = 0; // Player's points
    public TextMeshProUGUI pointsText; // Reference to the UI text displaying points

    private bool isGameOver = false; // Flag to check if the game is over
    private bool canJump = true; // Flag to control consecutive jumps

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component of the player
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component of the player
        UpdatePointsText(); // Update the UI text displaying points
    }

    private void Update()
    {
        if (isGameOver) return; // If the game is over, stop updating

        HandleTouchInput(); // Handle touch input for mobile controls

        float moveHorizontal = Input.GetAxis("Horizontal"); // Get input for horizontal movement
        Vector2 movement = new Vector2(moveHorizontal, 0.0f) * moveSpeed; // Calculate movement based on input
        rb.velocity = new Vector2(movement.x, rb.velocity.y); // Apply movement to the player

        CheckGroundedStatus(); // Check if the player is grounded

        // Handle jumping when the player is grounded and the jump button is pressed
        if (isGrounded && Input.GetButtonDown("Jump") && canJump)
        {
            Jump(); // Perform a jump
            canJump = false; // Prevent consecutive jumps
            StartCoroutine(EnableJumpAfterDelay(1.3f)); // Enable jump after a delay
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0) // Check if there is any touch input
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            float screenHeight = Screen.height;
            float screenWidth = Screen.width;
            float upperHalf = screenHeight * 0.75f; // Upper portion for jumping
            float leftThird = screenWidth * 0.25f; // Left third for moving left

            if (touch.phase == TouchPhase.Began) // Check if the touch has just started
            {
                Vector2 touchPos = touch.position; // Get the touch position

                // Jump action: if touch is in the upper portion of the screen
                if (touchPos.y > upperHalf && canJump)
                {
                    Jump(); // Perform a jump
                    canJump = false; // Prevent consecutive jumps
                    StartCoroutine(EnableJumpAfterDelay(1.3f)); // Enable jump after a delay
                }
                // Left movement: if touch is in the left third of the screen
                else if (touchPos.x < leftThird)
                {
                    MoveCharacter(-1); // Move the player left
                }
                // Right movement: if touch is in the right portion of the screen
                else if (touchPos.x > leftThird * 2)
                {
                    MoveCharacter(1); // Move the player right
                }
            }
        }
    }

    private void MoveCharacter(float direction)
    {
        Vector2 movement = new Vector2(direction * moveSpeed, rb.velocity.y); // Calculate movement
        rb.velocity = movement; // Apply movement to the player
    }

    private IEnumerator EnableJumpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        canJump = true; // Enable jumping after the delay
    }

    private void CheckGroundedStatus()
    {
        isGrounded = transform.position.y <= 0.1f; // Check if the player is close to the ground
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Apply upward force for jumping
        PlayJumpSound(); // Play the jump sound
    }

    private void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound); // Play the jump sound
        }
    }

    // Other methods for collision handling, audio, points, etc.

    private void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + points.ToString(); // Update the UI text with points
        }
    }

    private IEnumerator RestartLevelAfterDelay(float delay)
    {
        isGameOver = true; // Set game over flag

        yield return new WaitForSeconds(delay); // Wait for the specified delay

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the level
    }
}
