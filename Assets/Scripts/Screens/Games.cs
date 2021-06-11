using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Games : MonoBehaviour
{
    public Transform mGamesParent;
    public GameObject gameEntryPrefab;

    GameCatalog catalog;

    void Start()
    {
        catalog = GameCatalog.instance;

        PopulateGames();
    }

    public void PopulateGames()
    {
        // Do not destroy first child and use it as a reference to keep the button references
        DestroyAllChildren(mGamesParent, true);

        int index = 0;
        foreach (GameEntryData gameEntryData in catalog.GetMiniGames())
        {
            if (index == 0)
            {
                GameEntry firstGameEntry = mGamesParent.GetChild(0).GetComponent<GameEntry>();
                firstGameEntry.Set(gameEntryData);
                index++;
                continue;
            }

            GameEntry gameEntry = Instantiate(gameEntryPrefab, Vector3.zero, Quaternion.identity, mGamesParent).GetComponent<GameEntry>();
            gameEntry.Set(gameEntryData);
        }

        StartCoroutine(RefreshLayout());
    }

    void DestroyAllChildren(Transform transform, bool skipFirstChild)
    {
        int index = 0;
        foreach (Transform child in transform)
        {
            if (index == 0 && skipFirstChild)
            {
                index++;
                continue;
            }

            Destroy(child.gameObject);
        }
    }

    IEnumerator RefreshLayout()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        foreach (Transform child in transform.GetComponentInChildren<RectTransform>())
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)child);
    }

    void OnEnable()
    {
        StartCoroutine(RefreshLayout());
    }
}
