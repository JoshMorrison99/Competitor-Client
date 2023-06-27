using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountUI : MonoBehaviour
{
    public Button button_login;
    public Button button_signup;

    public GameObject login;
    public GameObject signup;

    private void Start()
    {
        login.SetActive(true);
        signup.SetActive(false);
    }

    public void OnSignInUIClicked()
    {
        login.SetActive(true);
        signup.SetActive(false);
    }

    public void OnSignupUIClicked()
    {
        login.SetActive(false);
        signup.SetActive(true);
    }
}


