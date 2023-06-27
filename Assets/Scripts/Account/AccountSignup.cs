using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class Account : MonoBehaviour
{
    public TMP_InputField inputfield_email;
    public TMP_InputField inputfield_password;
    public TMP_InputField inputfield_confirm_password;

    public TMP_Text text_email_error;
    public TMP_Text text_password_error;

    public void Start()
    {
        text_email_error.enabled = false;
        text_password_error.enabled = false;
    }

    public void OnSignupClicked()
    {
        string email = inputfield_email.text;
        string password = inputfield_password.text;
        string passwordConfirm = inputfield_confirm_password.text;

        IsEmailEmpty(email);
        IsPasswordEmpty(password);
        IsPasswordConfirmSame(password, passwordConfirm);
        IsPasswordLength(password);

        if (text_email_error.enabled == false && text_password_error.enabled == false)
        {
            // Send email and password to server to create an account
            Debug.Log($"Sending Email value: {email} and password value: {password} to server.");
            StartCoroutine(CreateAccount(email, password));
        }
    }

    public void IsEmailEmpty(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            text_email_error.enabled = true;
            text_email_error.text = "Email cannot be empty";
        }
        else
        {
            text_email_error.enabled = false;
        }
    }

    public void IsPasswordEmpty(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            text_password_error.enabled = true;
            text_password_error.text = "Password cannot be empty";
        }
        else
        {
            text_password_error.enabled = false;
        }
    }

    public void IsPasswordConfirmSame(string password, string passwordConfirm)
    {
        if (password != passwordConfirm)
        {
            text_password_error.enabled = true;
            text_password_error.text = "Passwords do not match";
        }
        else
        {
            text_password_error.enabled = false;
        }
    }

    public void IsPasswordLength(string password)
    {
        if (password.Length < 6)
        {
            text_password_error.enabled = true;
            text_password_error.text = "Password must be longer than 6 characters";
        }
        else
        {
            text_password_error.enabled = false;
        }
    }

    [System.Serializable]
    public class ResponseDataError
    {
        public string error;
    }

    [System.Serializable]
    public class ResponseData
    {
        public string jwt;
    }

    IEnumerator CreateAccount(string email, string password)
    {
 
        // Create a form and add the JSON data
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);

        // Create a UnityWebRequest object
        UnityWebRequest request = UnityWebRequest.Post("http://localhost:5000/api/user/create", form);

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
            Debug.Log("JSON request sent successfully with successful response");

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
