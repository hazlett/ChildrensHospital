using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

public class SettingsGUI : MonoBehaviour
{

    public GUISkin mainMenuSkin;
    public MainMenuGUI mainMenu;
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI, ulnaLength;
    private string birthdate, IDstring, name, ulnaLengthString, errorMessage, loadSave, diagnosis;
    private int ID, textBoxWidth = 300, textBoxHeight = 50, brookeScale;
    private DateTime birthDateTime;

  
    internal User user = new User();
    internal bool newUser, invalidInput = false, saving = true, male = true;

    // Use this for initialization
    void Start()
    {
        user.LoadUsers();
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        birthdate = IDstring = name = ulnaLengthString = diagnosis = "";
        loadSave = "Save";
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        TimedScreenResize();
    }

    void OnEnable()
    {
        if (newUser)
        {
            brookeScale = 1;
        }
        else
        {
            brookeScale = 0;
        }
        birthdate = IDstring = name = ulnaLengthString = "";
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

        if (newUser)
        {

            // Text fields for new users
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 420, textBoxWidth, textBoxHeight), "Name");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 310, textBoxWidth, textBoxHeight), "ID");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 200, textBoxWidth, textBoxHeight), "Birthdate (mm/dd/yy)");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 90, textBoxWidth, textBoxHeight), "Brooke Scale: " + brookeScale.ToString());
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2, textBoxWidth, textBoxHeight), "Ulna Length");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 90, textBoxWidth, textBoxHeight), "Diagnosis");

            name = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 385, textBoxWidth, textBoxHeight), name);
            IDstring = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 275, textBoxWidth, textBoxHeight), IDstring);
            birthdate = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 165, textBoxWidth, textBoxHeight), birthdate);
            brookeScale = (int)GUI.HorizontalSlider(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 55, textBoxWidth, textBoxHeight), brookeScale, 1, 6);
            ulnaLengthString = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 35, textBoxWidth, textBoxHeight), ulnaLengthString);
            diagnosis = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 125, textBoxWidth, textBoxHeight), diagnosis);
            male = GUI.Toggle(new Rect(scaledResolutionWidth / 2 - 80, nativeVerticalResolution / 2 + 215, 50, 25), male, " ", "Gender");

            if (male)
            {
                GUI.Label(new Rect(scaledResolutionWidth / 2 - 70, nativeVerticalResolution / 2 + 130, 200, 200), "Male");
            }
            else
            {
                GUI.Label(new Rect(scaledResolutionWidth / 2 - 70, nativeVerticalResolution / 2 + 130, 200, 200), "Female");
            }

            loadSave = "Save";
            saving = true;
        }
        else
        {
            // Text field labels for existing users
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 90, textBoxWidth, textBoxHeight), "Identification Number");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 20, textBoxWidth, textBoxHeight), "Brooke Scale: " + brookeScale.ToString());
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 130, textBoxWidth, textBoxHeight), "Ulna Length");

            // Text fields for existing users
            IDstring = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 55, textBoxWidth, textBoxHeight), IDstring);
            brookeScale = (int)GUI.HorizontalSlider(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 55, textBoxWidth, textBoxHeight), brookeScale, 0, 6);
            ulnaLengthString = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 165, textBoxWidth, textBoxHeight), ulnaLengthString);

            loadSave = "Load";
            saving = false;
        }

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution - 255, 300, 100), loadSave))
        {
            ApplyChanges();
            if (!invalidInput)
            {
                mainMenu.enabled = true;
                this.enabled = false;

                if (saving)
                {
                    string gender = "female";
                    if (male)
                    {
                        gender = "male";
                    }

                    user = new User(name, ID, birthDateTime, brookeScale, ulnaLength, diagnosis, male);
                    user.SaveUser();
                    DocumentManager.Instance.ArgsCreated = false;
                    EventLogger.Instance.LogData("New user selected.  Name: " + name + " ID: " + ID + " Birthday: "
                        + birthDateTime + " Brooke Scale: " + brookeScale + " Ulna Length: " + ulnaLength + "Diagnosis: " + diagnosis + " Gender: " + gender);
                }
                else
                {
                    string gender = "female";
                    if (UserContainer.Instance.UserDictionary[ID].Gender)
                    {
                        gender = "male";
                    }
                    user.LoadSpecificUser(ID, brookeScale, ulnaLength);
                    DocumentManager.Instance.ArgsCreated = false;
                    EventLogger.Instance.LogData("Existing user selected.  Name: " + UserContainer.Instance.UserDictionary[ID].Name + " ID: " + ID + " Birthday: "
                        + UserContainer.Instance.UserDictionary[ID].Birthdate + " Brooke Scale: " + UserContainer.Instance.UserDictionary[ID].BrookeScale
                        + " Ulna Length: " + UserContainer.Instance.UserDictionary[ID].UlnaLength + " Gender: " + gender);
                }
            }
        }

        // Cancel Button
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution - 150, 300, 100), "Cancel"))
        {
            invalidInput = false;
            mainMenu.enabled = true;
            this.enabled = false;
        }

        // Error box
        if (invalidInput)
        {
            GUI.Box(new Rect(scaledResolutionWidth / 2 - 380, 15, 760, 100), errorMessage);
        }

        // Limiting entry characters for each specific text field
        name = Regex.Replace(name, @"[^a-zA-Z.]", "");
        birthdate = Regex.Replace(birthdate, @"[^0-9/]", "");
        ulnaLengthString = Regex.Replace(ulnaLengthString, @"[^0-9.]", "");
        IDstring = Regex.Replace(IDstring, @"[^0-9]", "");
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }

    // Error checking for changes
    // For new users, check that nothing is blank and that everything is in the correct format.
    // For existing users, check if the user exists, and update user data if necessary.
    private void ApplyChanges()
    {
        if (newUser)
        {
            if (name.Equals(string.Empty))
            {
                errorMessage = "  Name is blank.\n  Please enter a valid name.";
                invalidInput = true;
                return;
            }
            else
            {
                invalidInput = false;
            }

            try
            {
                birthDateTime = DateTime.Parse(birthdate);
                invalidInput = false;
            }
            catch (Exception)
            {
                errorMessage = "  Birthdate is invalid.\n  Please follow the given format.";
                invalidInput = true;
                return;
            }

            try
            {
                ulnaLength = float.Parse(ulnaLengthString);
                invalidInput = false;
            }
            catch (Exception)
            {
                errorMessage = "  Invalid ulna length.\n  Please enter a correct length.";
                invalidInput = true;
                return;
            }

            try
            {
                ID = int.Parse(IDstring);

                if (UserContainer.Instance.UserDictionary.ContainsKey(ID))
                {
                    invalidInput = true;
                    errorMessage = "  ID Number " + IDstring + " exists.\n  Please choose a new ID number.";
                }
                else
                {
                    invalidInput = false;
                }
            }
            catch (Exception)
            {
                errorMessage = "  Invalid identification number.\n  Please enter a correct identification number.";
                invalidInput = true;
                return;
            }
        }

        if (!newUser)
        {
            try
            {
                if (ulnaLengthString.Equals(""))
                {
                    ulnaLength = 0;
                }
                else
                {
                    ulnaLength = float.Parse(ulnaLengthString);
                }
                invalidInput = false;
            }
            catch (Exception)
            {
                errorMessage = "  Invalid ulna length.\n  Please enter a correct length.";
                invalidInput = true;
                return;
            }

            try
            {
                ID = int.Parse(IDstring);

                if (!UserContainer.Instance.UserDictionary.ContainsKey(ID))
                {
                    invalidInput = true;
                    errorMessage = "  No user with ID Number: " + IDstring + " exists.\n  Please enter a correct identification number.";
                }
                else
                {
                    invalidInput = false;
                }
            }
            catch (Exception)
            {
                errorMessage = "  Invalid identification number.\n  Please enter a correct identification number.";
                invalidInput = true;
                return;
            }


        }
    }
}
