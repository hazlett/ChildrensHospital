using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[XmlRoot("Settings")]
public class XmlSettings
{
    [XmlAttribute]
    public string name;

    [XmlAttribute]
    public int id;

    [XmlAttribute]
    public string birthdate;

    [XmlAttribute]
    public float brooksScale;

    [XmlAttribute]
    public float ulnaLength;

    public XmlSettings(string Name, int ID, string Birthdate, float BrooksScale, float UlnaLength)
    {
        Name = name;
        ID = id;
        Birthdate = birthdate;
        BrooksScale = brooksScale;
        UlnaLength = ulnaLength;
    }

    public XmlSettings()
    {


    }

    public void SetXmlSettings(string Name, int ID, string Birthdate, float BrooksScale, float UlnaLength)
    {

        name = Name;
        id = ID;
        birthdate = Birthdate;
        brooksScale = BrooksScale;
        ulnaLength = UlnaLength;
    }

    public override string ToString()
    {
        return ("Name: " + name + " \nID: " + id + " \nBirthdate: " + birthdate + " \nBrook's Scale: " + brooksScale + " \nUlna Length: " + ulnaLength);
    }

}
