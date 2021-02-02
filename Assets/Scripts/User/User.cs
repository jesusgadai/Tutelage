using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public UserData _userData;

    LocalUserData _localData;

    #region Singleton
    public static User instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("[User.cs] - Multiple User(s) found!");
            Destroy(gameObject);
        }
    }
    #endregion 

    // Start is called before the first frame update
    void Start()
    {
        _userData = new UserData();
        _localData = new LocalUserData();
    }

    public void DownloadUserData(string username)
    {
        _userData.username = username;

        StartCoroutine(_userData.DownloadUserData(result =>
        {
            _userData = result;
            Debug.Log("download complete");
            Debug.Log(result.firstName);
            Debug.Log(_userData.firstName);
        }));
    }

    public void UpdateUserData()
    {
        StartCoroutine(_userData.Update(result =>
        {
            Debug.Log("[User.cs] - Update result: " + result);
        }));
    }

    public void LogOut()
    {
        _userData = null;
        _localData = null;
    }

    public void UpdateUserImage()
    {
        _localData.ReadUserImage();
    }

    public bool CheckFirstLogin()
    {
        bool oldUser = _localData.oldUser;

        if (!oldUser)
            _localData.oldUser = true;

        return oldUser;
    }

    #region _userData getters
    public List<KeyCount> GetSkills()
    {
        return _userData.skillsPlayed;
    }

    public List<KeyCount> GetGamesPlayed()
    {
        return _userData.gamesPlayed;
    }

    public string GetFirstName()
    {
        return _userData.firstName;
    }

    public string GetFullName()
    {
        return _userData.firstName + " " + _userData.lastName;
    }

    public Sprite GetUserImage()
    {
        return _localData.playerImage;
    }

    public int GetTokens()
    {
        return _userData.tokens;
    }

    public int GetTokensEarned()
    {
        return _userData.tokensEarned;
    }
    #endregion
    #region _userData updated setters
    public void AddTokens(int tokens)
    {
        _userData.tokens += tokens;
        _userData.tokensEarned += tokens;
        UpdateUserData();
    }

    public void UpdateKeyCountList(List<KeyCount> keyCountList, string name, int count)
    {
        KeyCount keyCount = keyCountList.Find(s => s.name.Equals(name));

        if (keyCount != null)
            keyCount.count += count;
        else
            keyCountList.Add(new KeyCount(name, count));

        UpdateUserData();
    }
    #endregion
}
