using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Login : MonoBehaviour
{
    UserData _user;

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

        StartCoroutine(_user.UploadData(true, result =>
        {
            Debug.Log(result);
            if (result || testBuild)
            {
                if (!testBuild && User.instance != null)
                {
                    User.instance.DownloadUserData(_user.username);
                }

                StartCoroutine(CheckFirstLogin());
            }
            else
            {
                validation.gameObject.SetActive(true);
            }
        }));
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