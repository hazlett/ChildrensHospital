using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GUISkin mainMenuSkin;

    private float timer, nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private string message;
    private float lowerLeftVolume, lowerRightVolume, middleLeftVolume, middleRightVolume, upperLeftVolume, upperRightVolume, yRightValue, yLeftValue;
    public MonoBehaviour rightHand, leftHand;
	void Start () {
        timer = 0.0f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
	}
	
	// Update is called once per frame
	void Update () {
        if (KinectManager.Instance.GetUsersCount() > 0)
        {
            timer += Time.deltaTime;
            if (timer > 1.5)
            {
                message = "PLAYING GAME";
                StartGame();
            }
            else
            {
                message = "COUNTING DOWN";
            }
        }
        else
        {
            message = "SKELETON NOT FOUND";
            timer = 0.0f;
            return;
        }
        CalculateVolumes();
        TimedScreenResize();
	}
    void OnGUI()
    {

        if (mainMenuSkin)
        {
            GUI.skin = mainMenuSkin;
        }
        else
        {
            Debug.Log("MainMenuGUI: GUI Skin object missing!");
        }

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 200, 10, 400, 75), message);
        GUI.Box(new Rect(10, 10, 400, 75), "Lower Left Volume: " + lowerLeftVolume);
        GUI.Box(new Rect(10, 85, 400, 75), "Middle Left Volume: " + middleLeftVolume);
        GUI.Box(new Rect(10, 160, 400, 75), "Upper Left Volume: " + upperLeftVolume);
        GUI.Box(new Rect(10, 235, 400, 75), "Left Y Value: " + yLeftValue);
        GUI.Box(new Rect(scaledResolutionWidth - 410, 10, 400, 75), "Lower Right Volume: " + lowerRightVolume);
        GUI.Box(new Rect(scaledResolutionWidth - 410, 85, 400, 75), "Middle Right Volume: " + middleRightVolume);
        GUI.Box(new Rect(scaledResolutionWidth - 410, 160, 400, 75), "Upper Right Volume: " + upperRightVolume);
        GUI.Box(new Rect(scaledResolutionWidth - 410, 235, 400, 75), "Right Y Value: " + yRightValue);
    }
    private void StartGame()
    {
        rightHand.enabled = true;
        leftHand.enabled = true;
    }
    private void CalculateVolumes()
    {
        lowerRightVolume = ((RightHandBehaviour)rightHand).LowerVolume;
        middleRightVolume = ((RightHandBehaviour)rightHand).MiddleVolume;
        upperRightVolume = ((RightHandBehaviour)rightHand).UpperVolume;
        lowerLeftVolume = Mathf.Abs(((LeftHandBehaviour)leftHand).LowerVolume);
        middleLeftVolume = Mathf.Abs(((LeftHandBehaviour)leftHand).MiddleVolume);
        upperLeftVolume = Mathf.Abs(((LeftHandBehaviour)leftHand).UpperVolume);
        yLeftValue = ((LeftHandBehaviour)leftHand).TopVolume;
        yRightValue = ((RightHandBehaviour)rightHand).TopVolume;
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }
}
