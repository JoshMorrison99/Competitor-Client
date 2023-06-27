using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AccountLogin : MonoBehaviour
{
    public TMP_InputField inputfield_email;
    public TMP_InputField inputfield_password;

    public TMP_Text text_email_error;

    private void Start()
    {
        text_email_error.enabled = false;
    }

    public void OnLoginClicked()
    {
        string email = inputfield_email.text;
        string password = inputfield_password.text;

        Debug.Log($"Sending email of {email} and password of {password} to server");
        StartCoroutine(LoginAccount(email, password));
    }

    [System.Serializable]
    public class ResponseData
    {
        public string jwt;
    }

    [System.Serializable]
    public class ResponseDataError
    {
        public string error;
    }

    IEnumerator LoginAccount(string email, string password)
    {

        // Create a form and add the JSON data
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);

        // Create a UnityWebRequest object
        UnityWebRequest request = UnityWebRequest.Post("http://localhost:5000/api/user/login", form);

        // Set the content type header
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        // Send the request and wait for a response
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending JSON request: " + request.error);

            string response = request.downloadHandler.text;
            // Deserialize the JSON response
            ResponseDataError responseData = JsonUtility.FromJson<ResponseDataError>(response);

            if (responseData.error != null)
            {
                Debug.LogError("Server error: " + responseData.error);
                text_email_error.enabled = true;
                text_email_error.text = responseData.error;
            }
        }
        else
        {
            Debug.Log("JSON request sent successfully");

            string response = request.downloadHandler.text;
            // Deserialize the JSON response
            ResponseData responseData = JsonUtility.FromJson<ResponseData>(response);

            // Retrieve the JWT from the deserialized response
            string jwt = responseData.jwt;

            // Use the JWT as needed
            Debug.Log("JWT: " + jwt);


            PlayerPrefs.SetString("JWT", jwt);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Main");


        }

        // Clean up the request object
        request.Dispose();
    }
}
