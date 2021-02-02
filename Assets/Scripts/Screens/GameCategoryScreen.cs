using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameCategoryScreen : MonoBehaviour
{
    public Image banner;
    public TMP_Text title;
    public TMP_Text description;
    public Transform requirementsParent;
    public GameObject requirementPrefab;

    [EnumFlagsAttribute]
    RequirementsEnum requirements;

    public Transform gamesParent;
    public GameObject gamePrefab;
    List<Game> games = new List<Game>();

    public void SetGameCategoryScreen(GameCategory gameCategory)
    {
        banner.sprite = gameCategory.banner;
        title.text = gameCategory.title;
        description.text = gameCategory.description;
        requirements = gameCategory.requirements;
        games = gameCategory.games;

        PopulateRequirements();
        PopulateGames();

        StartCoroutine(RefreshLayout());
    }

    void PopulateRequirements()
    {
        DestroyAllChildren(requirementsParent, true);

        int index = 0;
        foreach (RequirementsEnum selectedEnum in GetSelectedRequirements())
        {
            if (index == 0)
            {
                TMP_Text firstRequirement = requirementsParent.GetChild(0).GetComponent<TMP_Text>();
                firstRequirement.text = Requirements.EnumToString(selectedEnum);
                index++;
                continue;
            }

            TMP_Text requirement = Instantiate(requirementPrefab, Vector3.zero, Quaternion.identity, requirementsParent).GetComponent<TMP_Text>();
            requirement.text = Requirements.EnumToString(selectedEnum);
        }
    }

    void PopulateGames()
    {
        DestroyAllChildren(gamesParent, true);

        int index = 0;
        foreach (Game game in games)
        {
            if (index == 0)
            {
                GameList firstGameList = gamesParent.GetChild(0).GetComponent<GameList>();
                firstGameList.SetGameList(index, game);
                index++;
                continue;
            }

            GameList gameList = Instantiate(gamePrefab, Vector3.zero, Quaternion.identity, gamesParent).GetComponent<GameList>();
            gameList.SetGameList(index, game);
            index++;
        }
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

    public List<RequirementsEnum> GetSelectedRequirements()
    {

        List<RequirementsEnum> selectedElements = new List<RequirementsEnum>();

        for (int i = 0; i < System.Enum.GetValues(typeof(RequirementsEnum)).Length; i++)
        {
            int layer = 1 << i;
            if (((int)requirements & layer) != 0)
            {
                selectedElements.Add((RequirementsEnum)System.Enum.GetValues(typeof(RequirementsEnum)).GetValue(i));
            }
        }

        return selectedElements;
    }

    IEnumerator RefreshLayout()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        foreach (Transform child in transform.GetComponentInChildren<RectTransform>())
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)child);
    }
}
