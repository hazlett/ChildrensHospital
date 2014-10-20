﻿using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System;
[XmlRoot("CurrentLanguage")]
public class LanguageUsed {

    private static LanguageUsed instance = new LanguageUsed();
    public static LanguageUsed Instance { get { return instance; } }
    [XmlIgnore]
    private string path;

    [XmlElement("Current")]
    public int CurrentLanguage;

    public void Load(string path)
    {
        this.path = path;
        XmlSerializer serializer = new XmlSerializer(typeof(LanguageUsed));
        if (File.Exists(path))
        {
            Debug.Log(path);
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                instance = serializer.Deserialize(stream) as LanguageUsed;
            }
        }
    }
    public void Save()
    {
        try
        {
            Debug.Log("Saving");
            XmlSerializer serializer = new XmlSerializer(typeof(LanguageUsed));
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving current language: " + e.Message);
        }
    }
}