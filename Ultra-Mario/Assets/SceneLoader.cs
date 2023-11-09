using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLeveScene()
    {
        SceneManager.LoadScene("level");
    }
}
