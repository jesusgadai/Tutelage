using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game2 : MonoBehaviour
{

    [EnumFlagsAttribute]
    public DevSkillsEnum devSkills;
    public Image banner;
    public Transform developedSkillsParent;
    public TMP_Text mechanics;
    public Image userImage;
    public TMP_Text congratulationsText;
    public TMP_Text tokensEarned;
    public Button nextGameButton;
    Game game;

    string userFirstName;

    private void OnEnable()
    {
        if (User.instance != null)
        {
            userFirstName = User.instance.GetFirstName();
            userImage.sprite = User.instance.GetUserImage();
            userImage.color = Color.white;
        }
    }

    public void SetGame2Screen(Game game)
    {
        this.game = game;
        if (game.nextGame == null)
        {
            nextGameButton.gameObject.SetActive(false);
        }
        else
        {
            nextGameButton.gameObject.SetActive(true);
            nextGameButton.onClick.AddListener(() =>
            {
                SetGame2Screen(game.nextGame);
            });
        }
        banner.sprite = game.banner;
        mechanics.text = game.mechanics;
        devSkills = game.devSkills;
        congratulationsText.text = "CONGRATULATIONS " + userFirstName + " YOU WON " + game.title + "!";
        tokensEarned.text = "YOU JUST EARNED\n<color=#ff8426>" + "+" + game.tokensToEarn.ToString() + " TOKENS</color>";

        PopulateDevelopmentSkills();
        StartCoroutine(RefreshLayout());
    }

    public void RefreshLayoutBtn()
    {
        StartCoroutine(RefreshLayout());
    }

    public void TestWin()
    {
        if (User.instance != null)
        {
            User user = User.instance;
            user.AddDevelopmentSkill(GetSelectedSkillsEnum());

            user.AddPlayedGame(game.title);

            user.AddTokens(game.tokensToEarn);
        }
    }

    IEnumerator RefreshLayout()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        foreach (Transform child in transform.GetComponentInChildren<RectTransform>())
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)child);
    }

    public void PopulateDevelopmentSkills()
    {
        GameObject developmentSkillPrefab = DevelopmentSkillsCatalog.instance.developmentSkillPrefab;
        foreach (Transform child in developedSkillsParent)
            Destroy(child.gameObject);

        foreach (string devSkill in GetSelectedSkills())
        {
            DevelopmentSkillObject devObject = DevelopmentSkillsCatalog.instance.GetDevelopmentSkill(devSkill);
            if (devObject != null)
            {
                GameObject newGameObj = Instantiate(developmentSkillPrefab, Vector3.zero, Quaternion.identity, developedSkillsParent);
                DevelopmentSkills devSk = newGameObj.GetComponentInChildren<DevelopmentSkills>();
                // newGameObj.GetComponent<Image>().SetNativeSize();
                devSk.SetSkill(devObject);
            }
            else
            {
                Debug.Log(devSkill + ": Development Skills Object NULL");
            }
        }
    }

    public List<string> GetSelectedSkills()
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

    public List<DevSkillsEnum> GetSelectedSkillsEnum()
    {

        List<DevSkillsEnum> selectedElements = new List<DevSkillsEnum>();

        for (int i = 0; i < System.Enum.GetValues(typeof(DevSkillsEnum)).Length; i++)
        {
            int layer = 1 << i;
            if (((int)devSkills & layer) != 0)
            {
                selectedElements.Add((DevSkillsEnum)System.Enum.GetValues(typeof(DevSkillsEnum)).GetValue(i));
            }
        }

        return selectedElements;
    }
}
