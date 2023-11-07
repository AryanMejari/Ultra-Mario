using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify the ScoreManager that a coin has been collected
            ScoreManager.instance.CollectCoin();

            // Destroy the coin when the player collides with it
            Destroy(gameObject);
        }
    }
}