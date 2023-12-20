using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public float rotationSpeed = 100.0f;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip coinCollectSound;

    private Rigidbody2D rb;
    private bool isGrounded;
    private AudioSource audioSource;
    private int points = 0;
    public TextMeshProUGUI pointsText;

    private bool isGameOver = false;
    private bool canJump = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        UpdatePointsText();
    }

    private void Update()
    {
        if (isGameOver) return;

        HandleTouchInput();

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0.0f) * moveSpeed;
        rb.velocity = new Vector2(movement.x, rb.velocity.y);

        CheckGroundedStatus();

        if (isGrounded && Input.GetButtonDown("Jump") && canJump)
        {
            Jump();
            canJump = false;
            StartCoroutine(EnableJumpAfterDelay(1.3f));
        }

        RotatePlayer(moveHorizontal);
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Define screen areas for actions: upper half for jumping, lower half for left/right movement
            float screenHeight = Screen.height;
            float screenWidth = Screen.width;
            float upperHalf = screenHeight * 0.5f;

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPos = touch.position;

                // Jump action: if touch is in the upper half of the screen
                if (touchPos.y > upperHalf && canJump)
                {
                    Jump();
                    canJump = false;
                    StartCoroutine(EnableJumpAfterDelay(1.3f));
                }
                // Movement action: if touch is in the lower half of the screen
                else if (touchPos.y <= upperHalf)
                {
                    // Left movement: if touch is on the left side of the lower half
                    if (touchPos.x < screenWidth * 0.5f)
                    {
                        MoveCharacter(-1);
                    }
                    // Right movement: if touch is on the right side of the lower half
                    else
                    {
                        MoveCharacter(1);
                    }
                }
            }
        }
    }

    private void MoveCharacter(float direction)
    {
        Vector2 movement = new Vector2(direction * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    private void RotatePlayer(float moveHorizontal)
    {
        float rotationAmount = -moveHorizontal * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationAmount);
    }

    private IEnumerator EnableJumpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canJump = true;
    }

    private void CheckGroundedStatus()
    {
        isGrounded = transform.position.y <= 0.1f;
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

    // Other methods for collisions, audio, points, etc.
}
