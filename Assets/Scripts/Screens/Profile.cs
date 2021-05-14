using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Profile : MonoBehaviour
{
    public TMP_Text firstLastName;
    public TMP_Text username;
    public TMP_Text age;
    public TMP_Text dateOfBirth;
    public TMP_Text contactNumber;
    public TMP_Text emailAddress;
    public TMP_Text parentsEmail;
    public TMP_Text language;
    public Image userImage;

    void OnEnable()
    {
        if (User.instance != null)
        {
            User user = User.instance;
            firstLastName.text = user.GetFullName().Equals("") ? "Blank User" : user.GetFullName();
            username.text = user.GetUserName();
            age.text = user.GetAge().ToString();
            dateOfBirth.text = user.GetDateOfBirth();
            contactNumber.text = user.GetContactNumber();
            emailAddress.text = user.GetEmailAddress();
            parentsEmail.text = user.GetParentsEmail();
            language.text = user.GetLanguage();

            userImage.sprite = user.GetUserImage();
            userImage.color = Color.white;
        }
    }
}
