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
    private VolumeTracker tracker;
    public VolumeTracker Tracker { get { return tracker; } }
    public bool Playing { get { return playing; } }

	void Start () {
        tracker = new VolumeTracker();
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
                else
                {
                    tracker.Update();
                    volumes = tracker.GetVolumes();
                }
            }
            else
            {
                message = "SETTING UP GAME";
                
                // Spawn Gems
                generator.enabled = true;
                
            }
            if (timer > UserContainer.Instance.time)
            {
                if (!DebugMode)
                { 
                    GUIon = false;
                    endStats.enabled = true;
                    playing = false;
                }
            }
        }
        else
        {
            message = "SKELETON NOT FOUND";
            return;
        }

        

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
            GUI.Label(new Rect(scaledResolutionWidth / 2 - 200, 85, 400, 75), "Score: " + TotalVolume().ToString("F3"));

            //ShowAvatarVolumes();
            ShowVolumes();
        }
    }

    private void ShowAvatarVolumes()
    {
        GUI.Label(new Rect(10, 10, 400, 75), "Lower Left Volume: " + lowerLeftVolume.ToString("F3"));
        GUI.Label(new Rect(10, 85, 400, 75), "Middle Left Volume: " + middleLeftVolume.ToString("F3"));
        GUI.Label(new Rect(10, 160, 400, 75), "Upper Left Volume: " + upperLeftVolume.ToString("F3"));
        GUI.Label(new Rect(10, 235, 400, 75), "Left Y Value: " + yLeftValue.ToString("F3"));
        GUI.Label(new Rect(scaledResolutionWidth - 410, 10, 400, 75), "Lower Right Volume: " + lowerRightVolume.ToString("F3"));
        GUI.Label(new Rect(scaledResolutionWidth - 410, 85, 400, 75), "Middle Right Volume: " + middleRightVolume.ToString("F3"));
        GUI.Label(new Rect(scaledResolutionWidth - 410, 160, 400, 75), "Upper Right Volume: " + upperRightVolume.ToString("F3"));
        GUI.Label(new Rect(scaledResolutionWidth - 410, 235, 400, 75), "Right Y Value: " + yRightValue.ToString("F3"));
    }
    private void ShowVolumes()
    {
        GUI.Label(new Rect(10, 10, 400, 75), "Lower Left Volume: " + tracker.LowerLeftVolume().ToString("F3"));
        GUI.Label(new Rect(10, 85, 400, 75), "Middle Left Volume: " + tracker.MiddleLeftVolume().ToString("F3"));
        GUI.Label(new Rect(10, 160, 400, 75), "Upper Left Volume: " + tracker.UpperLeftVolume().ToString("F3"));
        GUI.Label(new Rect(10, 235, 400, 75), "Left Y Value: " + tracker.YLeft.ToString("F3"));
        GUI.Label(new Rect(scaledResolutionWidth - 410, 10, 400, 75), "Lower Right Volume: " + tracker.LowerRightVolume().ToString("F3"));
        GUI.Label(new Rect(scaledResolutionWidth - 410, 85, 400, 75), "Middle Right Volume: " + tracker.MiddleRightVolume().ToString("F3"));
        GUI.Label(new Rect(scaledResolutionWidth - 410, 160, 400, 75), "Upper Right Volume: " + tracker.UpperRightVolume().ToString("F3"));
        GUI.Label(new Rect(scaledResolutionWidth - 410, 235, 400, 75), "Right Y Value: " + tracker.YRight.ToString("F3"));
    }
    private void StartGame()
    {
        playing = true;
        rightHand.enabled = true;
        leftHand.enabled = true;
        
    }
    private void CalculateAvatarVolumes()
    {
        if (playing)
        {
            // Populate volume array
            lowerRightVolume = (float)((RightHandBehaviour)rightHand).LowerVolume;
            middleRightVolume = (float)((RightHandBehaviour)rightHand).MiddleVolume;
            upperRightVolume = (float)((RightHandBehaviour)rightHand).UpperVolume;
            lowerLeftVolume = (float)Mathf.Abs(((LeftHandBehaviour)leftHand).LowerVolume);
            middleLeftVolume = (float)Mathf.Abs(((LeftHandBehaviour)leftHand).MiddleVolume);
            upperLeftVolume = (float)Mathf.Abs(((LeftHandBehaviour)leftHand).UpperVolume);
            yLeftValue = (float)((LeftHandBehaviour)leftHand).TopVolume;
            yRightValue = (float)((RightHandBehaviour)rightHand).TopVolume;

            volumes.SetVolumes(lowerLeftVolume, lowerRightVolume, middleLeftVolume, middleRightVolume, upperLeftVolume, upperRightVolume, yRightValue, yLeftValue);}
    }

    internal float TotalAvatarVolume()
    {
        return lowerRightVolume + middleRightVolume + upperRightVolume + yRightValue +
            lowerLeftVolume + middleLeftVolume + upperLeftVolume + yLeftValue;
    }
    internal float TotalVolume()
    {
        return (tracker.LowerLeftVolume() + tracker.LowerRightVolume() 
            + tracker.MiddleLeftVolume() + tracker.MiddleRightVolume() 
            + tracker.UpperLeftVolume() + tracker.UpperRightVolume());
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }
}
