using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameList : MonoBehaviour
{
    public Image smallBanner;
    public TMP_Text title;
    public Button play;
    public Transform developmentSkillsParent;

    public Game2 game2Screen;

    Game game;

    public void SetGameList(int index, Game game)
    {
        this.game = game;
        smallBanner.sprite = game.smallBanner;
        title.text = "GAME " + (index + 1).ToString() + ": " + game.title;

        play.onClick.AddListener(() =>
        {
            game2Screen.SetGame2Screen(game);
        });

        PopulateDevelopmentSkills();
        StartCoroutine(RefreshLayout());
    }

    public void PopulateDevelopmentSkills()
    {
        foreach (Transform child in developmentSkillsParent)
            Destroy(child.gameObject);

        foreach (string devSkill in GetSelectedSkills(game.devSkills))
        {
            DevelopmentSkillObject devObject = DevelopmentSkillsCatalog.instance.GetDevelopmentSkill(devSkill);
            if (devObject != null)
            {
                GameObject newDevSkill = new GameObject();
                newDevSkill.transform.SetParent(developmentSkillsParent);
                newDevSkill.transform.localScale = new Vector3(.7f, .7f, .7f);
                Image image = newDevSkill.AddComponent<Image>();
                image.preserveAspect = true;
                image.sprite = devObject.image;
            }
        }
    }

    public List<string> GetSelectedSkills(DevSkillsEnum devSkills)
    {

        List<string> selectedElements = new List<string>();

        for (int i = 0; i < System.Enum.GetValues(typeof(DevSkillsEnum)).Length; i++)
        {
            int layer = 1 << i;
            if (((int)devSkills & layer) != 0)
            {
                selectedElements.Add(System.Enum.GetValues(typeof(DevSkillsEnum)).GetValue(i).ToString());
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
