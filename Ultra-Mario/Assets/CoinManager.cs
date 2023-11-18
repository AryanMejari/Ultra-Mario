using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance; // Singleton pattern

    private string coinsKey = "PlayerCoins";
    private int playerCoins = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Load player's coins from PlayerPrefs
        LoadCoins();
    }

    void OnDisable()
    {
        // Save player's coins to PlayerPrefs when the game/application is closed
        SaveCoins();
    }

    public void AddCoins(int amount)
    {
        playerCoins += amount;
        SaveCoins();
    }

    public int GetCoins()
    {
        return playerCoins;
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(coinsKey, playerCoins);
        PlayerPrefs.Save();
    }

    private void LoadCoins()
    {
        playerCoins = PlayerPrefs.GetInt(coinsKey, 0);
    }
}
