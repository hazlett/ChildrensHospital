﻿using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class MainMenuGUI : MonoBehaviour
{

    public GUISkin mainMenuSkin;
    public SettingsGUI settings;
    public InstructionsGUI instructions;

    internal Calibration calibration;

    private string playerStatus, timeString, errorMessage;
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private bool manualCalibration = false, invalidInput = false;

    void Start()
    {
        errorMessage = "Invalid trial length. \nPlease enter a trial length between 0 and 120.";
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

        if ((UserContainer.Instance.UserDictionary.ContainsKey(settings.user.ID)) && (GameControl.Instance.IsCalibrated))
        {
            if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 - 275, 300, 100), "Start Trial"))
            {
                GameControl.Instance.ReadCalibration();
                CheckTime();
                if (!invalidInput)
                {
                    Application.LoadLevel("Game");
                }
            }
        }
        else if ((UserContainer.Instance.UserDictionary.ContainsKey(settings.user.ID)) && (!GameControl.Instance.IsCalibrated))
        {
            string calibrateMessage;
            if (GameControl.Instance.IsCalibrating)
            {
                calibrateMessage = "Calibrating";
            }
            else
            {
                calibrateMessage = "Please Calibrate";
            }
            GUI.Label(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 - 275, 300, 100), calibrateMessage, "GreyStart");
        }
        else
        {
            GUI.Label(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 - 275, 300, 100), "Choose User", "GreyStart");
        }

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 - 170, 300, 100), "New User"))
        {
            GameControl.Instance.IsCalibrated = false;
            settings.newUser = true;
            settings.enabled = true;
            this.enabled = false;
        }

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 - 65, 300, 100), "Existing User"))
        {
            GameControl.Instance.IsCalibrated = false;
            settings.newUser = false;
            settings.enabled = true;
            this.enabled = false;
        }

        if (UserContainer.Instance.UserDictionary.ContainsKey(settings.user.ID))
        {
            if (!GameControl.Instance.IsCalibrated)
            {
                if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 + 40, 300, 100), "Calibrate"))
                {
                    GameControl.Instance.IsCalibrating = true;
                    try
                    {
                        calibration.Kill();
                    }
                    catch (Exception)
                    { }
                    calibration = new Calibration();
                    calibration.Calibrate();
                }
            }
            else
            {
                if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution / 2 + 40, 300, 100), "Recalibrate"))
                {
                    GameControl.Instance.IsCalibrating = true;
                    GameControl.Instance.IsCalibrated = false;
                    try
                    {
                        calibration.Kill();
                    }
                    catch (Exception)
                    { }
                    calibration = new Calibration();
                    calibration.Calibrate();
                }
            }
        }

        if (GUI.Button(new Rect(25, nativeVerticalResolution - 125, 300, 100), "INSTRUCTIONS"))
        {
            instructions.enabled = true;
            this.enabled = false;
        }

        if (GUI.Button(new Rect(scaledResolutionWidth - 325, nativeVerticalResolution - 125, 300, 100), "QUIT"))
        {
            Application.Quit();
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
