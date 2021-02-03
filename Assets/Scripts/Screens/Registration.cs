using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.Events;

public class Registration : MonoBehaviour
{
    public FormInput firstName;
    public FormInput lastName;
    public DatePicker dateOfBirth;
    public FormInput contactNumber;
    public FormInput emailAddress;
    public FormInput parentsEmail;
    public FormInput username;
    public FormInput password;
    public FormInput confirmPassword;

    public UnityEvent onRegister;

    public void ValidateInput()
    {
        bool validationFailed = false;
        validationFailed = ValidateDetails();
        validationFailed = ValidateUsername();
        validationFailed = ValidateEmails();
        validationFailed = ValidatePassword();

        if (validationFailed)
            Debug.Log("Validation failed");
        else
            UploadData();
    }

    bool ValidateDetails()
    {
        bool validationFailed = false;
        if (firstName.GetText().Equals(""))
        {
            firstName.ValidationFailed("* Field is required");
            validationFailed = true;
        }
        else
        {
            firstName.HideValidation();
        }

        if (lastName.GetText().Equals(""))
        {
            lastName.ValidationFailed("* Field is required");
            validationFailed = true;
        }
        else
        {
            lastName.HideValidation();
        }

        //Might need a separate more complex validation
        if (contactNumber.GetText().Equals(""))
        {
            contactNumber.ValidationFailed("* Field is required");
            validationFailed = true;
        }
        else
        {
            contactNumber.HideValidation();
        }

        return validationFailed;
    }

    bool ValidateUsername()
    {
        bool validationFailed = false;
        string uname = username.GetText();
        if (uname.Equals(""))
        {
            username.ValidationFailed("* Field is required");
            return true;
        }

        UserData userData = new UserData();
        userData.username = uname;
        StartCoroutine(userData.DownloadUserData(result =>
        {
            if (result != null)
            {
                username.ValidationFailed("* Username not available");
                username.ClearText();
                uname = "";
                validationFailed = true;
            }
        }));

        if (!validationFailed)
            username.HideValidation();

        userData = null;
        return validationFailed;
    }

    bool ValidateEmails()
    {
        bool validationFailed = false;

        //emailAddress
        if (emailAddress.GetText().Equals(""))
        {
            emailAddress.ValidationFailed("* Field is required");
            validationFailed = true;
        }
        else
        {
            try
            {
                MailAddress addr = new MailAddress(emailAddress.GetText());
                validationFailed = !(addr.Address == emailAddress.GetText());

                if (validationFailed)
                    emailAddress.ValidationFailed("* Invalid E-mail Address");
            }
            catch
            {
                validationFailed = true;
                emailAddress.ValidationFailed("* Invalid E-mail Address");
            }
        }

        //parentsEmail
        if (parentsEmail.GetText().Equals(""))
        {
            parentsEmail.ValidationFailed("* Field is required");
            validationFailed = true;
        }
        else
        {
            try
            {
                MailAddress addr = new MailAddress(parentsEmail.GetText());
                validationFailed = !(addr.Address == parentsEmail.GetText());

                if (validationFailed)
                    parentsEmail.ValidationFailed("* Invalid E-mail Address");
            }
            catch
            {
                validationFailed = true;
                parentsEmail.ValidationFailed("* Invalid E-mail Address");
            }
        }


        if (!validationFailed)
        {
            emailAddress.HideValidation();
            parentsEmail.HideValidation();
        }

        return validationFailed;
    }

    bool ValidatePassword()
    {
        bool validationFailed = false;
        if (password.GetText().Equals(""))
        {
            password.ValidationFailed("* Field is required");
            validationFailed = true;
            return validationFailed;
        }

        if (!password.GetText().Equals(confirmPassword.GetText()))
        {
            confirmPassword.ValidationFailed("* Passwords do not match");
            validationFailed = true;
            return validationFailed;
        }

        if (!validationFailed)
        {
            password.HideValidation();
            confirmPassword.HideValidation();
        }

        return false;
    }

    void UploadData()
    {
        UserData userData = new UserData();
        userData.firstName = firstName.GetText();
        userData.lastName = lastName.GetText();
        userData.dateOfBirth = dateOfBirth.Stringify();
        userData.contactnumber = contactNumber.GetText();
        userData.emailAddress = emailAddress.GetText();
        userData.parentsEmail = parentsEmail.GetText();
        userData.username = username.GetText();
        userData.password = password.GetText();
        userData.language = "English";

        StartCoroutine(userData.UploadData(false, false, result =>
        {
            if (result)
                if (onRegister != null)
                    onRegister.Invoke();
        }));
    }
}
