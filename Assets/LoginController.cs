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

public class LoginController : MonoBehaviour {
    private const int SUCCESS_CODE = 200;
    //private const int BAD_REQUEST_CODE = 400;

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


    //public static string Encrypt(string input, string key = "sblw-3hn8-sqoy19")
    //{
    //    byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
    //    TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
    //    tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
    //    tripleDES.Mode = CipherMode.ECB;
    //    tripleDES.Padding = PaddingMode.PKCS7;
    //    ICryptoTransform cTransform = tripleDES.CreateEncryptor();
    //    byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
    //    tripleDES.Clear();
    //    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    //}
    //public static string Decrypt(string input, string key = "sblw-3hn8-sqoy19")
    //{
    //    byte[] inputArray = Convert.FromBase64String(input);
    //    TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
    //    tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
    //    tripleDES.Mode = CipherMode.ECB;
    //    tripleDES.Padding = PaddingMode.PKCS7;
    //    ICryptoTransform cTransform = tripleDES.CreateDecryptor();
    //    byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
    //    tripleDES.Clear();
    //    return UTF8Encoding.UTF8.GetString(resultArray);
    //}

    // Use this for initialization
    void Start () {
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
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("Email", emailToEdit);
        form.AddField("Password", passwordToEdit);
        form.AddField("RemeberMe", 0);
        Debug.Log("SEND2");

        using (WWW www = new WWW(LoginURL, form))
        {
            //UnityWebRequest www = UnityWebRequest.Post("http://localhost:1107/api/Hello/Ian", form);
            yield return www;

            int responseCode = GetResponseCode(www);
            print(responseCode);

            if (responseCode != SUCCESS_CODE)
            {
                loginError = "The email or password you provided is incorrect";
            }
            else
            {
                string login = Cryptography.Encrypt("true");
                print(login);
                PlayerPrefs.SetString("Login", login);
                SceneManager.LoadScene(0);
            }
        }

    }

    public static int GetResponseCode(WWW request)
    {
        int ret = 0;

        if (request.responseHeaders == null)
        {
            Debug.Log("No Response Headers");
        }
        else
        {
            if (!request.responseHeaders.ContainsKey("STATUS"))
            {
                Debug.Log("Response Headers has no status");
            }
            else
            {
                ret = ParseResponseCode(request.responseHeaders["STATUS"]);
            }
        }

        return ret;
    }


    public static int ParseResponseCode(string statusLine)
    {
        int ret = 0;

        string[] components = statusLine.Split(' ');
        if (components.Length < 3)
        {
            Debug.Log("Invalid Response Status: " + components[1]);
        }
        else
        {
            if (!int.TryParse(components[1], out ret))
            {
                Debug.Log("Invalid Respnse Code: " + components[1]);
            }
        }

        return ret;
    }


    private void OnGUI()
    {

        GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));
        GUI.Box(new Rect(0, 0, 800, 600), "");
        GUI.Label(new Rect(340, 0, 255, 52), "LOGIN", style);

        GUI.skin.textField.fontSize = 25;
        GUI.skin.textField.alignment = TextAnchor.MiddleLeft;

        GUI.Label(new Rect(250, 180, 300, 100), loginError, errorStyle);

        GUI.Label(new Rect(250, 120, 255, 52), "Email:", labelStyle);
        emailToEdit = GUI.TextField(new Rect(250, 150, 300, 30), emailToEdit, 25);
        if (emailToEdit.Length == 0 || emailToEdit.Equals(null))
        {
            GUI.Label(new Rect(254, 150, 255, 52), "Enter email address...", placeholderStyle);
        }

        GUI.Label(new Rect(250, 220, 255, 52), "Password:", labelStyle);
        passwordToEdit = GUI.PasswordField(new Rect(250, 250, 300, 30), passwordToEdit, '*', 25);
        if (passwordToEdit.Length == 0 || passwordToEdit.Equals(null))
        {
            GUI.Label(new Rect(254, 250, 255, 52), "Enter password...", placeholderStyle);
        }        

        if (GUI.Button(new Rect(100, 460, 220, 70), "Back", buttonStyle))
        {
            SceneManager.LoadScene(0);
        }

        if (GUI.Button(new Rect(450, 460, 220, 70), "Login", buttonStyle))
        {
            StartCoroutine(Login());
        }


        GUI.EndGroup();
    }

}
