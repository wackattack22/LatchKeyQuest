using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        playerName = "Enter Your Name Here";
        

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
        
            GUI.Label(new Rect((Screen.width - width) / 2, 200, width, height), "NEW HIGH SCORE!", guiStyle);
            GUI.Box(new Rect((Screen.width - width) / 2, 400, width, height), "");
            playerName = GUI.TextField(new Rect(((Screen.width - width) / 2), 400, width-20, height), playerName, 20, guiStyle);



        
        if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 100, 100, 50), "Next",guiStyle))
        {
            //int temp = Random.Range(500, 1000);
            HSController.updateOnlineHighscoreData(playerName, playerScore);    //SCORE
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
        for (int i = 1; i <= 5; i++)
        {
            GUI.Label(new Rect(posx - width, posy + (height * i), width, height), i+". "+scoreList[j++],guiStyle);
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

    
}
