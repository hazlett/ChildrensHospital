using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class TranslatorList {
    [XmlAttribute("tag")]
    public string key;
    [XmlArray("Translations"), XmlArrayItem("Translation")]
    public List<string> translations;

}
