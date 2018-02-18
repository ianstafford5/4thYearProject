using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainMenuOptions : MonoBehaviour {

    public string username;
    public string email;
    public string password;
    public string confirmPassword;
    public string score;

    string SendScoreURL = "http://localhost:55275/Home/About?";
    string RegisterUserURL = "http://localhost:52272/api/Stock?";

    void Start()
    {
        StartCoroutine(GetHello());
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }

    //public void SendScore()
    //{
    //  Application.OpenURL("http://localhost:55275/Home/About?name=ian&score=100");
    //}

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


    /*public void Register()
    {
        Debug.Log("********SEND1******");
        WWWForm form = new WWWForm();
        form.AddField("Email", email);
        form.AddField("Password", password);
        Debug.Log("SEND2");
        WWW www = new WWW(RegisterUserURL, form);
        UnityWebRequest request = new UnityWebRequest();
    }*/

    public IEnumerator GetHello()
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
    }

}
