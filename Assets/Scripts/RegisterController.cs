using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    private string RegisterUserURL = "http://localhost:55275/unity/register";
    private bool isRunning = false;

    GUIStyle style = new GUIStyle();
    GUIStyle buttonStyle = new GUIStyle();
    GUIStyle placeholderStyle = new GUIStyle();
    GUIStyle labelStyle = new GUIStyle();
    GUIStyle errorStyle = new GUIStyle();
    public Font myFont;
    public Texture2D btnBG;
    public Texture2D hoverBtnBG;
    public Texture2D pressedBtnBG;
    public string emailToEdit = " ";
    public string passwordToEdit = " ";
    public string confirmPasswordToEdit = " ";
    public string emailError = " ";
    public string passwordError = " ";
    public string confirmPasswordError = " ";

    [System.Serializable]
    public class ResponseData
    {
        public string Email;
        public string Password;
        public string ConfirmPassword;
    }

    public void Start()
    {

        style.font = myFont;
        style.fontSize = 40;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = new Color32(255, 156, 49, 255);

        buttonStyle.font = myFont;
        buttonStyle.fontSize = 40;
        buttonStyle.fontStyle = FontStyle.Bold;
        buttonStyle.normal.textColor = new Color32(255, 156, 49, 255);
        buttonStyle.hover.textColor = new Color32(255, 156, 49, 255);
        buttonStyle.active.textColor = new Color32(255, 156, 49, 255);
        buttonStyle.normal.background = btnBG;
        buttonStyle.hover.background = hoverBtnBG;
        buttonStyle.active.background = pressedBtnBG;
        buttonStyle.alignment = TextAnchor.MiddleCenter;

        placeholderStyle.fontSize = 25;
        placeholderStyle.fontStyle = FontStyle.Italic;
        placeholderStyle.normal.textColor = Color.grey;

        labelStyle.fontSize = 25;
        labelStyle.normal.textColor = Color.white;

        errorStyle.fontSize = 20;
        errorStyle.fontStyle = FontStyle.Bold;
        errorStyle.normal.textColor = new Color32(197, 3, 3, 255);
    }

    public IEnumerator Register()
    {
        isRunning = true;

        WWWForm form = new WWWForm();
        Debug.Log(emailToEdit);
        Debug.Log(passwordToEdit);
        Debug.Log(confirmPasswordToEdit);
        form.AddField("Email", emailToEdit);

        //Passwords must be AT LEAST 6 CHARACTERS LONG
        //Passwords must have at least one non letter or digit character. 
        //Passwords must have at least one digit ('0'-'9').
        //Passwords must have at least one lowercase ('a'-'z').
        form.AddField("Password", passwordToEdit);
        form.AddField("ConfirmPassword", confirmPasswordToEdit);
        Debug.Log("SEND2");

        using (WWW www = new WWW(RegisterUserURL, form))
        {

            //UnityWebRequest www = UnityWebRequest.Post("http://localhost:1107/api/Hello/Ian", form);
            yield return www;

            int responseCode = Response.GetResponseCode(www);
            print(responseCode);

            if (responseCode == Response.SUCCESS_CODE)
            {
                string username = Cryptography.Encrypt(emailToEdit);
                string password = Cryptography.Encrypt(passwordToEdit);
                string login = Cryptography.Encrypt("true");
                print(login);

                PlayerPrefs.SetString("Username", username);
                PlayerPrefs.SetString("Password", password);
                PlayerPrefs.SetString("Login", login);

                isRunning = false;
                SceneManager.LoadScene(0);
            }

            string response = www.text;

            print(www.text);

            ResponseData info = JsonUtility.FromJson<ResponseData>(www.text);

            Debug.Log("************* MESSAGE ************");
            Debug.Log(info.Password);

            Debug.Log("************* Email ************");
            Debug.Log(info.Email);

            Debug.Log("************* ConfirmPassword ************");
            Debug.Log(info.ConfirmPassword);
            //SUCCESS CASE
            //EMAIL ALREADY EXISTS CASE

            if (info.Email != null)
            {
                emailError = info.Email;
            }
            else
            {
                emailError = " ";
            }

            if (info.Password != null)
            {
                passwordError = info.Password;
            }
            else
            {
                passwordError = " ";
            }

            if (info.ConfirmPassword != null)
            {
                confirmPasswordError = info.ConfirmPassword;
            }
            else
            {
                confirmPasswordError = " ";
            }

            isRunning = false;
            
        }
    }

    private void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));
        GUI.Box(new Rect(0, 0, 800, 600), "");
        GUI.Label(new Rect(300, 0, 255, 52), "REGISTER", style);

        GUI.skin.textField.fontSize = 25; 
        GUI.skin.textField.alignment = TextAnchor.MiddleLeft;

        GUI.Label(new Rect(250, 120, 255, 52), "Email:", labelStyle);
        emailToEdit = GUI.TextField(new Rect(250, 150, 300, 30), emailToEdit, 25);
        if (emailToEdit.Length == 0 || emailToEdit.Equals(null))
        {
            GUI.Label(new Rect(254, 150, 255, 52), "Enter email address...", placeholderStyle);
        } 
        GUI.Label(new Rect(250, 180, 300, 100), emailError, errorStyle);
        
        GUI.Label(new Rect(250, 220, 255, 52), "Password:", labelStyle);
        passwordToEdit = GUI.PasswordField(new Rect(250, 250, 300, 30), passwordToEdit, '*', 25);
        if (passwordToEdit.Length == 0 || passwordToEdit.Equals(null))
        {
            GUI.Label(new Rect(254, 250, 255, 52), "Enter password...", placeholderStyle);
        }
        GUI.Label(new Rect(250, 280, 300, 100), passwordError, errorStyle);

        GUI.Label(new Rect(250, 320, 255, 52), "Confirm Password:", labelStyle);
        confirmPasswordToEdit = GUI.PasswordField(new Rect(250, 350, 300, 30), confirmPasswordToEdit, '*',25);
        if (confirmPasswordToEdit.Length == 0 || confirmPasswordToEdit.Equals(null))
        {
            GUI.Label(new Rect(254, 350, 255, 52), "Re-enter password...", placeholderStyle);
        }
        GUI.Label(new Rect(250, 380, 300, 100), confirmPasswordError, errorStyle);


        if (GUI.Button(new Rect(100, 460, 220, 70), "Back", buttonStyle))
        {
            SceneManager.LoadScene(0);
        }
         
        if (GUI.Button(new Rect(450, 460, 220, 70), "Register", buttonStyle))
        {
            if (isRunning == false)
            {
                StartCoroutine(Register());
            }
        }

        GUI.EndGroup();
    }

}