using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevelScene()
    {
        SceneManager.LoadScene("Level");
    }

    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void QuitGame()
    {
        // This will quit the application (game) when running in standalone mode (e.g., PC, Mac).
        // Note: This won't work in the Unity Editor or certain platforms like web builds.
        Application.Quit();
    }
}
