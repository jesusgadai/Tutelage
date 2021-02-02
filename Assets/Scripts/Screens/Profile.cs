using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Profile : MonoBehaviour
{
    public TMP_Text firstLastName;
    public TMP_Text tokens;
    public Image userImage;

    void OnEnable()
    {
        if (User.instance != null)
        {
            firstLastName.text = User.instance.GetFullName().Equals("") ? "Blank User" : User.instance.GetFullName();
            tokens.text = User.instance.GetTokens().ToString();
            userImage.sprite = User.instance.GetUserImage();
            userImage.color = Color.white;
        }
    }

    public void LogOut()
    {
        User.instance.LogOut();
        Destroy(User.instance.gameObject);
        GetComponent<SceneController>().LoadScene(1);
    }
}
