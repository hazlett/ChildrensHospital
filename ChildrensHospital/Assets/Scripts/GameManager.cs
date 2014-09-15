using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public GUISkin mainMenuSkin;
    public EndGUI endStats;
    public GemGenerator generator;
    public SpiderSpawner spiders;
    private float endTrial = 20.0f;
    public float EndTrial {get {return endTrial;} }
    internal float timer;
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    internal string message;
    private Volumes volumes;
    public Volumes Volumes { get { return volumes; } }
    private float lowerLeftVolume, lowerRightVolume, middleLeftVolume, middleRightVolume, upperLeftVolume, upperRightVolume, yRightValue, yLeftValue;
    private bool GUIon;
    public bool DebugMode;
    internal MonoBehaviour rightHand, leftHand;
    private GameObject character;
    private bool playing;
    public bool Playing { get { return playing; } }

	void Start () {

        volumes = new Volumes();
        timer = 0.0f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        GUIon = true;
        if (UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Gender)
        {
            character = (GameObject)Instantiate(Resources.Load<GameObject>("Male"));
        }
        else
        {
            character = (GameObject)Instantiate(Resources.Load<GameObject>("Female"));
        }
        rightHand = GameObject.Find("RightHand").GetComponent<RightHandBehaviour>();
        leftHand = GameObject.Find("LeftHand").GetComponent<LeftHandBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        if (KinectManager.Instance.GetUsersCount() > 0)
        {
            timer += Time.deltaTime;
            if (timer > 1.5)
            {
                if (!playing)
                {
                    message = "PLAYING GAME";
                    StartGame();
                }
            }
            else
            {
                message = "SETTING UP GAME";
                if (UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Gender)
                {
                    spiders.enabled = true;
                }
                else
                {
                    generator.enabled = true;
                }
            }
            if (timer > endTrial)
            {
                if (!DebugMode)
                { 
                    GUIon = false;
                    endStats.enabled = true;
                }
            }
        }
        else
        {
            message = "SKELETON NOT FOUND";
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

        if (GUIon)
        {
            GUI.Label(new Rect(scaledResolutionWidth / 2 - 200, 10, 400, 75), message);
            GUI.Label(new Rect(scaledResolutionWidth / 2 - 200, 85, 400, 75), "Score: " );

            GUI.Label(new Rect(10, 10, 400, 75), "Lower Left Volume: " + lowerLeftVolume);
            GUI.Label(new Rect(10, 85, 400, 75), "Middle Left Volume: " + middleLeftVolume);
            GUI.Label(new Rect(10, 160, 400, 75), "Upper Left Volume: " + upperLeftVolume);
            GUI.Label(new Rect(10, 235, 400, 75), "Left Y Value: " + yLeftValue);
            GUI.Label(new Rect(scaledResolutionWidth - 410, 10, 400, 75), "Lower Right Volume: " + lowerRightVolume);
            GUI.Label(new Rect(scaledResolutionWidth - 410, 85, 400, 75), "Middle Right Volume: " + middleRightVolume);
            GUI.Label(new Rect(scaledResolutionWidth - 410, 160, 400, 75), "Upper Right Volume: " + upperRightVolume);
            GUI.Label(new Rect(scaledResolutionWidth - 410, 235, 400, 75), "Right Y Value: " + yRightValue);
        }
    }
    private void StartGame()
    {
        playing = true;
        rightHand.enabled = true;
        leftHand.enabled = true;
    }
    private void CalculateVolumes()
    {
        lowerRightVolume = (float)((RightHandBehaviour)rightHand).LowerVolume;
        middleRightVolume = (float)((RightHandBehaviour)rightHand).MiddleVolume;
        upperRightVolume = (float)((RightHandBehaviour)rightHand).UpperVolume;
        lowerLeftVolume = (float)Mathf.Abs(((LeftHandBehaviour)leftHand).LowerVolume);
        middleLeftVolume = (float)Mathf.Abs(((LeftHandBehaviour)leftHand).MiddleVolume);
        upperLeftVolume = (float)Mathf.Abs(((LeftHandBehaviour)leftHand).UpperVolume);
        yLeftValue = (float)((LeftHandBehaviour)leftHand).TopVolume;
        yRightValue = (float)((RightHandBehaviour)rightHand).TopVolume;

        volumes.SetVolumes(lowerLeftVolume, lowerRightVolume, middleLeftVolume, middleRightVolume, upperLeftVolume, upperRightVolume, yRightValue, yLeftValue);

        lowerRightVolume = (float)(Math.Truncate(((RightHandBehaviour)rightHand).LowerVolume * 100f) / 100f);
        middleRightVolume = (float)(Math.Truncate(((RightHandBehaviour)rightHand).MiddleVolume * 100f) / 100f);
        upperRightVolume = (float)(Math.Truncate(((RightHandBehaviour)rightHand).UpperVolume * 100f) / 100f);
        lowerLeftVolume = (float)(Math.Truncate(Mathf.Abs(((LeftHandBehaviour)leftHand).LowerVolume) * 100f) / 100f);
        middleLeftVolume = (float)(Math.Truncate(Mathf.Abs(((LeftHandBehaviour)leftHand).MiddleVolume) * 100f) / 100f);
        upperLeftVolume = (float)(Math.Truncate(Mathf.Abs(((LeftHandBehaviour)leftHand).UpperVolume) * 100f) / 100f);
        yLeftValue = (float)(Math.Truncate(((LeftHandBehaviour)leftHand).TopVolume * 100f) / 100f);
        yRightValue = (float)(Math.Truncate(((RightHandBehaviour)rightHand).TopVolume * 100f) / 100f);
    }
    internal float TotalVolume()
    {
        return (((RightHandBehaviour)rightHand).LowerVolume + ((RightHandBehaviour)rightHand).MiddleVolume + ((RightHandBehaviour)rightHand).UpperVolume
            + Mathf.Abs(((LeftHandBehaviour)leftHand).LowerVolume) + Mathf.Abs(((LeftHandBehaviour)leftHand).MiddleVolume) + Mathf.Abs(((LeftHandBehaviour)leftHand).UpperVolume)
            + ((LeftHandBehaviour)leftHand).TopVolume + ((RightHandBehaviour)rightHand).TopVolume);
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }
}
