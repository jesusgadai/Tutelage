using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGames : MonoBehaviour
{
    public Transform mGamesParent;
    public GameObject gameEntryPrefab;

    MiniGameCatalog catalog;

    void Start()
    {
        catalog = MiniGameCatalog.instance;

        PopulateMiniGames();
    }

    public void PopulateMiniGames()
    {
        // Do not destroy first child and use it as a reference to keep the button references
        DestroyAllChildren(mGamesParent, true);

        int index = 0;
        foreach (MiniGame miniGame in catalog.GetMiniGames())
        {
            if (index == 0)
            {
                GameEntry firstGameEntry = mGamesParent.GetChild(0).GetComponent<GameEntry>();
                firstGameEntry.Set(miniGame);
                index++;
                continue;
            }

            GameEntry gameEntry = Instantiate(gameEntryPrefab, Vector3.zero, Quaternion.identity, mGamesParent).GetComponent<GameEntry>();
            gameEntry.Set(miniGame);
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
