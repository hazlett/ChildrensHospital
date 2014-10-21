using UnityEngine;
using System.Collections;

public class ManualCameraControl : MonoBehaviour {
    public Camera mainCamera, shoulderCamera;
    public FocusedCameraControl control;
    private int index;
	void Start () {
        index = 0;
	}
	
	void Update () {
	    if (Input.GetKeyUp(KeyCode.C))
        {
            control.gameManager.message = Languages.Instance.GetTranslation("MANUAL CONTROL");
            control.enabled = false;
            shoulderCamera.enabled = !shoulderCamera.enabled;
            shoulderCamera.gameObject.GetComponent<DebugCamera>().enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            control.enabled = !control.enabled;
            if (control.enabled)
            {
                control.gameManager.message = Languages.Instance.GetTranslation("AUTOMATING CAMERA");
            }
            shoulderCamera.gameObject.GetComponent<DebugCamera>().enabled = !control.enabled;
            control.automating = true;
        }
	}
}
