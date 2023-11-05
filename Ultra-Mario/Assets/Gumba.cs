using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumbaMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float moveDistance = 8.0f; // The distance Gumba should move to the left and right

    private int direction = 1; // 1 for right, -1 for left
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        // Check if Gumba reached the desired distance to change direction
        if (Mathf.Abs(transform.position.x - originalPosition.x) >= moveDistance)
        {
            direction = -direction; // Change direction
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe"))
        {
            direction = -direction; // Change direction when colliding with objects tagged as "Pipe"
        }
    }
}
