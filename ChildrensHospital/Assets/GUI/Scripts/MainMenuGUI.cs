using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

    public GUISkin mainMenuSkin;
    public SettingsGUI settings;

    private string playerStatus;
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;

	// Use this for initialization
	void Start () {

        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
	}
	
	// Update is called once per frame
	void Update () {

        TimedScreenResize();
        SetPlayerStatus();
	}

    void OnEnable()
    {
        SetPlayerStatus();
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

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 - 145, 300, 100), "Start Trial"))
        {
        }
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 - 35, 300, 100), "New User"))
        {
            settings.newUser = true;
            settings.enabled = true;
            this.enabled = false;
        }
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 + 75, 300, 100), "Existing User"))
        {
            settings.newUser = false;
            settings.enabled = true;
            this.enabled = false;
        }

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 270, nativeVerticalResolution - 300, 540, 270), playerStatus);
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }

    private void SetPlayerStatus()
    {
        playerStatus = GameControl.Instance.Print();
    }
}
