//using System;
//using System.Security.Cryptography;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Security.AccessControl;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using System.Text;

//public class Login1 : MonoBehaviour
//{

//    public static string Encrypt(string input, string key = "sblw-3hn8-sqoy19")
//    {
//        byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
//        TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
//        tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
//        tripleDES.Mode = CipherMode.ECB;
//        tripleDES.Padding = PaddingMode.PKCS7;
//        ICryptoTransform cTransform = tripleDES.CreateEncryptor();
//        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
//        tripleDES.Clear();
//        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
//    }
//    public static string Decrypt(string input, string key = "sblw-3hn8-sqoy19")
//    {
//        byte[] inputArray = Convert.FromBase64String(input);
//        TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
//        tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
//        tripleDES.Mode = CipherMode.ECB;
//        tripleDES.Padding = PaddingMode.PKCS7;
//        ICryptoTransform cTransform = tripleDES.CreateDecryptor();
//        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
//        tripleDES.Clear();
//        return UTF8Encoding.UTF8.GetString(resultArray);
//    }

//    // Use this for initialization
//    void Start()
//    {
//        string str = "200";
//        print(str);
//        string encrypt = Encrypt(str);
//        print(encrypt);

//        PlayerPrefs.SetString("Score", encrypt);

//        string decrypt = PlayerPrefs.GetString("Score");
//        print(decrypt);
//        decrypt = Decrypt(decrypt);
//        print(decrypt);

//        int currentScore = PlayerPrefs.GetInt("Score");

//        string i = "23242";

//        int it = 0;

//        Int32.TryParse(i, out it);

//        print(it);

//        string s = i.ToString();

//        print(s);
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    GUIStyle style = new GUIStyle();
//    GUIStyle buttonStyle = new GUIStyle();
//    GUIStyle placeholderStyle = new GUIStyle();
//    GUIStyle labelStyle = new GUIStyle();
//    GUIStyle errorStyle = new GUIStyle();
//    public Font myFont;
//    public Texture2D btnBG;
//    public Texture2D hoverBtnBG;
//    public Texture2D pressedBtnBG;
//    public string emailToEdit = "";
//    public string passwordToEdit = "";
//    public string emailError = " ";
//    public string passwordError = " ";

//    private void OnGUI()
//    {
//        style.font = myFont;
//        style.fontSize = 40;
//        style.fontStyle = FontStyle.Bold;
//        style.normal.textColor = new Color32(255, 156, 49, 255);

//        buttonStyle.font = myFont;
//        buttonStyle.fontSize = 40;
//        buttonStyle.fontStyle = FontStyle.Bold;
//        buttonStyle.normal.textColor = new Color32(255, 156, 49, 255);
//        buttonStyle.hover.textColor = new Color32(255, 156, 49, 255);
//        buttonStyle.active.textColor = new Color32(255, 156, 49, 255);
//        buttonStyle.normal.background = btnBG;
//        buttonStyle.hover.background = hoverBtnBG;
//        buttonStyle.active.background = pressedBtnBG;
//        buttonStyle.alignment = TextAnchor.MiddleCenter;

//        placeholderStyle.fontSize = 25;
//        placeholderStyle.fontStyle = FontStyle.Italic;
//        placeholderStyle.normal.textColor = Color.grey;

//        labelStyle.fontSize = 25;
//        labelStyle.normal.textColor = Color.white;

//        errorStyle.fontSize = 20;
//        errorStyle.fontStyle = FontStyle.Bold;
//        errorStyle.normal.textColor = new Color32(197, 3, 3, 255);

//        GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));
//        GUI.Box(new Rect(0, 0, 800, 600), "");
//        GUI.Label(new Rect(340, 0, 255, 52), "LOGIN", style);

//        GUI.Label(new Rect(250, 120, 255, 52), "Email:", labelStyle);
//        emailToEdit = GUI.TextField(new Rect(250, 150, 300, 30), emailToEdit, 25);
//        if (emailToEdit.Length == 0 || emailToEdit.Equals(null))
//        {
//            GUI.Label(new Rect(254, 150, 255, 52), "Enter email address...", placeholderStyle);
//        }
//        GUI.Label(new Rect(250, 180, 300, 100), emailError, errorStyle);

//        GUI.Label(new Rect(250, 220, 255, 52), "Password:", labelStyle);
//        passwordToEdit = GUI.PasswordField(new Rect(250, 250, 300, 30), passwordToEdit, '*', 25);
//        if (passwordToEdit.Length == 0 || passwordToEdit.Equals(null))
//        {
//            GUI.Label(new Rect(254, 250, 255, 52), "Enter password...", placeholderStyle);
//        }
//        GUI.Label(new Rect(250, 280, 300, 100), passwordError, errorStyle);

//        if (GUI.Button(new Rect(100, 460, 220, 70), "Back", buttonStyle))
//        {
//            SceneManager.LoadScene(0);
//        }

//        if (GUI.Button(new Rect(450, 460, 220, 70), "Login", buttonStyle))
//        {

//        }

//        GUI.EndGroup();
//    }

//    private string LoginURL = "";

//    private int responseCode;

//    private IEnumerator Login()
//    {
//        Debug.Log("********SEND1******");
//        WWWForm form = new WWWForm();
//        Debug.Log(emailToEdit);
//        Debug.Log(passwordToEdit);
//        form.AddField("Email", emailToEdit);
//        form.AddField("Password", passwordToEdit);
//        Debug.Log("SEND2");

//        using (WWW www = new WWW(LoginURL, form))
//        {
//            //UnityWebRequest www = UnityWebRequest.Post("http://localhost:1107/api/Hello/Ian", form);
//            yield return www;

//            responseCode = GetResponseCode(www);

//            print(responseCode);

//        }
//    }

//    public static int GetResponseCode(WWW request)
//    {
//        int ret = 0;

//        if (request.responseHeaders == null)
//        {
//            Debug.Log("No Response Headers");
//        }
//        else
//        {
//            if (!request.responseHeaders.ContainsKey("STATUS"))
//            {
//                Debug.Log("Response Headers has no status");
//            }
//            else
//            {
//                ret = ParseResponseCode(request.responseHeaders["STATUS"]);
//            }
//        }

//        return ret;
//    }


//    public static int ParseResponseCode(string statusLine)
//    {
//        int ret = 0;

//        string[] components = statusLine.Split(' ');
//        if (components.Length < 3)
//        {
//            Debug.Log("Invalid Response Status: " + components[1]);
//        }
//        else
//        {
//            if (!int.TryParse(components[1], out ret))
//            {
//                Debug.Log("Invalid Respnse Code: " + components[1]);
//            }
//        }

//        return ret;
//    }
//}
