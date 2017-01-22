using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameOver : MonoBehaviour {

    public GUIStyle guiStyle;

    private Color color;

    private int playerScore;

    private string playerName;

    private HSController hs;

    public enum Page
    {
        PlayerScore, Quit
    }

    private Page currentPage;

    // Use this for initialization
    void Start ()
    {
        playerScore = PlayerController.totalScore;
        color = Color.white;
        currentPage = Page.PlayerScore;
        playerName = "Enter Name";
        

    }
	
	// Update is called once per frame
    /*
	void Update ()
    {
	}*/

    void OnGUI()
    {
        GUI.color = color; 

        switch (currentPage)
        {
            case Page.PlayerScore: displayText(); break;
            case Page.Quit: quitPage(); break;
        } 
    }

    void displayText()
    {
        int width = 600;
        int height = 160;

        GUI.Label(new Rect((Screen.width - width) / 2, 120, width, height), "Final Score: " + playerScore, guiStyle);

        // Needs fix, has to wait for GetScores to return before condition can be evaluated
        /*
        if (checkHighScore(playerScore))
        {
            GUI.Label(new Rect((Screen.width - width) / 2, 200, width, height), "NEW HIGH SCORE!", guiStyle);
            GUI.Box(new Rect((Screen.width - width) / 2, 400, width, height), "");
            playerName = GUI.TextField(new Rect(((Screen.width - width) / 2), 400, width - 20, height), playerName, 10, guiStyle);
        }
        else
        {
            GUI.Label(new Rect((Screen.width - width) / 2, 200, width, height), "Try harder next time :(", guiStyle);
        }
        */
        GUI.Box(new Rect((Screen.width - width) / 2, 439, width, height/2), "");    //Box
        playerName = GUI.TextField(new Rect(((Screen.width - width+20) / 2), 400, width - 20, height), playerName, 10, guiStyle);


        if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 100, 100, 50), "Next",guiStyle))
        {
            //int temp = Random.Range(500, 1000);
            GetComponent<HSController>().updateOnlineHighscoreData(playerName, playerScore);    //SCORE
            GetComponent<HSController>().startPostScores();
            
            currentPage = Page.Quit;
        }

    }
 

    void quitPage()
    {



        GetComponent<HSController>().startGetScores();
        string[] scoreList = GetComponent<HSController>().GetScoreList();


        int width = 300;
        int height = 60;
        int posy = 125;
        int posx = (Screen.width - width) / 2 ;
        int j = 0;
        int len = 5 > scoreList.Length / 2 ? scoreList.Length / 2 : 5;
        for (int i = 1; i <= len; i++)
        {
            GUI.Label(new Rect(posx - width, posy + (height * i), width+20, height), i+". "+scoreList[j++],guiStyle);
            GUI.Label(new Rect(posx + width, posy + (height * i), width, height), scoreList[j++],guiStyle);
        }
        
        GUI.Label(new Rect(posx, posy, width, height), "Highscores", guiStyle);
        
        if (GUI.Button(new Rect(25, Screen.height - 100, 100, 50), "Main Menu",guiStyle))
        {
            GetComponent<HSController>().stopGetScores();
            GetComponent<HSController>().stopPostScores();
            GUILayout.EndArea();
            SceneManager.LoadScene("Intro Scene");
            
        }
        if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 100, 100, 50), "Quit", guiStyle))
        {
            Application.Quit();
        } 
    }

    bool checkHighScore(int playerScore)
    {
        
        string[] scoreList = GetComponent<HSController>().GetScoreList();
        if (scoreList != null)
        {
            int[] scores = new int[5];
            int j = 0;
            int len = 5 > scoreList.Length / 2 ? scoreList.Length / 2 : 5;
            for (int i = 1; i <= len; i += 2)
            {
                //scores[j++] = Convert.ToInt32(scoreList[i]);
                Debug.Log(scoreList[i]);
            }
            foreach (int score in scores)
            {
                if (playerScore >= score)
                {
                    return true;
                }
            }
            return false;
        }
        return false;
        

    }
}
