using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DevelopmentStats : MonoBehaviour
{
    public TMP_Text firstLastName;
    public TMP_Text tokensEarned;
    public Image userImage;

    // Development Skills
    public Transform developmentSkills;
    public GameObject skillsListPair;

    // Games Played
    public Transform gamesPlayed;
    public GameObject gamesListPair;

    private void OnEnable()
    {
        if (User.instance != null && User.instance.GetSkills() != null)
        {
            firstLastName.text = User.instance.GetFullName();
            tokensEarned.text = User.instance.GetTokensEarned().ToString();
            userImage.sprite = User.instance.GetUserImage();
            userImage.color = Color.white;

            PopulateList(developmentSkills, skillsListPair, User.instance.GetSkills());
            PopulateList(gamesPlayed, gamesListPair, User.instance.GetGamesPlayed());
        }

        StartCoroutine(RefreshLayout());
    }

    void PopulateList(Transform parent, GameObject prefab, List<KeyCount> keyPairs)
    {
        DestroyAllChildren(parent);

        foreach (KeyCount keyCount in keyPairs)
        {
            GameObject newListPair = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            if (parent == developmentSkills)
            {
                newListPair.transform.Find("Game").GetComponent<TMP_Text>().text = keyCount.name;
                newListPair.transform.Find("Amount").GetComponent<TMP_Text>().text = keyCount.count.ToString();
            }
            else
            {
                DevelopmentGameList devGameList = newListPair.GetComponent<DevelopmentGameList>();
                Sprite icon = MiniGameCatalog.instance.FindIcon(keyCount.name);
                devGameList.SetGameList(icon, keyCount.name, keyCount.count);
            }
        }
    }

    void DestroyAllChildren(Transform transform)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    IEnumerator RefreshLayout()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        foreach (Transform child in transform.GetComponentInChildren<RectTransform>())
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)child);
    }
}
