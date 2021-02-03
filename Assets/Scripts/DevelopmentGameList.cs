using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DevelopmentGameList : MonoBehaviour
{
    public Image icon;
    public TMP_Text title;
    public TMP_Text count;

    public void SetGameList(Sprite icon, string title, int count)
    {
        this.icon.sprite = icon;
        this.title.text = title;
        this.count.text = count.ToString();
    }
}
