using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroMenu : MonoBehaviour {

    public GUIStyle guiStyle;

    public GUIStyle mainMenuStyle;
    
    private float startTime;

    private int toolbarInt;

    private string[] toolbarstrings = { "Audio", "Graphics", "Stats" };

    private Color color;

    private HSController hs;

    public enum Page
    {
        Main, Options, HighScore
    }

    private Page currentPage;

    // Use this for initialization
    void Start ()
    {
        startTime = 0.1f;
        currentPage = Page.Main;
        toolbarInt = 0;
        color = Color.white;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnGUI()
    {
        GUI.color = color;
        switch (currentPage)
        {
            case Page.Main: mainMenu(); break;
            case Page.Options: showToolbar(); break;
            case Page.HighScore: showHighScore(); break;
        }
    }

    void mainMenu()
    {
        int space = 20;

        BeginPage(300,200);
        if (GUILayout.Button("Start Game", mainMenuStyle))
        {
            SceneManager.LoadScene("Entryway");
        }
        GUILayout.Space(space);
        if (GUILayout.Button("Options", mainMenuStyle))
        {
            currentPage = Page.Options;
        }
        GUILayout.Space(space);
        if (GUILayout.Button("High Score", mainMenuStyle))
        {
            currentPage = Page.HighScore;
        }
        GUILayout.Space(space);
        if (GUILayout.Button("Quit", mainMenuStyle))
        {
            Application.Quit();
        }
        EndPage();
    }

    void showToolbar()
    {
        BeginPage(300, 200);
        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarstrings);
        switch (toolbarInt)
        {
            case 0: VolumeControl(); break;
            case 1: Qualities(); QualityControl(); break;
            case 2: ShowDevice(); break;
        }
        EndPage();
    }

    void showHighScore()
    {
        

        BeginPage(300, 500);
        
        GetComponent<HSController>().StartGetScores();
        string[] scoreList = GetComponent<HSController>().GetScoreList();

        int height = 25;
        int width = 150;

        GUI.Label(new Rect(-320, 0, width, height), "\tHighscores", mainMenuStyle);
       
        int j = 0;
        int len = 10 > scoreList.Length / 2 ? scoreList.Length / 2 : 10;
        for (int i = 1; i <= len; i++)
        {
            if (i < 10)
            {
                GUI.Label(new Rect(0, height * (2 * i), width, height), i + ".  " + scoreList[j++], guiStyle);
            }
            else
            {
                GUI.Label(new Rect(0, height * (2 * i), width, height), i + ". " + scoreList[j++], guiStyle);
            }
            
            GUI.Label(new Rect(width+150, height*(2*i), width, height), scoreList[j++], guiStyle);
        }
               
        EndPage();  
    }

    bool IsBeginning()
    {
        return (Time.time < startTime);
    }

    void ShowDevice()
    {
        GUILayout.Label("Unity player version " + Application.unityVersion);
        GUILayout.Label("Graphics: " + SystemInfo.graphicsDeviceName + " " +
            SystemInfo.graphicsMemorySize + "MB\n" +
            SystemInfo.graphicsDeviceVersion + "\n" +
            SystemInfo.graphicsDeviceVendor);
        GUILayout.Label("Shadows: " + SystemInfo.supportsShadows);
        GUILayout.Label("Image Effects: " + SystemInfo.supportsImageEffects);
        GUILayout.Label("Render Textures: " + SystemInfo.supportsRenderTextures);
    }

    void Qualities()
    {
        switch (QualitySettings.GetQualityLevel())
        {
            case 0:
                GUILayout.Label("Fastest");
                break;
            case 1:
                GUILayout.Label("Fast");
                break;
            case 2:
                GUILayout.Label("Simple");
                break;
            case 3:
                GUILayout.Label("Good");
                break;
            case 4:
                GUILayout.Label("Beautiful");
                break;
            default:
                GUILayout.Label("Fantastic");
                break;
        }
    }

    void QualityControl()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Decrease"))
        {
            QualitySettings.DecreaseLevel();
        }
        if (GUILayout.Button("Increase"))
        {
            QualitySettings.IncreaseLevel();
        }
        GUILayout.EndHorizontal();
    }

    void VolumeControl()
    {
        GUILayout.Label("Volume");
        AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
    }

    void BeginPage(int width, int height)
    {
        GUILayout.BeginArea(new Rect(
            ((Screen.width / 2) - width) / 2 + (Screen.width / 2),
            (Screen.height - height) / 2,
            width+100, height*2));
    }

    void EndPage()
    {
        GUILayout.EndArea();
        if (currentPage != Page.Main)
        {
            ShowBackButton();
        }
    }

    void ShowBackButton()
    {
        if (GUI.Button(new Rect(20, Screen.height - 50, 100, 20), "Back", mainMenuStyle))
        {
            currentPage = Page.Main;
        }
    }
}
