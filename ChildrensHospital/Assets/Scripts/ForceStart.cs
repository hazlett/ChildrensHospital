﻿using UnityEngine;
using System.Collections;

public class ForceStart : MonoBehaviour {
	// Use this for initialization
	void Start () { 
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp(KeyCode.K))
        {
            GameControl.Instance.ReadCalibration();
        }
	}
}
