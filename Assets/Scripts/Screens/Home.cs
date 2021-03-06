﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Home : MonoBehaviour
{
    public TMP_Text firstLastName;
    public TMP_Text gamesPlayed;
    public TMP_Text tokens;
    public Image userImage;

    void OnEnable()
    {
        if (User.instance != null)
        {
            User user = User.instance;
            firstLastName.text = user.GetFullName().Equals("") ? "Blank User" : user.GetFullName();
            gamesPlayed.text = user.GetTotalGamesPlayed().ToString();
            tokens.text = user.GetTokens().ToString();
            userImage.sprite = user.GetUserImage();
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
