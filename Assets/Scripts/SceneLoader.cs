using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGamePage()
    {
        SceneManager.LoadScene(1); // Load scene with index 1
    }
}