using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    public TMP_Text coinText;

    void Start()
    {
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        // Get the player's current coin count from CoinManager
        int playerCoins = CoinManager.instance.GetCoins();

        // Update the text of the TextMeshPro UI element
        coinText.text = "Coins: " + playerCoins.ToString();
    }
}
