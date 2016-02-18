using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataHandling : MonoBehaviour
{
    public GameObject floor;
    public GameObject player;
    public UnityEngine.UI.Text TenOrLess;
    string ballSkinText;
    [Serializable]
    public class PlayerData
    {
        //Put data in here.
        public int highScore;
        public int recentScore;
        public string playerName;
        public string ballSkin;
        public float mouseSensitivity;
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            //Setters here.
            ballSkinText = data.ballSkin;
            player.GetComponent<Renderer>().material = player.GetComponent<Player>().selectSkin(data.ballSkin);
            floor.GetComponent<Driver>().highScore = data.highScore;
            floor.GetComponent<Driver>().recentHeight = data.recentScore;
            floor.GetComponent<LeaderBoard>().playerName = data.playerName;
            if (data.mouseSensitivity == 0)
            {
                player.GetComponent<MouseLook>().sensitivityX = 5;
                player.GetComponent<MouseLook>().sensitivityY = 5;
            }
            else
            {
                player.GetComponent<MouseLook>().sensitivityX = data.mouseSensitivity;
                player.GetComponent<MouseLook>().sensitivityY = data.mouseSensitivity;
            }
            file.Close();
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        //All My variables
        data.ballSkin = ballSkinText;
        data.playerName = floor.GetComponent<LeaderBoard>().playerName;
        data.highScore = floor.GetComponent<Driver>().highScore;
        data.recentScore = floor.GetComponent<Driver>().recentHeight;
        data.mouseSensitivity = player.GetComponent<MouseLook>().sensitivityX;
        //End Variables
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadButton()
    {
        if (TenOrLess.text.Length <= LeaderBoard.MAXCHARACTERS + 1)
        {
            //Load
            BinaryFormatter bf = new BinaryFormatter();
            PlayerData data = new PlayerData();
            try
            {
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
                data = (PlayerData)bf.Deserialize(file);
                file.Close();
            }
            catch
            {
                print("File not found! Creating...");
                File.Delete(Application.persistentDataPath + "/playerInfo.dat");
            }
            //Save

            FileStream sfile = File.Create(Application.persistentDataPath + "/playerInfo.dat");
            //Set the Name
            string nameBeforeSpaces = GameObject.Find("playerNameBox").GetComponent<Text>().text;
            string nameFixed = nameBeforeSpaces.Replace(" ", "_");
            data.playerName = nameFixed;
            data.ballSkin = GameObject.Find("SkinLabel").GetComponent<UnityEngine.UI.Text>().text;
            bf.Serialize(sfile, data);
            sfile.Close();
            Application.LoadLevel(2);
        }
        else
        {
            UnityEngine.UI.Text tooLong = GameObject.Find("10orLess").GetComponent<Text>();
            tooLong.color = Color.red;
        }
    }
}
