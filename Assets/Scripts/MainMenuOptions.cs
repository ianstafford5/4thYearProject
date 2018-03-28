using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainMenuOptions : MonoBehaviour {

    bool menu = true;
    bool modal = false;

    GUIStyle style = new GUIStyle();
    GUIStyle buttonStyle = new GUIStyle();
    GUIStyle modalStyle = new GUIStyle();
    GUIStyle modalTxtStyle = new GUIStyle();
    public Font myFont;
    public Font modalFont;
    public Texture2D btnBG;
    public Texture2D hoverBtnBG;
    public Texture2D pressedBtnBG;
    public Texture2D box; 

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
        Debug.Log(login);
        login = PlayerPrefs.GetString("Login");
        Debug.Log(login);
        login = Cryptography.Decrypt(login);
        Debug.Log(login);

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

        modalStyle.normal.background = box;
        
        modalTxtStyle.wordWrap = true;
        modalTxtStyle.font = modalFont;
        modalTxtStyle.fontSize = 30;
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

    void Menu(int windowID)
    {
        GUI.Box(new Rect(0, 0, 800, 700), "");
        GUI.Label(new Rect(300, 0, 255, 52), "Main Menu", style);

        if (GUI.Button(new Rect(240, 300, 320, 70), "PLAY", buttonStyle))
        {
            if (login.Equals("false"))
            {
                menu = false;
                modal = true;
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }

        if (login.Equals("true"))
        {
            if (GUI.Button(new Rect(240, 380, 320, 70), "LEADERBOARD", buttonStyle))
            {
                SceneManager.LoadScene("Leaderboard");
            }

            if (GUI.Button(new Rect(240, 460, 320, 70), "LOG OUT", buttonStyle))
            {
                login = Cryptography.Encrypt("false");
                PlayerPrefs.SetString("Login", login);
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            if (GUI.Button(new Rect(240, 380, 320, 70), "LOGIN", buttonStyle))
            {
                SceneManager.LoadScene("Login");
            }

            if (GUI.Button(new Rect(240, 460, 320, 70), "REGISTER", buttonStyle))
            {
                SceneManager.LoadScene("Register");
            }
        }

        if (GUI.Button(new Rect(240, 540, 320, 70), "EXIT", buttonStyle))
        {
            Application.Quit();
        }
    }

    void Modal(int windowID)
    {
        GUI.Label(new Rect(60, 30, 400, 190), "Users who are not logged in can only play the first level before having to log in or register. Once you have logged in you can resume where you left off!", modalTxtStyle);

        if (GUI.Button(new Rect(80, 200, 160, 50), "BACK", buttonStyle))
        {
            modal = false;
            menu = true;
        }
        if (GUI.Button(new Rect(280, 200, 160, 50), "PLAY", buttonStyle))
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnGUI()
    {
        if (menu)
        {
            GUI.Window(0, new Rect(Screen.width / 2 - 400, Screen.height / 2 - 350, 800, 700), Menu, "", GUIStyle.none);
        }

        if (modal)
        {
            GUI.ModalWindow(1, new Rect(Screen.width / 2 - 260, Screen.height / 2 - 230, 500, 250), Modal, "", modalStyle);
        }
    }
}
