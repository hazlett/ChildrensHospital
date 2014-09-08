﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

    private static GameControl instance = new GameControl();
    public static GameControl Instance
    {
        get
        {
            return instance;
        }
    }

    internal struct userInformation
    {
        public string Name;
        public DateTime Birthdate;
        public int brookeScale;
        public float UlnaLength;
    };

    internal userInformation user;
    internal IDictionary<int, userInformation> playerData = new Dictionary<int, userInformation>();

    private int idKey;

	void Awake () {

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        LoadUsers();
	}

    void OnDestroy()
    {
        Save();
    }

    public string Print()
    {
        return ("Name: " + user.Name + " \nID: " + idKey + " \nBirthdate: " + user.Birthdate.Month.ToString() + '/' + user.Birthdate.Day.ToString()
            + '/' + user.Birthdate.Year.ToString() + " \nBrooke Scale: " + user.brookeScale + " \nUlna Length: " + user.UlnaLength);
    }

    public void AddUser(int ID)
    {
        playerData.Add(ID, user);
        Debug.Log(playerData.ContainsKey(ID).ToString());
        idKey = ID;
    }

    public void UpdateUser(int ID)
    {
        if (playerData.ContainsKey(ID))
        {
            playerData.Remove(ID);
            playerData.Add(ID, user);
        }
    }

    // Saves the dictionary values into a file
    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/userInfo.dat");

        string dataString = (playerData.Count).ToString();

        // For each pair of values, parse them into a string in order to save them.
        foreach(KeyValuePair<int,userInformation> pair in playerData)
        {
            dataString += ("<KEY>" + pair.Key.ToString() + "<NAME>" + pair.Value.Name + "<BIRTHDATE>"
                + pair.Value.Birthdate.ToString() + "<SCALE>" + pair.Value.brookeScale.ToString() + "<ULNA>" + pair.Value.UlnaLength.ToString() + "<END>\n");
        }

        PlayerData data = new PlayerData();
        data.playerData = dataString;

        formatter.Serialize(file, data);
        file.Close();
    }

    // Loads a specific user, and updates the values if necessary
    public void LoadUser(int ID, int brookeScale, float UlnaLength)
    {
        idKey = ID;
        if(playerData.ContainsKey(ID)){
            user.Name = playerData[ID].Name;
            user.Birthdate = playerData[ID].Birthdate;

            // If no changes have been made, keep the past Brook's Scale and Ulna Length values
            if (brookeScale == 0)
            {
                user.brookeScale = playerData[ID].brookeScale;
            }
            else
            {
                user.brookeScale = brookeScale;
            }
            if (UlnaLength == 0)
            {
                user.UlnaLength = playerData[ID].UlnaLength;
            }
            else
            {
                user.UlnaLength = UlnaLength;
            }
            UpdateUser(ID);
        }
    }

    // Loads all users and stores them in the dictionary
    public void LoadUsers()
    {
        if (File.Exists(Application.persistentDataPath + "/userInfo.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/userInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)formatter.Deserialize(file);
            file.Close();

            string dataString = data.playerData;

            // Find the number of saved users
            int userCount = int.Parse(dataString[0].ToString());

            // For each saved user, parse the string into the user data
            for (int i = 0; i < userCount; i++)
            {
                int index1 = dataString.IndexOf("<KEY>");
                int index2 = dataString.IndexOf("<NAME>");
             
                idKey = int.Parse(dataString.Substring(index1 + 5, (index2 - index1 - 5)));

                index1 = dataString.IndexOf("<BIRTHDATE>");
       
                user.Name = dataString.Substring(index2 + 6, (index1 - index2 - 6));

                index2 = dataString.IndexOf("<SCALE>");

                user.Birthdate = DateTime.Parse(dataString.Substring(index1 + 11, (index2 - index1 - 11)));

                index1 = dataString.IndexOf("<ULNA>");

                user.brookeScale = int.Parse(dataString.Substring(index2 + 7, (index1 - index2 - 7)));

                index2 = dataString.IndexOf("<END>");

                user.UlnaLength = float.Parse(dataString.Substring(index1 + 6, (index2 - index1 - 6)));

                AddUser(idKey);
                dataString = dataString.Substring(index2 + 5);
            }
        }
    }
}

[Serializable]
class PlayerData
{
     public string playerData;
}