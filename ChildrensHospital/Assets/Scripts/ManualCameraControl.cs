using UnityEngine;
using System.Collections;

public class ManualCameraControl : MonoBehaviour {
    public Camera[] cameras;
    private int index;
	void Start () {
        index = 0;
	}
	
	void Update () {
	    if (Input.GetKeyUp(KeyCode.C))
        {
            Debug.Log("C Pressed");
            index++;
            if (index < cameras.Length)
            {
                if (index != 1)
                {
                    Debug.Log("index != 1");
                    cameras[index - 1].enabled = false;
                    cameras[index].enabled = true;
                }
                else
                {
                    Debug.Log("index == 1");
                    cameras[index].enabled = true;
                }
            }
            else
            {
                Debug.Log("else");
                cameras[index - 1].enabled = false;
                index = 0;
            }
            Debug.Log("index = " + index);
        }
	}
}
