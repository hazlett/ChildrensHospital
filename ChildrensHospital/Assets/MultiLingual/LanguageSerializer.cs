using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot("LanguageConverter")]
public class LanguageSerializer {

    private static LanguageSerializer instance = new LanguageSerializer();
    public static LanguageSerializer Instance { get { return instance; } }

    [XmlArray("Languages"), XmlArrayItem("Language")]
    public List<string> languages;

    [XmlArray("LanguageKeys"), XmlArrayItem("Key")]
    public List<TranslatorList> translatorList;


    public void Load(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(LanguageSerializer));
        if (File.Exists(path))
        {
            Debug.Log(path);
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                instance = serializer.Deserialize(stream) as LanguageSerializer;
            }
        }
    }
    public void Save()
    {
        string path = Path.Combine(Application.persistentDataPath, "languagesTest.xml");
        Debug.Log("Saving");
        XmlSerializer serializer = new XmlSerializer(typeof(LanguageSerializer));
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }
}
