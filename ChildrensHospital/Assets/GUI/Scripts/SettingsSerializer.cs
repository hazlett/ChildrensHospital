using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

public class SettingsSerializer
{

    private string logPath = "Settings.xml";


    private static SettingsSerializer instance = new SettingsSerializer();

    private SettingsSerializer()
    {


    }
    public static SettingsSerializer Instance
    {
        get { return instance; }
    }

    public void WriteSettings(XmlSettings settings)
    {
        XmlSerializer xml = new XmlSerializer(typeof(XmlSettings));
        using (Stream stream = File.Create(logPath))
        {
            xml.Serialize(stream, settings);
        }
    }
    public XmlSettings ReadSettings()
    {
        try
        {
            using (var file = File.Open(logPath, FileMode.Open))
            {
                XmlSerializer xml = new XmlSerializer(typeof(XmlSettings));
                return (XmlSettings)xml.Deserialize(file);
            }
        }
        catch (Exception e)
        {
            XmlSettings newSettings = new XmlSettings();
            return newSettings;
        }
    }
}
