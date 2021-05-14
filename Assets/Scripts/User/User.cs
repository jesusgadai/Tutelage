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

        _localData = new LocalUserData();
    }
    #endregion 

    public void UpdateStayLogin(bool value)
    {
        _localData.UpdateStayLogin(value);
    }

    public void AddDevelopmentSkill(List<DevSkillsEnum> devtSkillsEnums)
    {
        foreach (DevSkillsEnum devSkillEnum in devtSkillsEnums)
        {
            string developmentSkill = DevSkills.EnumToString(devSkillEnum);
            KeyCount skillPlayed = _userData.skillsPlayed.Find(r => r.name.Equals(developmentSkill));

            if (skillPlayed != null)
                skillPlayed.count++;
            else
                _userData.skillsPlayed.Add(new KeyCount(developmentSkill, 1));
        }

        UpdateUserData();
    }

    public UserData ReadLocalJSON()
    {
        return _userData.ReadLocalJSON();
    }

    public void StayLoggedIn(bool value)
    {
        _localData.stayLoggedIn = value;
    }

    public void AddPlayedGame(string gameTitle)
    {
        KeyCount gamePlayed = _userData.gamesPlayed.Find(r => r.name.Equals(gameTitle));

        if (gamePlayed != null)
            gamePlayed.count++;
        else
            _userData.gamesPlayed.Add(new KeyCount(gameTitle, 1));

        UpdateUserData();
    }

    public void DownloadUserData(string username, bool updateLocalJSON)
    {
        _userData.username = username;

        StartCoroutine(_userData.DownloadUserData(result =>
        {
            if (result != null)
            {
                _userData = result;
                Debug.Log("[User.cs] - Downloaded User: " + result.username);
            }

            if (updateLocalJSON)
                _userData.SaveLocalJSON(_localData.stayLoggedIn);
        }));
    }

    public void UpdateUserData()
    {
        StartCoroutine(_userData.Update(result =>
        {
            Debug.Log("[User.cs] - Update result: " + result);
        }));

        _userData.SaveLocalJSON(_localData.stayLoggedIn);
    }

    public void DeleteAllLocalData()
    {
        _localData.DeleteAllLocalData();
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

    public bool CheckStayLoggedIn()
    {
        bool stayLoggedIn = _localData.stayLoggedIn;
        bool JSONexists = _userData.JSONexists();

        if (stayLoggedIn && JSONexists)
            return true;

        return false;
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

    public string GetUserName()
    {
        return _userData.username;
    }

    public int GetAge()
    {
        System.DateTime now = System.DateTime.Now;
        System.DateTime dateOfBirth = System.DateTime.Parse(_userData.dateOfBirth);

        int a = (now.Year * 100 + now.Month) * 100 + now.Day;
        int b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

        return (a - b) / 10000;
    }

    public string GetDateOfBirth()
    {
        System.DateTime dateOfBirth = System.DateTime.Parse(_userData.dateOfBirth);

        return dateOfBirth.ToString("MMMM dd, yyyy");
    }

    public Sprite GetUserImage()
    {
        return _localData.playerImage;
    }

    public string GetContactNumber()
    {
        return _userData.contactnumber;
    }

    public string GetEmailAddress()
    {
        return _userData.emailAddress;
    }

    public string GetParentsEmail()
    {
        return _userData.parentsEmail;
    }

    public string GetLanguage()
    {
        return _userData.language;
    }

    public int GetTotalGamesPlayed()
    {
        int total = 0;
        foreach (KeyCount gamePlayed in _userData.gamesPlayed)
        {
            total += gamePlayed.count;
        }

        return total;
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

        if (tokens > 0)
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
