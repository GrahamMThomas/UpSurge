using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Tutorial : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        try
        {
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            file.Close();
            Application.LoadLevel(1);
        }
        catch
        {
            print("File not found! Creating...");
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playGame()
    {
        Application.LoadLevel(1);
    }

    public void openTutorial()
    {
        Application.LoadLevel(0);
    }

}
