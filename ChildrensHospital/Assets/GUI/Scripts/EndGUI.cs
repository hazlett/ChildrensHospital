using UnityEngine;
using System.Collections;

public class EndGUI : MonoBehaviour
{
    public GUISkin mainMenuSkin;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private bool saveData = false;

    // Use this for initialization
    void Start()
    {
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
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

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 270, nativeVerticalResolution / 2 - 270, 540, 540), "Results\nName: \nVolume: \nOther Stats");

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 315, nativeVerticalResolution - 250, 300, 100), "Run Trial Again"))
        {
            //Start the scene
        }
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 + 15, nativeVerticalResolution - 250, 300, 100), "Quit"))
        {
            //Main Menu
        }

        saveData = GUI.Toggle(new Rect(scaledResolutionWidth / 2 - 125, nativeVerticalResolution - 125, 50, 50), saveData, "");

        GUI.Label(new Rect(scaledResolutionWidth / 2 - 65, nativeVerticalResolution - 125, 200, 50), "Save Results");
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }
}
