using UnityEngine;
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

    [XmlAttribute("Diagnosis")]
    public string Diagnosis;

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
            Name = UserContainer.Instance.UserDictionary[id].Name;
            ID = id;
            Birthdate = UserContainer.Instance.UserDictionary[id].Birthdate;
            BrookeScale = UserContainer.Instance.UserDictionary[id].BrookeScale;
            UlnaLength = UserContainer.Instance.UserDictionary[id].UlnaLength;
            Diagnosis = UserContainer.Instance.UserDictionary[id].Diagnosis;
            Gender = UserContainer.Instance.UserDictionary[id].Gender;

            if (brookeScale != 0)
            {
                BrookeScale = UserContainer.Instance.UserDictionary[id].BrookeScale = brookeScale;
            }
            if (ulnaLength != 0)
            {
                UlnaLength = UserContainer.Instance.UserDictionary[id].UlnaLength = ulnaLength;
            }

            UserContainer.Instance.currentUser = ID;
        }
    }

    public User() { }
    public User(string name, int id, DateTime birthdate, int brookeScale, float ulnaLength, string diagnosis, bool gender)
    {
        numberOfUsers = UserContainer.Instance.UserDictionary.Count;
        Name = name;
        ID = id;
        Birthdate = birthdate;
        BrookeScale = brookeScale;
        UlnaLength = ulnaLength;
        Diagnosis = diagnosis;
        Gender = gender;
    }

    public override string ToString()
    {
        string userGender;
        if (Gender)
        {
            userGender = Languages.Instance.GetTranslation("Male");
        }
        else
        {
            userGender = Languages.Instance.GetTranslation("Female");
        }

        return (Languages.Instance.GetTranslation("Name") + ": " + Name + " \n" + Languages.Instance.GetTranslation("ID") + ": " + ID + " \n" + Languages.Instance.GetTranslation("Birthdate") + ": " + Birthdate.Month.ToString() + '/' + Birthdate.Day.ToString()
            + '/' + Birthdate.Year.ToString() + " \n" + Languages.Instance.GetTranslation("Brooke Scale") + ": " + BrookeScale + " \n" + Languages.Instance.GetTranslation("Ulna Length") + ": " + UlnaLength + "\n" + Languages.Instance.GetTranslation("Diagnosis") + ": "
            + Diagnosis + "\n" + Languages.Instance.GetTranslation("Gender") + ": " + userGender);
    }
}
