using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FormInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text validationText;

    public void ValidationFailed(string message)
    {
        validationText.text = message;
        validationText.gameObject.SetActive(true);
    }

    public string GetText()
    {
        return inputField.text;
    }

    public void ClearText()
    {
        inputField.text = "";
    }

    public void HideValidation()
    {
        validationText.gameObject.SetActive(false);
    }
}
