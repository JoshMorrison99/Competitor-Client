using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UserManager : MonoBehaviour
{

    public string username;
    public string email;

    private const string JWTKey = "JWT";


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("User Manager");
        StartCoroutine(fetchUserRunsCoroutine());

    }

    private IEnumerator fetchUserRunsCoroutine()
    {
        string url = "http://localhost:5000/api/user/run"; // Replace with your server's URL

        string jwt = PlayerPrefs.GetString(JWTKey);
        if (!string.IsNullOrEmpty(jwt))
        {
            Debug.Log(jwt);
            string authorizationHeader = "Bearer " + jwt; // Attach the JWT to the 'Authorization' header


            UnityWebRequest request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Authorization", authorizationHeader);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Run request successful");
                // Process the response as needed
            }
            else
            {
                Debug.Log("Run request failed: " + request.error);
                // Handle error as needed
            }
        }
        else
        {
            Debug.LogError("NO JWT!");
        }
    }

}
