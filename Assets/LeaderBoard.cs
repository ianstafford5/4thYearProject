using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour {

    private string ScoreURL = "http://localhost:55275/unity/scores";

    bool running = false;

    public Font myFont;

    GUIStyle style = new GUIStyle();
    GUIStyle style2 = new GUIStyle();
    GUIStyle style3 = new GUIStyle();
    GUIStyle style4 = new GUIStyle();

    public Texture btnBG;

    public string score1;
    public string score2;
    public string score3;
    public string score4;
    public string score5;
    public string score6;
    public string score7;
    public string score8;
    public string score9;
    public string score10;

    public string name1;
    public string name2;
    public string name3;
    public string name4;
    public string name5;
    public string name6;
    public string name7;
    public string name8;
    public string name9;
    public string name10;

    [System.Serializable]
    private class Scores
    {
        public int ID;
        public string Name;
        public int Score;
    }

    [System.Serializable]
    private class ScoreData
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

        ScoreData info = JsonUtility.FromJson<ScoreData>(www.text);

        name1 = info.Score1.Name;
        score1 = info.Score1.Score.ToString();

        name2 = info.Score2.Name;
        score2 = info.Score2.Score.ToString();

        name3 = info.Score3.Name;
        score3 = info.Score3.Score.ToString();

        name4 = info.Score4.Name;
        score4 = info.Score4.Score.ToString();

        name5 = info.Score5.Name;
        score5 = info.Score5.Score.ToString();

        name6 = info.Score6.Name;
        score6 = info.Score6.Score.ToString();

        name7 = info.Score7.Name;
        score7 = info.Score7.Score.ToString();

        name8 = info.Score8.Name;
        score8 = info.Score8.Score.ToString();

        name9 = info.Score9.Name;
        score9 = info.Score9.Score.ToString();

        name10 = info.Score10.Name;
        score10 = info.Score10.Score.ToString();


        running = true;
    }
    
    // Use this for initialization
    void Start () {
        StartCoroutine(Score());

        style.font = myFont;
        style.fontSize = 40;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = new Color32(255, 156, 49, 255);

        style2.font = myFont;
        style2.fontSize = 30;
        style2.fontStyle = FontStyle.Bold;
        style2.normal.textColor = new Color32(255, 156, 49, 255);

        style3.font = myFont;
        style3.fontSize = 25;
        style3.fontStyle = FontStyle.Bold;
        style3.normal.textColor = Color.white;

        style4.font = myFont;
        style4.fontSize = 30;
        style4.fontStyle = FontStyle.Bold;
        style4.normal.textColor = Color.white;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if (running == true)
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));
            GUI.Box(new Rect(0, 0, 800, 600), "");
            GUI.Label(new Rect(300, 0, 255, 52), "Leaderboard", style);

            GUI.Label(new Rect(70, 45, 255, 52), "Your Highscore: ", style2);
            GUI.Label(new Rect(300, 45, 100, 52), "200 ", style4);

            GUI.Label(new Rect(70, 80, 69, 30), "Rank", style2);
            GUI.Label(new Rect(160, 80, 69, 30), "Name", style2);
            GUI.Label(new Rect(650, 80, 69, 30), "Score", style2);


            GUI.Label(new Rect(70, 120, 43, 30), "1st", style3);
            GUI.Label(new Rect(160, 120, 43, 30), name1, style3);
            GUI.Label(new Rect(650, 120, 43, 30), score1, style3);

            GUI.Label(new Rect(70, 160, 43, 30), "2nd", style3);
            GUI.Label(new Rect(160, 160, 43, 30), name2, style3);
            GUI.Label(new Rect(650, 160, 43, 30), score2, style3);

            GUI.Label(new Rect(70, 200, 43, 30), "3rd", style3);
            GUI.Label(new Rect(160, 200, 43, 30), name3, style3);
            GUI.Label(new Rect(650, 200, 43, 30), score3, style3);

            GUI.Label(new Rect(70, 240, 43, 30), "4th", style3);
            GUI.Label(new Rect(160, 240, 43, 30), name4, style3);
            GUI.Label(new Rect(650, 240, 43, 30), score4, style3);

            GUI.Label(new Rect(70, 280, 43, 30), "5th", style3);
            GUI.Label(new Rect(160, 280, 43, 30), name5, style3);
            GUI.Label(new Rect(650, 280, 43, 30), score5, style3);

            GUI.Label(new Rect(70, 320, 43, 30), "6th", style3);
            GUI.Label(new Rect(160, 320, 43, 30), name6, style3);
            GUI.Label(new Rect(650, 320, 43, 30), score6, style3);

            GUI.Label(new Rect(70, 360, 43, 30), "7th", style3);
            GUI.Label(new Rect(160, 360, 43, 30), name7, style3);
            GUI.Label(new Rect(650, 360, 43, 30), score7, style3);

            GUI.Label(new Rect(70, 400, 43, 30), "8th", style3);
            GUI.Label(new Rect(160, 400, 43, 30), name8, style3);
            GUI.Label(new Rect(650, 400, 43, 30), score8, style3);

            GUI.Label(new Rect(70, 440, 43, 30), "9th", style3);
            GUI.Label(new Rect(160, 440, 43, 30), name9, style3);
            GUI.Label(new Rect(650, 440, 43, 30), score9, style3);

            GUI.Label(new Rect(70, 480, 43, 30), "10th", style3);
            GUI.Label(new Rect(160, 480, 43, 30), name10, style3);
            GUI.Label(new Rect(650, 480, 43, 30), score10, style3);


            if (GUI.Button(new Rect(300, 520, 200, 60), btnBG, GUIStyle.none))
            {
                SceneManager.LoadScene(0);
            }
            GUI.Label(new Rect(360, 525, 255, 52), "EXIT", style);

            GUI.EndGroup();
        }
    }
}
