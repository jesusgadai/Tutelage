using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameIntroResult : MonoBehaviour
{
    [Header("Pre-Game Elements")]
    [EnumFlagsAttribute]
    public DevSkillsEnum devSkills;
    public Image banner;
    public Transform developedSkillsParent;
    public GameObject mechanicsHeader;
    public TMP_Text mechanics;
    public Image userImage;
    public GameObject preDetails;
    public GameObject preNavigation;
    public GameObject preNavigationReading;

    [Header("Game Elements")]
    public GameObject game3;
    public GameObject footer;

    [Header("Post-Game Elements")]
    public TMP_Text congratulationsText;
    public TMP_Text tokensEarned;
    public Button nextGameButton;
    public GameObject postDetails;
    public GameObject postNavigation;

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

        GameObject[] preGame = { preDetails, preNavigation, preNavigationReading };
        foreach (GameObject gObject in preGame)
            gObject.SetActive(true);

        GameObject[] postGame = { postDetails, postNavigation };
        foreach (GameObject gObject in postGame)
            gObject.SetActive(false);
    }

    public void GameStart()
    {
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath(game.title);

        //scene > 0 means scene is not the login screen
        if (sceneIndex > 0)
        {
            StartCoroutine(LoadGameScene(sceneIndex));
        }
        else
        {
            GameDone(true, 0);
        }
    }

    IEnumerator LoadGameScene(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            yield return null;
        }

        GameController controller = GameObject.FindObjectOfType<GameController>();
        controller.isGameDone.AddListener(delegate { GameDone(false, sceneIndex); });

        game3.SetActive(true);
        footer.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void GameDone(bool isSceneEmpty, int sceneIndex)
    {
        this.gameObject.SetActive(true);

        GameObject[] preGame = { preDetails, preNavigation, preNavigationReading, game3 };
        foreach (GameObject gObject in preGame)
            gObject.SetActive(false);

        GameObject[] postGame = { postDetails, postNavigation, footer };
        foreach (GameObject gObject in postGame)
            gObject.SetActive(true);

        if (!isSceneEmpty)
            SceneManager.UnloadSceneAsync(sceneIndex);

        RefreshLayoutBtn();
    }

    public void SetGame2Screen(Game game)
    {
        this.game = game;

        banner.sprite = game.banner;
        mechanicsHeader.SetActive(game.mechanics.Equals("") ? false : true);
        mechanics.text = game.mechanics;
        devSkills = game.devSkills;
        congratulationsText.text = "CONGRATULATIONS " + userFirstName + " YOU WON " + game.title + "!";
        tokensEarned.text = "YOU JUST EARNED\n<color=#ff8426>" + "+" + game.tokensToEarn.ToString() + " TOKENS</color>";

        preNavigation.SetActive(!game.readingGame);
        preNavigationReading.SetActive(game.readingGame);

        PopulateDevelopmentSkills();
        StartCoroutine(RefreshLayout());
    }

    public void ResetGame2Screen()
    {
        preNavigation.SetActive(!game.readingGame);
        preNavigationReading.SetActive(game.readingGame);
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
