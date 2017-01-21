using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroMenu : MonoBehaviour {

    //public GUIStyle guiStyle;
    
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
        int space = 5;

        BeginPage(200,200);
        if (GUILayout.Button("Start Game"))
        {
            SceneManager.LoadScene("Entryway");
        }
        GUILayout.Space(space);
        if (GUILayout.Button("Options"))
        {
            currentPage = Page.Options;
        }
        GUILayout.Space(space);
        if (GUILayout.Button("High Score"))
        {
            currentPage = Page.HighScore;
        }
        GUILayout.Space(space);
        if (GUILayout.Button("Quit"))
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
        

        BeginPage(300, 200);
        /*
        GetComponent<HSController>().startGetScores();
        string[] scoreList = HSController.GetScoreList();


        int width = 300;
        int height = 60;
        int posy = 125;
        int posx = (Screen.width - width) / 2;
        int j = 0;
        for (int i = 1; i <= 5; i++)
        {
            GUI.Label(new Rect(posx - width, posy + (height * i), width, height), i + ". " + scoreList[j++], guiStyle);
            GUI.Label(new Rect(posx + width, posy + (height * i), width, height), scoreList[j++], guiStyle);
        }

        GUI.Label(new Rect(posx, posy, width, height), "Highscores", guiStyle);
        */
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
            width, height));
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
        if (GUI.Button(new Rect(20, Screen.height - 50, 50, 20), "Back"))
        {
            currentPage = Page.Main;
        }
    }
}
