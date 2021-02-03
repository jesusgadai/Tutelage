using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalUserData
{
    public string playerImagePath;
    public Sprite playerImage;
    public bool stayLoggedIn;

    bool privateOldUser;
    public bool oldUser
    {
        get
        {
            int loginCount = PlayerPrefs.GetInt(Keys.oldUser);
            PlayerPrefs.SetInt(Keys.oldUser, loginCount + 1);
            return privateOldUser;
        }
        set
        {
            privateOldUser = value;
        }
    }

    public LocalUserData()
    {
        string path = "resources";
#if UNITY_ANDROID
        path = ".resources";
#endif
        path = Path.Combine(Application.persistentDataPath, path);
        playerImagePath = Path.Combine(path, "playerImage.png");

        ReadUserImage();

        oldUser = PlayerPrefs.GetInt(Keys.oldUser) > 0 ? true : false;
        stayLoggedIn = PlayerPrefs.GetInt(Keys.stayLoggedIn) > 0 ? true : false;
    }

    public Texture2D ReadUserImage()
    {
        // Debug.Log("Reading file: " + playerImagePath);
        if (File.Exists(playerImagePath))
        {
            byte[] textureBytes = File.ReadAllBytes(playerImagePath);
            Texture2D texture = new Texture2D(8, 8);
            texture.LoadImage(textureBytes);
            playerImage = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            return texture;
        }

        return null;
    }

    public void UpdateStayLogin(bool value)
    {
        stayLoggedIn = value;
        PlayerPrefs.SetInt(Keys.stayLoggedIn, value ? 1 : 0);
    }
}
