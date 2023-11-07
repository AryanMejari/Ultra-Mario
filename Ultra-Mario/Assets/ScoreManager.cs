using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance

    public Text scoreText; // Reference to your UI Text component
    private int score = 0; // Current score

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void CollectCoin()
    {
        // Increase the score by 1
        score++;

        // Update the UI text
        if (scoreText != null)
            scoreText.text = "Coins: " + score.ToString();
    }
}