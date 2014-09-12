using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public Camera mainCamera, shoulderCamera;
    private float timer;
    public GameManager gameManager;
    internal bool automating;
    private float relativeRightVolume, relativeLeftVolume;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
    void OnEnable()
    {
        timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameManager.Playing)
        {
            timer += Time.deltaTime;
            if (automating)
            {
                if (timer > gameManager.EndTrial * 0.1f)
                {
                    timer = 0.0f;
                    relativeRightVolume = ((RightHandBehaviour)gameManager.rightHand).RelativeLowerVolume +
                        ((RightHandBehaviour)gameManager.rightHand).RelativeMiddleVolume +
                        ((RightHandBehaviour)gameManager.rightHand).RelativeUpperVolume;
                    relativeLeftVolume = ((LeftHandBehaviour)gameManager.leftHand).RelativeLowerVolume +
                        ((LeftHandBehaviour)gameManager.leftHand).RelativeMiddleVolume +
                        ((LeftHandBehaviour)gameManager.leftHand).RelativeUpperVolume;
                    if (relativeRightVolume == 0)
                    {
                        shoulderCamera.enabled = true;
                        gameManager.message = "FOCUS ON YOUR RIGHT HAND";
                        shoulderCamera.gameObject.transform.localEulerAngles = new Vector3(0, 45.0f, 0);
                    }
                    else if (relativeLeftVolume == 0)
                    {
                        shoulderCamera.enabled = true;
                        gameManager.message = "FOCUS ON YOUR LEFT HAND";
                        shoulderCamera.gameObject.transform.localEulerAngles = new Vector3(0, -45.0f, 0);
                    }
                    else if (relativeLeftVolume < relativeRightVolume)
                    {
                        shoulderCamera.enabled = true;
                        gameManager.message = "FOCUS ON YOUR LEFT HAND";
                        shoulderCamera.gameObject.transform.localEulerAngles = new Vector3(0, -45.0f, 0);
                    }
                    else
                    {
                        shoulderCamera.enabled = true;
                        gameManager.message = "FOCUS ON YOUR RIGHT HAND";
                        shoulderCamera.gameObject.transform.localEulerAngles = new Vector3(0, 45.0f, 0);
                    }
                }
            }
            else
            {
                if (timer > gameManager.EndTrial * 0.25f)
                {
                    automating = true;
                    timer = 0;
                }
            }
        }
	}
}
