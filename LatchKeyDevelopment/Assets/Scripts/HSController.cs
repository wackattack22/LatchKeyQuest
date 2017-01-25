using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HSController : MonoBehaviour
{
    
    void Start()
    {
        StartGetScores();
        //startPostScores();

    }

    private string secretKey = "123456789";       // Should be same as key stored on server
    string addScoreURL = "ec2-54-242-216-198.compute-1.amazonaws.com/addscore.php?"; // EC2 address
    string highscoreURL = "ec2-54-242-216-198.compute-1.amazonaws.com/display.php";

    
    private string uniqueID;
    private string playerName;
    private int playerScore;
    

    private string[] onlineHighScore;


    // Use these to start and stop coroutines
    public void StartGetScores()
    {
        StartCoroutine(GetScores());
    }

    public void StartPostScores()
    {
        StartCoroutine(PostScores());
    }

    public void StopGetScores()
    {
        StopCoroutine(GetScores());
    }
    public void StopPostScores()
    {
        StopCoroutine(PostScores());
    }

    public string[] GetScoreList()
    {
        return onlineHighScore;
    }

    

    // set actual values before posting score
    public void UpdateOnlineHighscoreData(string name, int score)
    {
        uniqueID = Random.Range(1,10000).ToString();
        playerName = name;
        playerScore = score;
    }

    // MD5 hash to validate data
    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    
    IEnumerator PostScores()
    {               
            // This connects to a server side php script that will add the name and score to a MySQL DB.
            // updateOnlineHighScoreData() must be called prior to starting this
            string hash = Md5Sum(playerName + playerScore + secretKey);
            
            string post_url = addScoreURL + "uniqueID=" + uniqueID + "&name=" + WWW.EscapeURL(playerName) + "&score=" + playerScore + "&hash=" + hash;
            
            // Post the URL to the site and create a download object to get the result.
            // Do not use https!! server not configured for it
            WWW hs_post = new WWW("http://" + post_url);

            yield return hs_post; // Wait until the download is done
        
            if (hs_post.error != null)
            {
                //print("There was an error posting the high score: " + hs_post.error);
            }  
    }


    // Get the scores from the MySQL DB, convert to string array
    // Needs to be started in Start(), then also called when ready to display scores
    IEnumerator GetScores()
    {
        WWW hs_get = new WWW("http://" + highscoreURL);

        yield return hs_get;

        if (hs_get.error != null)
        {
            //Debug.Log("There was an error getting the high score: " + hs_get.error);

        }
        else
        {
            // Gets top 10 scores
            // Returns a string, split and convert to array
            string help = hs_get.text;

            onlineHighScore = help.Split(";"[0]);
        }  
    }
}
