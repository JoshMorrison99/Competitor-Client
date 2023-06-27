using UnityEngine;
using UnityEngine.SceneManagement;

public class JWTManager : MonoBehaviour
{
    private const string JWTKey = "JWT";

    private void Awake()
    {
        // Make the "GameManager" object persistent throughout scenes
        DontDestroyOnLoad(gameObject);

        string jwt = PlayerPrefs.GetString(JWTKey);
        if (!string.IsNullOrEmpty(jwt))
        {
            // JWT exists, load Main scene
            Debug.Log(jwt);
            SceneManager.LoadScene("Main");
        }
    }
}

