using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour
{
    public GameObject onLabel;

    Toggle toggle;
    void Start()
    {
        toggle = GetComponent<Toggle>();
        onLabel.SetActive(toggle.isOn);
    }

    public void OnValueChanged()
    {
        onLabel.SetActive(toggle.isOn);
    }
}
