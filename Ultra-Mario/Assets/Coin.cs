using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Set the value of each coin

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify the CoinManager that a coin has been collected and pass the coin value
            CoinManager.instance.AddCoins(coinValue);

            // Destroy the coin when the player collides with it
            Destroy(gameObject);
        }
    }
}
