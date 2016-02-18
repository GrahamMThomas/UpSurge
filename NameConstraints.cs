using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class NameConstraints : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("10orLess").GetComponent<Text>().text = LeaderBoard.MAXCHARACTERS + " Letters or Less" + "\n" + "No Symbols";
    }

    public void loadTutorial()
    {

        File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        Application.LoadLevel(0);
    }
}
