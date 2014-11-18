using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
public class EndGUI : MonoBehaviour
{
    public GUISkin mainMenuSkin;
    public GameManager gameManager;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private bool saveData = true, popUp = false, quit = true;
    private string args;
    private static List<int> previousScores = new List<int>();
    private string previousScoresString, validityCheck, spiderGems;
    private static List<float> previousVolumes = new List<float>();
    // Use this for initialization
    void Start()
    {
        if (UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Gender)
        {
            spiderGems = Languages.Instance.GetTranslation("Spiders Squished");
        }
        else
        {
            spiderGems = Languages.Instance.GetTranslation("Gems Collected");
        }
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        DocumentManager.Instance.CreateArgs();
        enabled = false;
    }

    void OnEnable()
    {
        popUp = false;
        previousScoresString = "";
        foreach (int score in previousScores)
        {
            previousScoresString += (score.ToString() + "\n");
        }
        validityCheck = DataValidity.Instance.CheckValidity(previousVolumes);
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

        if (popUp)
        {
            DrawPopup();
        }
        else
        {
            DrawMain();
        }
    }


    private void AppendArgs()
    {
        DocumentManager.Instance.AppendArgs(gameManager);
    }

    private void DrawMain()
    {
        GUI.Box(new Rect(scaledResolutionWidth / 2 - 350, nativeVerticalResolution / 2 - 445, 700, 850), "", "Window");

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 270, nativeVerticalResolution / 2 - 400, 540, 540), Languages.Instance.GetTranslation("Results") + "\n" + Languages.Instance.GetTranslation("Name") + ": " + UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Name
           + "\n" + Languages.Instance.GetTranslation("ID") + ": " + UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].ID + "\n" + Languages.Instance.GetTranslation("Volume") + ": " + gameManager.TotalVolume().ToString("F4") + " " + Languages.Instance.GetTranslation("meters cubed") + 
           "\n" + spiderGems + ": " + GameControl.Instance.GemsCollected.ToString() + "\n" + Languages.Instance.GetTranslation("Previous") + " " + spiderGems + "\n" + previousScoresString, "EndBox");


        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 315, nativeVerticalResolution - 380, 300, 100), Languages.Instance.GetTranslation("Run Trial Again")))
        {
            AppendArgs();
            previousScores.Add(GameControl.Instance.GemsCollected);
            if (saveData)
            {
                validityCheck = DataValidity.Instance.CheckValidity(previousVolumes);
                Debug.Log("Validity check: " + validityCheck);
                if (validityCheck == null)
                {
                    previousVolumes.Add(GameControl.Instance.totalVolume);
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Saving Data"));
                    gameManager.Tracker.DebugExtremas();
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Trial Ended"));
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Starting New Trial"));
                    Application.LoadLevel("Game");
                }
                else
                {
                    popUp = true;
                    quit = false;
                }
            }
            else 
            {
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Trial Ended"));
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Starting New Trial"));
                    Application.LoadLevel("Game");
            }
        }
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 + 15, nativeVerticalResolution - 380, 300, 100), Languages.Instance.GetTranslation("Quit")))
        {
            AppendArgs();
            previousScores = new List<int>();
            previousVolumes = new List<float>();
            if (saveData)
            {
                validityCheck = DataValidity.Instance.CheckValidity(previousVolumes);
                Debug.Log("Validity check: " + validityCheck);
                if (validityCheck == null)
                {
                    previousVolumes.Add(GameControl.Instance.totalVolume);
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Saving Data"));
                    gameManager.Tracker.DebugExtremas();
                    DocumentManager.Instance.CreateReport();
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Trial Ended"));
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Exiting to Menu"));
                    Application.LoadLevel("MainMenu");
                }
                else
                {
                    popUp = true;
                    quit = true;
                }
            }
            else
            {
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Trial Ended"));
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Exiting to Menu"));
                    Application.LoadLevel("MainMenu");
            }
        }

        saveData = GUI.Toggle(new Rect(scaledResolutionWidth / 2 - 200, nativeVerticalResolution - 250, 50, 50), saveData, "");

        GUI.Label(new Rect(scaledResolutionWidth / 2, nativeVerticalResolution - 250, 200, 50), Languages.Instance.GetTranslation("Save Results"), "ToggleLabel");
    }

    private void DrawPopup()
    {
        GUI.Box(new Rect(scaledResolutionWidth / 2 - 350, nativeVerticalResolution / 2 - 445, 700, 850), "", "Window");
        GUI.Box(new Rect(scaledResolutionWidth / 2 - 270, nativeVerticalResolution / 2 - 400, 540, 540), "Based on our calculations, this doesn't look like a valid trial.\n" + validityCheck +"\n\nAre you sure you would like to save this data?", "EndBox");

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 315, nativeVerticalResolution - 380, 300, 100), Languages.Instance.GetTranslation("Yes")))
        {
            previousVolumes.Add(GameControl.Instance.totalVolume);
            EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Saving Data"));
            gameManager.Tracker.DebugExtremas();
            
            if(quit)
            {                
                DocumentManager.Instance.CreateReport();
                EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Trial Ended"));
                EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Exiting to Menu"));
                Application.LoadLevel("MainMenu");
            }
            else
            {
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Trial Ended"));
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Starting New Trial"));
                    Application.LoadLevel("Game");
            }


            popUp = false;
        }

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 + 15, nativeVerticalResolution - 380, 300, 100), Languages.Instance.GetTranslation("No")))
        {
            if(quit)
            {
                EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Trial Ended"));
                EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Exiting to Menu"));
                Application.LoadLevel("MainMenu");
            }
            else
            {
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Trial Ended"));
                    EventLogger.Instance.LogData(Languages.Instance.GetTranslation("Starting New Trial"));
                    Application.LoadLevel("Game");
            }
            
            popUp = false;
        }
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }
}
