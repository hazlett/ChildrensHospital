using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class PauseGUI : MonoBehaviour
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

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 270, nativeVerticalResolution / 2 - 400, 540, 540), Languages.Instance.GetTranslation("Game Paused") + "\n" + Languages.Instance.GetTranslation("Name") + ": " + UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Name
            + "\n" + Languages.Instance.GetTranslation("Volume") + ": " + gameManager.TotalVolume().ToString("F4") + " " + Languages.Instance.GetTranslation("meters cubed"), "EndBox");

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 315, nativeVerticalResolution - 380, 325, 100), Languages.Instance.GetTranslation("Restart Trial")))
        {
            if (saveData)
            {
                gameManager.Tracker.DebugExtremas();
            }
            Time.timeScale = 1;
            Application.LoadLevel("Game");
        }
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 10, nativeVerticalResolution - 380, 325, 100), Languages.Instance.GetTranslation("Main Menu")))
        {
            if (saveData)
            {
                gameManager.Tracker.DebugExtremas();
            }
            Time.timeScale = 1;
            Application.LoadLevel("MainMenu");
        }

        saveData = GUI.Toggle(new Rect(scaledResolutionWidth / 2 - 200, nativeVerticalResolution - 250, 50, 50), saveData, "");

        GUI.Label(new Rect(scaledResolutionWidth / 2, nativeVerticalResolution - 250, 200, 50), Languages.Instance.GetTranslation("Save Data"), "ToggleLabel");
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }
}
