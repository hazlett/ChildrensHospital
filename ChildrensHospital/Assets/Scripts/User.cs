﻿using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;

public class User  {

    [XmlAttribute("Name")]
    public string Name;

    [XmlAttribute("ID")]
    public int ID = -1;

    [XmlAttribute("Birthdate")]
    public DateTime Birthdate;

    [XmlAttribute("BrookeScale")]
    public int BrookeScale;

    [XmlAttribute("UlnaLength")]
    public float UlnaLength;

    [XmlAttribute("Gender")]
    public bool Gender;

    [XmlIgnore]
    internal int numberOfUsers;

    public void SaveUser()
    {
        UserContainer.Instance.Users.Add(this);
        UserContainer.Instance.UserDictionary.Add(ID, this);
        UserContainer.Instance.currentUser = ID;
        UserContainer.Instance.Save(Path.Combine(Application.persistentDataPath, "users.xml"));
    }

    public void LoadUsers()
    {
        UserContainer.Instance.Load(Path.Combine(Application.persistentDataPath, "users.xml"));
        UserContainer.Instance.PopulateDictionary();
        numberOfUsers = UserContainer.Instance.Users.Count;
    }

    public void LoadSpecificUser(int id, int brookeScale, float ulnaLength)
    {

        if (UserContainer.Instance.UserDictionary.ContainsKey(id))
        {
            Name = UserContainer.Instance.Users[id].Name;
            ID = id;
            Birthdate = UserContainer.Instance.Users[id].Birthdate;
            BrookeScale = UserContainer.Instance.Users[id].BrookeScale;
            UlnaLength = UserContainer.Instance.Users[id].UlnaLength;
            Gender = UserContainer.Instance.Users[id].Gender;

            if (brookeScale != 0)
            {
                BrookeScale = UserContainer.Instance.Users[id].BrookeScale = brookeScale;
            }
            if (ulnaLength != 0)
            {
                UlnaLength = UserContainer.Instance.Users[id].UlnaLength = ulnaLength;
            }

            UserContainer.Instance.currentUser = ID;
        }
    }

    public User() { }
    public User(string name, int id, DateTime birthdate, int brookeScale, float ulnaLength, bool gender)
    {
        numberOfUsers = UserContainer.Instance.UserDictionary.Count;
        Name = name;
        ID = id;
        Birthdate = birthdate;
        BrookeScale = brookeScale;
        UlnaLength = ulnaLength;
        Gender = gender;
    }

    public override string ToString()
    {
        string userGender;
        if (Gender)
        {
            userGender = "Male";
        }
        else
        {
            userGender = "Female";
        }

        return ("Name: " + Name + " \nID: " + ID + " \nBirthdate: " + Birthdate.Month.ToString() + '/' + Birthdate.Day.ToString()
            + '/' + Birthdate.Year.ToString() + " \nBrooke Scale: " + BrookeScale + " \nUlna Length: " + UlnaLength
            + "\nGender: " + userGender + " Total Users: " + numberOfUsers);
    }
}
