using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (User.instance != null)
        {
            bool stayLoggedIn = User.instance.CheckStayLoggedIn();
            if (stayLoggedIn)
            {
                StartCoroutine(CompareLogins());
            }
            else
            {
                StartCoroutine(DelaySplash());
            }
        }
    }

    IEnumerator CompareLogins()
    {
        UserData savedUserData = User.instance.ReadLocalJSON();
        User.instance.DownloadUserData(savedUserData.username, false);
        yield return new WaitForSeconds(1f);

        UserData downloadedUserData = User.instance._userData;

        User.instance._userData = UserData.Compare(savedUserData, downloadedUserData);
        Debug.Log(User.instance._userData.timeStamp);
        gameObject.GetComponent<SceneController>().LoadScene(2);
    }

    IEnumerator DelaySplash()
    {
        yield return new WaitForSeconds(1f);

        gameObject.GetComponent<SceneController>().LoadScene(1);
    }
}
