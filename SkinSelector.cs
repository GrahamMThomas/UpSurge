using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    /*
    To add a new skin, add it's parameter into Repopulate and then return 
    it's value in the selectSkin() method.
    */
    public UnityEngine.UI.Text playerName;
    Dropdown skinList;
    int playerScore;
    string playerScoreText;
    string oldName = "";


    // Use this for initialization
    void Start()
    {
        skinList = GetComponent<Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerName.text != oldName && playerName.text.Replace(" ", "") != "")
        {
            oldName = playerName.text;
            skinList.options.Clear();
            playerScore = 0;
            StartCoroutine("getScore");
        }
    }

    void Repopulate()
    {
        skinList.options.Clear();
        //Default
        skinList.options.Add(new Dropdown.OptionData() { text = "Default" });
        if (playerScore >= 100)
            skinList.options.Add(new Dropdown.OptionData() { text = "Happy Face" });
        if (playerScore >= 200)
            skinList.options.Add(new Dropdown.OptionData() { text = "Soccer Ball" });
        if (playerScore >= 300)
            skinList.options.Add(new Dropdown.OptionData() { text = "Patriot" });
        if (playerScore >= 400)
            skinList.options.Add(new Dropdown.OptionData() { text = "Pokeball" });
        if (playerScore >= 500)
            skinList.options.Add(new Dropdown.OptionData() { text = "Gold" });
    }

    IEnumerator getScore()
    {
        WWW www = new WWW("http://dreamlo.com/lb/5682672e6e51b617f0be4ab2/pipe-get/" + playerName.text.Replace(" ", "_"));
        yield return www;
        try
        {
            playerScoreText = www.text.Substring(www.text.IndexOf("|") + 1);
            playerScoreText = playerScoreText.Substring(0, playerScoreText.IndexOf("|"));
        }
        catch
        {

        }
        try
        {
            playerScore = Int32.Parse(playerScoreText);
        }
        catch
        {
            playerScore = 0;
        }
        Repopulate();
    }


}
