﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour {

	public static int[] lvlScores = new int[4];
	int currentScene;
	int totalScore;
    public Texture img;

    public GUIStyle guiStyle;
    

	/**
        **** Scoring Table ****
        Level Score = Points - Elapsed Time
        Max points possible (assumes 0 elapsed time)      
        Level 1: 50
            5 boblins x 10 points = 50
        
        Level 2: 45
            2 boblins x 10 points = 20
            1 lobster x 25 points = 25
        
        Level 3: 105
            3 boblins x 10 points = 30
            3 lobsters x 25 points = 75
        Total = 200
    */

	void OnGUI() {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, 30), img);
        //GUI.Label(new Rect(0, 0, Screen.width, 30), img);
        GUI.Label(new Rect(10, 10, 100, 20), "Score: " + PlayerController.lvlScore.ToString(),guiStyle);
		GUI.Label(new Rect(130, 10, 100, 20), "Life: " + PlayerController.lifeCount.ToString(), guiStyle);
		GUI.Label(new Rect(250, 10, 100, 20), "Time: " + Mathf.Floor(PlayerController.time).ToString(), guiStyle);

		currentScene = SceneManager.GetActiveScene().buildIndex;

		if (currentScene > 1)
		{
			int j = 1;
			for(int i = 0; i < currentScene-2; i++) {

				GUI.Label(new Rect((Screen.width / 2) + j, 10, 100, 20), "Level " + (i+1)
					+ ": " + lvlScores[i].ToString(),guiStyle);
				j+=150;
			}
			if (currentScene == lvlScores.Length && PlayerController.lvlComplete)
			{
				//Debug.Log(currentScene);
				GUI.Label(new Rect((Screen.width / 2) + j, 10, 100, 20), "Level " + (currentScene)
					+ ": " + lvlScores[currentScene-2].ToString(),guiStyle);

				j += 150;
                /*
				string rank = "";

				totalScore = PlayerController.totalScore;

				if (totalScore < 100)
					rank = "Lame!";
				else if (totalScore >= 100 && totalScore < 275)
					rank = "Rookie";             
				else if (totalScore >= 275 && totalScore < 450)
					rank = "Semi-Pro";               
				else if (totalScore >= 450)
					rank = "Pro!";

				j += 20;
				GUI.Label(new Rect(Screen.width - 150, j, 100, 20), "Total Score: " + totalScore.ToString());
				j += 20;
				GUI.Label(new Rect(Screen.width - 150, j, 100, 20), "Rank: " + rank); 
                */
			}

		}
	}
}