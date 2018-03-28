using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using Assets.Scripts;

public class Login : MonoBehaviour
{

    private string LoginURL = "http://localhost:55275/unity/login";

    GUIStyle style = new GUIStyle();
    GUIStyle buttonStyle = new GUIStyle();
    GUIStyle placeholderStyle = new GUIStyle();
    GUIStyle labelStyle = new GUIStyle();
    GUIStyle errorStyle = new GUIStyle();
    public Font myFont;
    public Texture2D btnBG;
    public Texture2D hoverBtnBG;
    public Texture2D pressedBtnBG;
    public string emailToEdit = "";
    public string passwordToEdit = "";
    public string loginError;
    public float hSliderValue = 0.0F;

    public bool loading = true;

    // Use this for initialization
    void Start()
    {
        //print("***************************************************************");
        //string str = "200";
        //print(str);
        //string encrypt = Encrypt(str);
        //print(encrypt);
        //print("***************************************************************");

        //PlayerPrefs.SetString("Score", encrypt);

        //string decrypt = PlayerPrefs.GetString("Score");
        //print(decrypt);
        //decrypt = Decrypt(decrypt);
        //print(decrypt);

        //print("***************************************************************");

        //string i = "23242";

        //int it = 0;

        //Int32.TryParse(i, out it);

        //print(it);

        //string s = i.ToString();

        //print(s.GetType());
        //  GUI.skin.horizontalSlider.;

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
        errorStyle.wordWrap = true;
        errorStyle.normal.textColor = new Color32(197, 3, 3, 255);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator LoginUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("Email", emailToEdit);
        form.AddField("Password", passwordToEdit);
        form.AddField("RemeberMe", 0);
        Debug.Log("SEND2");

        using (WWW www = new WWW(LoginURL, form))
        {
            //UnityWebRequest www = UnityWebRequest.Post("http://localhost:1107/api/Hello/Ian", form);
            //yield return www;

            while (!www.isDone)
            {
                loading = true;
                Debug.Log("Progress");
                float progress = Mathf.Clamp01(www.progress / .9f);
                Debug.Log(progress * 100);


                yield return null;
            }

            loading = false;
            int responseCode = Response.GetResponseCode(www);
            print(responseCode);

            if (responseCode != Response.SUCCESS_CODE)
            {
                loginError = "The email or password you provided is incorrect";
            }
            else
            {
                string username = Cryptography.Encrypt(emailToEdit);
                string password = Cryptography.Encrypt(passwordToEdit);
                string login = Cryptography.Encrypt("true");
                print(login);

                PlayerPrefs.SetString("Username", username);
                PlayerPrefs.SetString("Password", password);
                PlayerPrefs.SetString("Login", login);
                SceneManager.LoadScene(0);
            }
        }
    }

    public GUISkin standartSkin;
    void Loader(int windowID)
    {
        GUI.skin = standartSkin;
        hSliderValue = GUI.HorizontalSlider(new Rect(25, 25, 300, 70), 0, 0.1f, 6f);
    }

    private void OnGUI()
    {

        if (loading)
        {
            GUI.ModalWindow(1, new Rect(Screen.width / 2 - 260, Screen.height / 2 - 230, 500, 250), Loader, "");
        }

        GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));
        GUI.Box(new Rect(0, 0, 800, 600), "");
        GUI.Label(new Rect(340, 0, 255, 52), "LOGIN", style);

        GUI.skin.textField.fontSize = 25;
        GUI.skin.textField.alignment = TextAnchor.MiddleLeft;

        GUI.Label(new Rect(250, 60, 300, 100), loginError, errorStyle);

        GUI.Label(new Rect(250, 120, 255, 52), "Email:", labelStyle);
        emailToEdit = GUI.TextField(new Rect(250, 150, 300, 32), emailToEdit, 25);
        if (emailToEdit.Length == 0 || emailToEdit.Equals(null))
        {
            GUI.Label(new Rect(256, 150, 255, 52), "Enter email address...", placeholderStyle);
        }

        GUI.Label(new Rect(250, 220, 255, 52), "Password:", labelStyle);
        passwordToEdit = GUI.PasswordField(new Rect(250, 250, 300, 32), passwordToEdit, '*', 25);
        if (passwordToEdit.Length == 0 || passwordToEdit.Equals(null))
        {
            GUI.Label(new Rect(256, 250, 255, 52), "Enter password...", placeholderStyle);
        }

        if (GUI.Button(new Rect(100, 460, 220, 70), "Back", buttonStyle))
        {
            SceneManager.LoadScene(0);
        }

        if (GUI.Button(new Rect(450, 460, 220, 70), "Login", buttonStyle))
        {
            StartCoroutine(LoginUser());
        }



        GUI.EndGroup();
    }

}
