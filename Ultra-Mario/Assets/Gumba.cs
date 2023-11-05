using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumbaMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public Transform leftBoundary;
    public Transform rightBoundary;

    private bool movingRight = true;

    private void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        // Check if Gumba reached its left or right boundary
        if (transform.position.x >= rightBoundary.position.x)
        {
            movingRight = false;
        }
        else if (transform.position.x <= leftBoundary.position.x)
        {
            movingRight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe"))
        {
            // Change direction when colliding with objects tagged as "Pipe"
            movingRight = !movingRight;
        }
    }
}
