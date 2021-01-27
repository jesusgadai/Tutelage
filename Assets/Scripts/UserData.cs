using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class UserData
{
    public string firstName;
    public string lastName;
    public string dateOfBirth;
    public string contactnumber;
    public string emailAddress;
    public string parentsEmail;
    public string username;
    public string password;
    public string language;
    public List<KeyCount> skillsPlayed;
    public List<KeyCount> gamesPlayed;
    public int tokens;
    public int tokensEarned;
    public string timeStamp;

    public string Stringify()
    {
        return JsonUtility.ToJson(this);
    }

    public static UserData Parse(string json)
    {
        return JsonUtility.FromJson<UserData>(json);
    }

    public IEnumerator DownloadUserData(System.Action<UserData> callback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/users/" + this.username))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(null);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(UserData.Parse(request.downloadHandler.text));
                }
            }
        }
    }

    public IEnumerator UploadData(bool login, System.Action<bool> callback = null)
    {
        timeStamp = System.DateTime.Now.ToString();

        string webhook = login ? "http://localhost:3000/users/login" : "http://localhost:3000/users";
        using (UnityWebRequest request = new UnityWebRequest(webhook, "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(this.Stringify());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(false);
                }
            }
            else
            {
                // Debug.Log(request.downloadHandler.text);
                if (callback != null)
                {
                    callback.Invoke(request.downloadHandler.text != "{}");
                }
            }
        }
    }

    public IEnumerator Update(System.Action<bool> callback = null)
    {
        timeStamp = System.DateTime.Now.ToString();

        using (UnityWebRequest request = new UnityWebRequest("http://localhost:3000/users/" + this.username, "PUT"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(this.Stringify());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(false);
                }
            }
            else
            {
                // Debug.Log(request.downloadHandler.text);
                if (callback != null)
                {
                    callback.Invoke(request.downloadHandler.text != "{}");
                }
            }
        }
    }
}
