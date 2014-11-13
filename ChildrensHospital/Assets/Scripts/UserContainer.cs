using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("UserCollection")]
public class UserContainer {

    private static UserContainer instance = new UserContainer();
    public static UserContainer Instance
    {
        get
        {
            return instance;
        }
    }

    [XmlIgnore]
    public Dictionary<string, User> UserDictionary = new Dictionary<string, User>();

    [XmlIgnore]
    public int time;
    [XmlIgnore]
    public string currentUser;

    [XmlArray("Users"), XmlArrayItem("User")]
    public List<User> Users = new List<User>();

    public void Save(string path)
    {
        Debug.Log("Saving");
        XmlSerializer serializer = new XmlSerializer(typeof(UserContainer));
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public void Load(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(UserContainer));
        if (File.Exists(path))
        {
            Debug.Log(path);
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                instance = serializer.Deserialize(stream) as UserContainer;
            }
        }
    }

    public void PopulateDictionary()
    {
        Debug.Log("Populating Dictionary");
        for (int i = 0; i < Users.Count; i++)
        {
            Debug.Log(Users[i].ID);
            UserDictionary.Add(Users[i].ID, Users[i]);
        }
        SortList();
    }

    private void SortList()
    {
        List<string> tempList = new List<string>();
        for (int i = 0; i < Users.Count; i++)
        {
            for (int j = i + 1; j < Users.Count; j++)
            {
                if (string.CompareOrdinal(Users[i].ID, Users[j].ID) > 0)
                {
                    User temp = Users[j];
                    Users.Remove(Users[j]);
                    Users.Insert(i, temp);
                }
            }
        }
    }
}
