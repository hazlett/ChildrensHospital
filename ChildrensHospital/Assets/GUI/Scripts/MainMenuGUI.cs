using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class MainMenuGUI : MonoBehaviour
{

    public GUISkin mainMenuSkin;
    public SettingsGUI settings;
    public InstructionsGUI instructions;
    public DropdownLanguagesGUI dropdown;
    public KinectManager kinectManager;

    internal Calibration calibration;

    private string playerStatus, timeString, errorMessage, style;
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private bool manualCalibration = false, invalidInput = false, popUp = false, calibrationError = false;
    private Vector2 labelSize = new Vector2(600, 100), buttonSize = new Vector2(350, 100);

    void Start()
    {
        style = "activeDrop";
        errorMessage = Languages.Instance.GetTranslation("Invalid trial length.") + " \n" + Languages.Instance.GetTranslation("Please enter a trial length between 0 and 120.");
        UserContainer.Instance.time = 60;
        timeString = UserContainer.Instance.time.ToString();
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


        if (!popUp)
        {
            if ((UserContainer.Instance.UserDictionary.ContainsKey(settings.user.ID)) && (GameControl.Instance.IsCalibrated))
            {
                //kinectManager.displayColorMap = false;
                if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 175, nativeVerticalResolution / 2 - 275, 350, buttonSize.y), Languages.Instance.GetTranslation("Start Trial")))
                {
                    if (GameControl.Instance.ReadCalibration() != Matrix4x4.zero)
                    {
                        CheckTime();

                        if (!invalidInput)
                        {
                            kinectManager.displayColorMap = false;
                            EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Game Started"));
                            Application.LoadLevel("Game");
                        }
                    }
                    else
                    {
                        popUp = true;
                        calibrationError = true;
                    }
                }
            }
            else if ((UserContainer.Instance.UserDictionary.ContainsKey(settings.user.ID)) && (!GameControl.Instance.IsCalibrated))
            {
                string calibrateMessage;
                if (GameControl.Instance.IsCalibrating)
                {
                    calibrateMessage = Languages.Instance.GetTranslation("Calibrating");
                }
                else
                {
                    calibrateMessage = Languages.Instance.GetTranslation("Please Calibrate");
                }
                GUI.Label(new Rect(scaledResolutionWidth / 2 - labelSize.x / 2, nativeVerticalResolution / 2 - 275, labelSize.x, labelSize.y), calibrateMessage, "GreyStart");
            }
            else
            {
                GUI.Label(new Rect(scaledResolutionWidth / 2 - labelSize.x / 2, nativeVerticalResolution / 2 - 275, labelSize.x, labelSize.y), Languages.Instance.GetTranslation("Please Select a User"), "GreyStart");
            }

            if (GUI.Button(new Rect(scaledResolutionWidth / 2 - buttonSize.x / 2, nativeVerticalResolution / 2 - 170, buttonSize.x, buttonSize.y), Languages.Instance.GetTranslation("New User")))
            {
                GameControl.Instance.IsCalibrated = false;
                settings.newUser = true;
                settings.enabled = true;
                dropdown.yPosition = 0;
                dropdown.opened = dropdown.enabled = false;
                this.enabled = false;
            }

            if (GUI.Button(new Rect(scaledResolutionWidth / 2 - buttonSize.x / 2, nativeVerticalResolution / 2 - 65, buttonSize.x, buttonSize.y), Languages.Instance.GetTranslation("Existing User")))
            {
                GameControl.Instance.IsCalibrated = false;
                settings.newUser = false;
                settings.enabled = true;
                dropdown.yPosition = 0;
                dropdown.opened = dropdown.enabled = false;
                this.enabled = false;
            }

            if (UserContainer.Instance.UserDictionary.ContainsKey(settings.user.ID))
            {
                if (!GameControl.Instance.IsCalibrated)
                {
                    if (GUI.Button(new Rect(scaledResolutionWidth / 2 - buttonSize.x / 2, nativeVerticalResolution / 2 + 40, buttonSize.x, buttonSize.y), Languages.Instance.GetTranslation("Calibrate")))
                    {
                        popUp = true;
                        calibrationError = false;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(scaledResolutionWidth / 2 - buttonSize.x / 2, nativeVerticalResolution / 2 + 40, buttonSize.x, buttonSize.y), Languages.Instance.GetTranslation("Recalibrate")))
                    {
                        popUp = true;
                        calibrationError = false;
                    }
                }
            }

            if (GUI.Button(new Rect(25, nativeVerticalResolution - 250, buttonSize.x, buttonSize.y), Languages.Instance.GetTranslation("Instructions")))
            {
                instructions.enabled = true;
                dropdown.yPosition = 0;
                dropdown.opened = dropdown.enabled = false;
                this.enabled = false;
            }

            if (GUI.Button(new Rect(25, nativeVerticalResolution - 125, buttonSize.x, buttonSize.y), Languages.Instance.GetTranslation("Quit")))
            {
                Application.Quit();
            }

            GUI.Label(new Rect(scaledResolutionWidth / 2 - labelSize.x / 2, nativeVerticalResolution / 2 + 150, labelSize.x, 50), Languages.Instance.GetTranslation("Trial Length") + " (sec)");
            timeString = GUI.TextField(new Rect(scaledResolutionWidth / 2 - buttonSize.x / 2, nativeVerticalResolution / 2 + 185, buttonSize.x, 50), timeString);
        }

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 275, nativeVerticalResolution - 300, 550, 300), Languages.Instance.GetTranslation("Current User") + "\n" + playerStatus);

        // Error box
        if (invalidInput)
        {
            GUI.Box(new Rect(scaledResolutionWidth / 2 - 380, 15, 760, 100), errorMessage);
        }

        DrawPopup();

        timeString = Regex.Replace(timeString, @"[^0-9]", "");
    }

    private void DrawPopup()
    {
        if (popUp)
        {
            GUI.Box(new Rect(scaledResolutionWidth / 2 - 350, nativeVerticalResolution / 2 - 445, 700, 750), "", "Window");

            if (calibrationError)
            {
                GUI.Box(new Rect(scaledResolutionWidth / 2 - 270, nativeVerticalResolution / 2 - 400, 540, 540), Languages.Instance.GetTranslation("Calibration Error!") + "\n\n" + Languages.Instance.GetTranslation("Make sure the part of the box with 'table' on it is closest to the table."), "EndBox");

                if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution - 380, 300, 100), Languages.Instance.GetTranslation("Okay")))
                {
                    popUp = false;
                    calibrationError = false;
                }
            }
            else
            {
                GUI.Box(new Rect(scaledResolutionWidth / 2 - 270, nativeVerticalResolution / 2 - 400, 540, 540), Languages.Instance.GetTranslation("Make sure the circle on your calibration box is centered with the patient's body!"), "EndBox");

                if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution - 380, 300, 100), Languages.Instance.GetTranslation("Okay")))
                {
                    GameControl.Instance.IsCalibrating = true;
                    GameControl.Instance.IsCalibrated = false;
                    try
                    {
                        calibration.Kill();
                    }
                    catch (Exception)
                    { }
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Calibration Started"));
                    calibration = new Calibration();
                    calibration.Calibrate();
                    popUp = false;
                }
            }

            //if (GUI.Button(new Rect(scaledResolutionWidth / 2 + 15, nativeVerticalResolution - 380, 300, 100), Languages.Instance.GetTranslation("No")))
            //{            
            //    popUp = false;
            //}
        }
    }

    void OnDestroy()
    {
        try
        {
            calibration.Kill();
        }
        catch (Exception) { }
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
