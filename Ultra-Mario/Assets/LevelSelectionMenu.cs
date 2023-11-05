using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level1"); // Load the "Level1" scene.
    }
}
