using UnityEngine.SceneManagement;
using UnityEngine;

public class Startup : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MenuScene");
    }
}
