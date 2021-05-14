using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    UserData _user;

    public Toggle stayLoggedin;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text validation;
    public UnityEvent onLogin;

    public bool testBuild = false;
    public GameObject testBuildBanner;

    void Start()
    {
        _user = new UserData();

        if (testBuild)
            testBuildBanner.SetActive(true);
    }

    public void LogIn()
    {
        _user.username = usernameInput.text;
        _user.password = passwordInput.text;
        bool stayLoggedInValue = stayLoggedin.isOn;

        ProcessLogIn(stayLoggedInValue);
    }

    UserData ProcessLogIn(bool stayLoggedInValue)
    {
        StartCoroutine(_user.UploadData(true, stayLoggedInValue, result =>
        {
            Debug.Log(result ? "Login successful" : "Login failed");
            if (result || testBuild)
            {
                if (!testBuild && User.instance != null)
                {
                    User.instance.DownloadUserData(_user.username, true);
                }
                else
                {
                    User.instance._userData = new UserData();
                    UserData _userData = User.instance._userData;
                    _userData.firstName = "Test";
                    _userData.lastName = "User";
                    _userData.emailAddress = "testUser@email.com";
                    _userData.parentsEmail = "testUserParent@email.com";
                    _userData.username = "testUser";
                    _userData.password = "password";
                    _userData.tokens = 101;
                    _userData.tokensEarned = 202;
                    _userData.skillsPlayed = new List<KeyCount>();
                    _userData.gamesPlayed = new List<KeyCount>();
                }

                StartCoroutine(CheckFirstLogin());
            }
            else
            {
                validation.gameObject.SetActive(true);
            }

            User.instance.UpdateStayLogin(stayLoggedInValue);
        }));

        return User.instance._userData;
    }

    IEnumerator CheckFirstLogin()
    {
        yield return new WaitForSeconds(0.5f);

        if (User.instance.CheckFirstLogin())
            GetComponent<SceneController>().LoadScene(2);
        else
        {
            validation.gameObject.SetActive(false);

            if (onLogin != null)
                onLogin.Invoke();
        }
    }

    void OnDestroy()
    {
        _user = null;
    }
}