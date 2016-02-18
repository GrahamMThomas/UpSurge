using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DisplayScoreBoard : MonoBehaviour
{
    UnityEngine.UI.Text scoreBoard;
    WWW www;
    List<string> names = new List<string>();
    List<string> scores = new List<string>();
    // Use this for initialization
    void Start()
    {
        scoreBoard = GetComponent<UnityEngine.UI.Text>();
        StartCoroutine("getTop10");

    }

    IEnumerator getTop10()
    {
        www = new WWW("http://dreamlo.com/lb/5682672e6e51b617f0be4ab2/quoted/11");
        yield return www;
        print(www.text);
        //Concactanation
        string editedStr = www.text.Substring(1);
        try
        {
            for (int i = 0; i < 11; i++)
            {
                names.Add(editedStr.Substring(0, editedStr.IndexOf(",") - 1));
                editedStr = editedStr.Substring(editedStr.IndexOf(",") + 2);
                scores.Add(editedStr.Substring(0, editedStr.IndexOf(",") - 1));
                editedStr = editedStr.Substring(editedStr.IndexOf("\n") + 1);
                editedStr = editedStr.Substring(1);
            }
        }
        catch
        {
            //print("Not enough Scores!" + e);
        }
        showMeTheScores();
    }

    void showMeTheScores()
    {
        for (int j = names.Count-1; j != -1; j--)
        {
            names[j] = names[j].Replace("_", " ");
            for (int i = names[j].Length; i < LeaderBoard.MAXCHARACTERS + 1; i++)
            {
                names[j] += " ";
            }
        }
        int k = 0;
        scoreBoard.text = "";
        try
        {
            for (k = 0; k < 11; k++)
            {
                scoreBoard.text = scoreBoard.text + (k + 1) + ". " + names[k] + "          " + scores[k] + "\n\n";
            }
        }
        catch
        {
            for (int bob = 5; k < 11; k++)
            {
                scoreBoard.text = scoreBoard.text + (k + 1) + ". " + "\n\n";
                bob++;
            }
        }

    }

    public void LoadGame()
    {
        Application.LoadLevel(2);
    }
}
