using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("UserCollection")]
public class UserContainer {

    [XmlArray("Users"), XmlArrayItem("User")]
    public List<User> Users = new List<User>();

    public void Save(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(UserContainer));
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static UserContainer Load(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(UserContainer));
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            return serializer.Deserialize(stream) as UserContainer;
        }
    }
}
