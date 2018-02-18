using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour {


    private string RegisterUserURL = "http://localhost:55275/api/Hello";
    private bool isRunning = false;

    public InputField email;
    public InputField password;
    public InputField confirmPassword;
    public Text emailResponse;
    public Text passwordResponse;
    public Text confirmPasswordResponse;

    public void Start()
    {
        // Sets the MyValidate method to invoke after the input field's default input validation invoke (default validation happens every time a character is entered into the text field.)
        //mainInputField.onValidateInput += delegate (string input, int charIndex, char addedChar) { return MyValidate(addedChar); };
        //StartCoroutine(Register());
    }

    private char MyValidate(char charToValidate)
    {
        //Checks if a dollar sign is entered....
        if (charToValidate == '$')
        {
            // ... if it is change it to an empty character.
            //text.text = "Email";
        }
        return charToValidate;
    }

    [System.Serializable]
    public class ResponseData
    {
        public string Email;
        public string Password;
        public string ConfirmPassword;
    }

    public void RegButton()
    {
        if (isRunning == false)
        {
            StartCoroutine(Register());
        }
    }

    public IEnumerator Register()
    {
        isRunning = true;
        Debug.Log("********SEND1******");
        WWWForm form = new WWWForm();
        form.AddField("Email", email.text);
        form.AddField("Password", password.text);
        form.AddField("ConfirmPassword", confirmPassword.text);
        Debug.Log("SEND2");
        WWW www = new WWW(RegisterUserURL, form);
        //UnityWebRequest www = UnityWebRequest.Post("http://localhost:1107/api/Hello/Ian", form);
        yield return www;

        string response = www.text;

        print(www.text);

        ResponseData info = JsonUtility.FromJson<ResponseData>(www.text);

        Debug.Log("************* MESSAGE ************");
        Debug.Log(info.Password);

        Debug.Log("************* Email ************");
        Debug.Log(info.Email);

        Debug.Log("************* ConfirmPassword ************");
        Debug.Log(info.ConfirmPassword);

        if (info.Email != null)
        {
            emailResponse.text = info.Email;
        }
        else
        {
            emailResponse.text = " ";
        }

        if (info.Password != null)
        {
            passwordResponse.text = info.Password;
        }
        else
        {
            passwordResponse.text = " ";
        }

        if (info.Email != null)
        {
            confirmPasswordResponse.text = info.ConfirmPassword;
        }
        else
        {
            confirmPasswordResponse.text = " ";
        }

        isRunning = false;
        //yield return www.responseHeaders;
    }

    /*public IEnumerator GetHello()
    {
        Debug.Log("Message");
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:1107/api/Hello/Ian");
        yield return www.Send();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error");
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }*/

}
