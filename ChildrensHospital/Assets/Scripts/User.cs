using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;

public class User  {

    private static User instance = new User();
    public static User Instance
    {
        get
        {
            return instance;
        }
    }

    [XmlAttribute("name")]
    public string Name;

    [XmlAttribute("id")]
    public int ID;

    [XmlAttribute("birthdate")]
    public DateTime Birthdate;

    [XmlAttribute("brookeScale")]
    public int BrookeScale;

    [XmlAttribute("ulnaLength")]
    public float UlnaLength;

    [XmlAttribute("gender")]
    public bool Gender;

    [XmlAttribute("volume")]
    public List<Volumes> Volume;

    UserContainer userCollection = new UserContainer();

    internal int numberOfUsers;

    public void SaveUser()
    {
        userCollection.Users.Add(User.Instance);
        userCollection.Save(Path.Combine(Application.persistentDataPath, "users.xml"));
    }

    public void LoadUsers()
    {
        userCollection = UserContainer.Load(Path.Combine(Application.persistentDataPath, "users.xml"));
        numberOfUsers = userCollection.Users.Count;
    }

    public void LoadSpecificUser(int id)
    {
        Name = userCollection.Users[id].Name;
        ID = id;
        Birthdate = userCollection.Users[id].Birthdate;
        BrookeScale = userCollection.Users[id].BrookeScale;
        UlnaLength = userCollection.Users[id].UlnaLength;
        Gender = userCollection.Users[id].Gender;
        Volume = userCollection.Users[id].Volume;
    }

    public void AddUser(string name, int id, DateTime birthdate, int brookeScale, float ulnaLength, bool gender)
    {
        Name = name;
        ID = id;
        Birthdate = birthdate;
        BrookeScale = brookeScale;
        UlnaLength = ulnaLength;
        Gender = gender;
    }
}
