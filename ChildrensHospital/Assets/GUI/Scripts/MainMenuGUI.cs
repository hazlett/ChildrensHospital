using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class MainMenuGUI : MonoBehaviour
{

    public GUISkin mainMenuSkin;
    public SettingsGUI settings;

    private string playerStatus, timeString, errorMessage;
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private bool manualCalibration = false, invalidInput = false;

    void Start()
    {
        errorMessage = "Invalid trial length. \nPlease enter a trial length between 0 and 120.";
        timeString = "30";
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }

    void Update()
    {

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

        if ((UserContainer.Instance.UserDictionary.ContainsKey(settings.user.ID)) && (GameControl.Instance.IsCalibrated))
        {
            if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 - 175, 300, 100), "Start Trial"))
            {
                if (manualCalibration)
                {
                    Debug.Log("Manual Transform Matrix: " + GameControl.Instance.ReadCalibration(Application.dataPath + @"/../ManualCalibration/" + "ChessBoardWCS.exe"));
                }
                else
                {
                    Debug.Log("Auto Transform Matrix: " + GameControl.Instance.ReadCalibration());
                }
                CheckTime();
                if (!invalidInput)
                {
                    Application.LoadLevel("Game");
                }
            }
        }
        else if ((UserContainer.Instance.UserDictionary.ContainsKey(settings.user.ID)) && (!GameControl.Instance.IsCalibrated))
        {
            GUI.Label(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 - 175, 300, 100), "Calibrating", "GreyStart");
        }
        else
        {
            GUI.Label(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 - 175, 300, 100), "Choose User", "GreyStart");
        }
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 - 70, 300, 100), "New User"))
        {
            settings.newUser = true;
            settings.enabled = true;
            this.enabled = false;
        }
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 + 35, 300, 100), "Existing User"))
        {
            settings.newUser = false;
            settings.enabled = true;
            this.enabled = false;
        }

        GUI.Label(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 + 150, 300, 50), "Trial Length (sec)");
        timeString = GUI.TextField(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 + 185, 300, 50), timeString);

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 270, nativeVerticalResolution - 300, 540, 270), "Current User\n" + playerStatus);

        // Error box
        if (invalidInput)
        {
            GUI.Box(new Rect(scaledResolutionWidth / 2 - 380, 15, 760, 100), errorMessage);
        }

        timeString = Regex.Replace(timeString, @"[^0-9]", "");
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
        playerStatus = settings.user.ToString();
    }

    private void CheckTime()
    {
        try
        {
            UserContainer.Instance.time = int.Parse(timeString);

            if (UserContainer.Instance.time < 0 || UserContainer.Instance.time > 120)
            {
                invalidInput = true;
            }
        }
        catch (Exception)
        {
            invalidInput = true;
        }
    }
}
