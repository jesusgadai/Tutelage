using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEntry : MonoBehaviour
{
    public Image image;
    public TMP_Text title;
    public TMP_Text price;
    public GameObject lockOverlay;
    public GameObject playButton;
    public GameObject unlockButton;
    public GameIntroResult gameIntroResult;

    GameEntryData gameEntryData;
    public void Set(GameEntryData gameEntryData)
    {
        this.gameEntryData = gameEntryData;
        this.title.text = gameEntryData.title;
        this.price.text = "<color=#6A6A6A>TOKEN</color> <color=#FFD426>" + gameEntryData.price.ToString() + "</color>";
        image.sprite = gameEntryData.image;

        Lock(gameEntryData.status == Status.LOCKED);

        playButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            gameIntroResult.SetGame2Screen(gameEntryData.game);
        });
    }

    void Lock(bool isLocked)
    {
        lockOverlay.SetActive(isLocked);
        unlockButton.SetActive(isLocked);
        playButton.SetActive(!isLocked);
    }
}
