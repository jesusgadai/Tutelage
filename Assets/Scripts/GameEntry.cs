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

    public GameCategoryScreen categoryScreen;

    MiniGame miniGame;
    public void Set(MiniGame miniGame)
    {
        this.miniGame = miniGame;
        this.title.text = miniGame.title;
        this.price.text = "<color=#6A6A6A>TOKEN</color> <color=#FFD426>" + miniGame.price.ToString() + "</color>";
        image.sprite = miniGame.image;

        Lock(miniGame.status == Status.LOCKED);

        playButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            categoryScreen.SetGameCategoryScreen(miniGame.gameCategory);
        });
    }

    void Lock(bool isLocked)
    {
        lockOverlay.SetActive(isLocked);
        unlockButton.SetActive(isLocked);
        playButton.SetActive(!isLocked);
    }
}
