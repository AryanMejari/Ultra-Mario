using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLeveScene()
    {
        SceneManager.LoadScene("level");
    }

    public void ExitScene()
    {
        // Get the name of the current scene
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Load the current scene, effectively restarting it
        SceneManager.LoadScene(currentSceneName);
    }
}
