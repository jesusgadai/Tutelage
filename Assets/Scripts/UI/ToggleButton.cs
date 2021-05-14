using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Image checkValue;
    public Sprite check;
    public Sprite uncheck;
    public bool value { get; private set; } = true;

    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Toggle);
        UpdateCheck();
    }

    public void Toggle()
    {
        value = !value;
        UpdateCheck();
    }

    void UpdateCheck()
    {
        checkValue.sprite = value ? check : uncheck;
    }
}
