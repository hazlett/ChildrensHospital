using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;

public class User  {

    public string Name;

    public int ID;

    public DateTime Birthdate;

    public int BrooksScale;

    public float UlnaLength;

    public List<Volumes> Volume;
}
