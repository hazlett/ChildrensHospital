using UnityEngine;
using System.Collections;
using System;

public class EndGUI : MonoBehaviour
{
    public GUISkin mainMenuSkin;
    public GameManager gameManager;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private bool saveData = true;

    // Use this for initialization
    void Start()
    {
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        TimedScreenResize();

        if (saveData)
        {
        }
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

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 350, nativeVerticalResolution / 2 - 445, 700, 850), "", "Window");

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 270, nativeVerticalResolution / 2 - 400, 540, 540), "Results\nName: " + GameControl.Instance.user.Name + "\nVolume: "
            + (Math.Truncate(gameManager.TotalVolume() * 100f) / 100f).ToString() + "\nOther Stats", "EndBox");

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 315, nativeVerticalResolution - 380, 300, 100), "Run Trial Again"))
        {
            Application.LoadLevel("GemGame");
        }
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 + 15, nativeVerticalResolution - 380, 300, 100), "Quit"))
        {
            Application.LoadLevel("MainMenu");
        }

        saveData = GUI.Toggle(new Rect(scaledResolutionWidth / 2 - 125, nativeVerticalResolution - 250, 50, 50), saveData, "");

        GUI.Label(new Rect(scaledResolutionWidth / 2 - 65, nativeVerticalResolution - 250, 200, 50), "Save Results", "ToggleLabel");
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }
}
