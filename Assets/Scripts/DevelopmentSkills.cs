using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DevelopmentSkills : MonoBehaviour
{
    public GameObject listPair;

    void OnEnable()
    {
        if (User.instance != null)
        {
            if (User.instance.GetSkills().Count > 0)
                DestroyAllChildren();

            foreach (KeyCount keyCount in User.instance.GetSkills())
            {
                GameObject newListPair = Instantiate(listPair, Vector3.zero, Quaternion.identity, transform);
                newListPair.transform.Find("Game").GetComponent<TMP_Text>().text = keyCount.name;
                newListPair.transform.Find("Amount").GetComponent<TMP_Text>().text = keyCount.count.ToString();
            }
        }

        StartCoroutine(RefreshLayout());
    }

    void DestroyAllChildren()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    IEnumerator RefreshLayout()
    {
        yield return new WaitForFixedUpdate();

        yield return new WaitForFixedUpdate();
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.parent.parent);
    }
}
