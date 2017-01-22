using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HSController : MonoBehaviour
{
    
    void Start()
    {
        startGetScores();
        //startPostScores();

        //
        //HSController.Instance.startGetScores ();
    }

    private string secretKey = "123456789"; // Should be same as key stored on server
    string addScoreURL = "ec2-54-242-216-198.compute-1.amazonaws.com/addscore.php?"; // EC2 address
    string highscoreURL = "ec2-54-242-216-198.compute-1.amazonaws.com/display.php";

    //for testing
    private string uniqueID;
    private string name;
    private int score;
    

    private string[] onlineHighScore;

    public void startGetScores()
    {
        StartCoroutine(GetScores());
    }

    public void startPostScores()
    {
        StartCoroutine(PostScores());
    }

    public void stopGetScores()
    {
        StopCoroutine(GetScores());
    }
    public void stopPostScores()
    {
        StopCoroutine(PostScores());
    }

    public string[] GetScoreList()
    {
        return onlineHighScore;
    }

    

    //set actual values before posting score
    public void updateOnlineHighscoreData(string playerName, int playerScore)
    {
        // uniqueID, name and score will get the actual value before posting score

        uniqueID = Random.Range(1,10000).ToString();
        name = playerName;
        score = playerScore;
        //startPostScores();
    }

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

    // remember to use StartCoroutine when calling this function!
    IEnumerator PostScores()
    {
        
            
            //This connects to a server side php script that will add the name and score to a MySQL DB.
            // Supply it with a string representing the players name and the players score.
            string hash = Md5Sum(name + score + secretKey);
            
            string post_url = addScoreURL + "uniqueID=" + uniqueID + "&name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
            
            // Post the URL to the site and create a download object to get the result.
            // Do not use https!!
            WWW hs_post = new WWW("http://" + post_url);

            yield return hs_post; // Wait until the download is done
        
            if (hs_post.error != null)
            {
                print("There was an error posting the high score: " + hs_post.error);
            }
            
        
        
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScores()
    {
        WWW hs_get = new WWW("http://" + highscoreURL);

        yield return hs_get;

        if (hs_get.error != null)
        {
            Debug.Log("There was an error getting the high score: " + hs_get.error);

        }
        else
        {
            // Gets top 10 scores
            //Change .text into string to use Substring and Split
            string help = hs_get.text;

            
            //200 is maximum length of highscore - 100 Positions (name+score)

            onlineHighScore = help.Split(";"[0]);
            

        }

        
    }


}
