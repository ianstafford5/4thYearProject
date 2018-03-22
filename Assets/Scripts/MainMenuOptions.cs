using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainMenuOptions : MonoBehaviour {

    GUIStyle style = new GUIStyle();
    GUIStyle buttonStyle = new GUIStyle();
    public Font myFont;
    public Texture2D btnBG;
    public Texture2D hoverBtnBG;
    public Texture2D pressedBtnBG;

    private string login;
    public string username;
    public string email;
    public string password;
    public string confirmPassword;
    public string score;

    string SendScoreURL = "http://localhost:55275/Home/About?";
    string RegisterUserURL = "http://localhost:52272/api/Stock?";

    void Start()
    {
        login = PlayerPrefs.GetString("Login");
        print(login);
        login = Cryptography.Decrypt(login);
        print(login);

        style.font = myFont;
        style.fontSize = 40;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = new Color32(255, 156, 49, 255); ;

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
    }

    public void SendScore()
    {
        Debug.Log("********SEND1******");
        WWWForm form = new WWWForm();
        form.AddField("Name",username);
        form.AddField("Score", score);
        Debug.Log("********SEND2******");
        Debug.Log(form);
        WWW www = new WWW(SendScoreURL, form);
    }


    //public IEnumerator GetHello()
    //{
    //    Debug.Log("Message");
    //    UnityWebRequest www = UnityWebRequest.Get("http://localhost:1107/api/Hello/Ian");
    //    yield return www.Send();

    //    if (www.isNetworkError || www.isHttpError)
    //    {
    //        Debug.Log("Error");
    //    }
    //    else
    //    {
    //        Debug.Log(www.downloadHandler.text);
    //    }
    //}

    private void OnGUI()
    {

        GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 350, 800, 700));
        GUI.Box(new Rect(0, 0, 800, 700), "");
        GUI.Label(new Rect(300, 0, 255, 52), "Main Menu", style);

        if (GUI.Button(new Rect(240, 300, 320, 70), "PLAY", buttonStyle))
        {
            SceneManager.LoadScene(1);
        }

        if (!login.Equals("true"))
        {
            if (GUI.Button(new Rect(240, 380, 320, 70), "LOGIN", buttonStyle))
            {
                SceneManager.LoadScene("Login");
            }

            if (GUI.Button(new Rect(240, 460, 320, 70), "REGISTER", buttonStyle))
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            if (GUI.Button(new Rect(240, 380, 320, 70), "LEADERBOARD", buttonStyle))
            {
                SceneManager.LoadScene(0);
            }

            if (GUI.Button(new Rect(240, 460, 320, 70), "LOG OUT", buttonStyle))
            {
                SceneManager.LoadScene(0);
            }
        }

        if (GUI.Button(new Rect(240, 540, 320, 70), "EXIT", buttonStyle))
        {
            Application.Quit();
        }
        GUI.EndGroup();
    }
}
