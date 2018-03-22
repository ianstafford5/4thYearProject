using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{


    private string RegisterUserURL = "http://localhost:55275/unity/register";
    private string ScoreURL = "http://localhost:55275/unity/scores";
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
        //StartCoroutine(Score());
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

    [System.Serializable]
    public class Scores
    {
        public int ID;
        public string Name;
        public int Score;
    }

    [System.Serializable]
    public class ScoreData
    {
        public Scores Score1;
        public Scores Score2;
        public Scores Score3;
        public Scores Score4;
        public Scores Score5;
        public Scores Score6;
        public Scores Score7;
        public Scores Score8;
        public Scores Score9;
        public Scores Score10;
    }

    public IEnumerator Score()
    {
        Debug.Log("********SEND1******");
        WWW www = new WWW(ScoreURL);

        yield return www;

        string response = www.text;

        print(www.text);

        ScoreData info = JsonUtility.FromJson<ScoreData>(www.text);

        print(info.Score1.Name);
        print(info.Score1.Score);
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
        Debug.Log(emailToEdit);
        Debug.Log(passwordToEdit);
        Debug.Log(confirmPasswordToEdit);
        form.AddField("Email", emailToEdit);
        form.AddField("Password", passwordToEdit);
        form.AddField("ConfirmPassword", confirmPasswordToEdit);
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

        if (info.Email != null)
        {
            confirmPasswordError = info.ConfirmPassword;
        }
        else
        {
            confirmPasswordError = " ";
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

    private void OnGUI()
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


        if (GUI.Button(new Rect(100, 460, 220, 70), "Exit", buttonStyle))
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