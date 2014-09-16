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
    public Dictionary<int, User> UserDictionary = new Dictionary<int, User>();

    [XmlIgnore]
    public int currentUser;

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
    }
}
