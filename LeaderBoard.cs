using UnityEngine;
using System.Collections;

public class LeaderBoard : MonoBehaviour
{

    //http://dreamlo.com/lb/ioTGKT9g7Emq3JYLSlNkxgFeMmUWta60i9m-ApP_MItw
    string PRIVATEURL = "ioTGKT9g7Emq3JYLSlNkxgFeMmUWta60i9m-ApP_MItw";
    string PUBLICURL = "5682672e6e51b617f0be4ab2";

    public const int MAXCHARACTERS = 16;
    public int totalScore = 0;
    public string playerName;
    public string highScores = "";

    void Update()
    {

    }

    public void saveScore()
    {
        if (playerName != null)
        {
            StartCoroutine("AddScore");
        }
    }

    // This function saves a trip to the server. Adds the score and retrieves results in one trip.
    IEnumerator AddScoreWithPipe()
    {
        WWW www = new WWW(PRIVATEURL + "/add-pipe/" + WWW.EscapeURL(playerName) + "/" + totalScore.ToString());
        yield return www;
        highScores = www.text;
    }

    // Or you can use these too...
    IEnumerator AddScore()
    {
        WWW www = new WWW("http://dreamlo.com/lb/" + PRIVATEURL + "/add/" + playerName + "/" + totalScore);
        yield return www;
    }

    IEnumerator GetScores()
    {
        WWW www = new WWW(PUBLICURL + "/pipe");
        yield return www;
        highScores = www.text;
    }

}
