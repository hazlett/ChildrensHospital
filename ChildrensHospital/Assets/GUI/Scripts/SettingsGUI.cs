using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

public class SettingsGUI : MonoBehaviour
{

    public GUISkin mainMenuSkin;
    public MainMenuGUI mainMenu;
    public DropdownUserListGUI dropdown;
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI, ulnaLength, brookeScale;
    internal string birthdate, IDstring, name, ulnaLengthString, errorMessage, loadSave, diagnosis;
    private int ID, textBoxWidth = 300, textBoxHeight = 50, brookeInt;
    private DateTime birthDateTime;
    private Vector2 labelSize = new Vector2(600, 50), buttonSize = new Vector2(350, 100);
  
    internal User user = new User();
    internal bool newUser, invalidInput = false, saving = true, male = true;

    // Use this for initialization
    void Start()
    {
        user.LoadUsers();
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        birthdate = IDstring = name = ulnaLengthString = diagnosis = "";
        loadSave = Languages.Instance.GetTranslation("Save");
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        TimedScreenResize();
    }
    void OnDestroy()
    {
        LanguageUsed.Instance.Save();
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
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (labelSize.x / 2), nativeVerticalResolution / 2 - 420, labelSize.x, labelSize.y), Languages.Instance.GetTranslation("Name"));
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (labelSize.x / 2), nativeVerticalResolution / 2 - 310, labelSize.x, labelSize.y), Languages.Instance.GetTranslation("ID"));
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (labelSize.x / 2), nativeVerticalResolution / 2 - 200, labelSize.x, labelSize.y), Languages.Instance.GetTranslation("Birthdate") + " " + Languages.Instance.GetTranslation("(mm/dd/yy)"));
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (labelSize.x / 2), nativeVerticalResolution / 2 - 90, labelSize.x, labelSize.y), Languages.Instance.GetTranslation("Brooke Scale") +": " + brookeInt);
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (labelSize.x / 2), nativeVerticalResolution / 2, labelSize.x, labelSize.y), Languages.Instance.GetTranslation("Ulna Length"));
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (labelSize.x / 2), nativeVerticalResolution / 2 + 90, labelSize.x, labelSize.y), Languages.Instance.GetTranslation("Diagnosis"));

            name = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 385, textBoxWidth, textBoxHeight), name);
            IDstring = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 275, textBoxWidth, textBoxHeight), IDstring);
            birthdate = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 165, textBoxWidth, textBoxHeight), birthdate);
            brookeScale = GUI.HorizontalSlider(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 55, textBoxWidth, textBoxHeight), brookeScale, 1, 6);
            ulnaLengthString = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 35, textBoxWidth, textBoxHeight), ulnaLengthString);
            diagnosis = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 125, textBoxWidth, textBoxHeight), diagnosis);
            male = GUI.Toggle(new Rect(scaledResolutionWidth / 2 - 80, nativeVerticalResolution / 2 + 215, 50, 25), male, " ", "Gender");

            if (male)
            {
                GUI.Label(new Rect(scaledResolutionWidth / 2 - 50, nativeVerticalResolution / 2 + 130, 200, 200), Languages.Instance.GetTranslation("Male"));
            }
            else
            {
                GUI.Label(new Rect(scaledResolutionWidth / 2 - 50, nativeVerticalResolution / 2 + 130, 200, 200), Languages.Instance.GetTranslation("Female"));
            }

            loadSave = Languages.Instance.GetTranslation("Save");
            saving = true;
        }
        else
        {
            // Text field labels for existing users
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (labelSize.x / 2), nativeVerticalResolution / 2 - 90, labelSize.x, labelSize.y), Languages.Instance.GetTranslation("Identification Number"));
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (labelSize.x / 2), nativeVerticalResolution / 2 + 20, labelSize.x, labelSize.y), Languages.Instance.GetTranslation("Brooke Scale") + ": " + brookeInt);
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (labelSize.x / 2), nativeVerticalResolution / 2 + 130, labelSize.x, labelSize.y), Languages.Instance.GetTranslation("Ulna Length"));

            // Text fields for existing users
            IDstring = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 55, textBoxWidth, textBoxHeight), IDstring);
            brookeScale = GUI.HorizontalSlider(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 55, textBoxWidth, textBoxHeight), brookeScale, 0, 6);
            ulnaLengthString = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 165, textBoxWidth, textBoxHeight), ulnaLengthString);

            loadSave = Languages.Instance.GetTranslation("Load");
            saving = false;

            dropdown.enabled = true;
        }

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution - 255, 300, 100), loadSave))
        {
            ApplyChanges();
            if (!invalidInput)
            {
                mainMenu.dropdown.enabled = true;
                mainMenu.enabled = true;
                dropdown.yPosition = 0.0f;
                dropdown.opened = dropdown.enabled = false;
                this.enabled = false;

                if (saving)
                {
                    string gender = Languages.Instance.GetTranslation("female");
                    if (male)
                    {
                        gender = Languages.Instance.GetTranslation("male");
                    }

                    user = new User(name, ID, birthDateTime, brookeInt, ulnaLength, diagnosis, male);
                    user.SaveUser();
                    DocumentManager.Instance.ArgsCreated = false;
                    EventLogger.Instance.LogData("New user selected.  Name: " + name + " ID: " + ID + " Birthday: "
                        + birthDateTime + " Brooke Scale: " + brookeScale + " Ulna Length: " + ulnaLength + "Diagnosis: " + diagnosis + " Gender: " + gender);
                }
                else
                {
                    string gender = Languages.Instance.GetTranslation("female");
                    if (UserContainer.Instance.UserDictionary[ID].Gender)
                    {
                        gender = Languages.Instance.GetTranslation("male");
                    }
                    user.LoadSpecificUser(ID, brookeInt, ulnaLength);
                    DocumentManager.Instance.ArgsCreated = false;
                    EventLogger.Instance.LogData("Existing user selected.  Name: " + UserContainer.Instance.UserDictionary[ID].Name + " ID: " + ID + " Birthday: "
                        + UserContainer.Instance.UserDictionary[ID].Birthdate + " Brooke Scale: " + UserContainer.Instance.UserDictionary[ID].BrookeScale
                        + " Ulna Length: " + UserContainer.Instance.UserDictionary[ID].UlnaLength + " Gender: " + gender);
                }
            }
        }

        brookeInt = Mathf.RoundToInt(brookeScale);

        // Cancel Button
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution - 150, 300, 100), Languages.Instance.GetTranslation("Cancel")))
        {
            invalidInput = false;
            mainMenu.dropdown.enabled = true;
            mainMenu.enabled = true;
            dropdown.yPosition = 0.0f;
            dropdown.opened = dropdown.enabled = false;
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

        // Error box
        if (invalidInput)
        {
            GUI.Box(new Rect(scaledResolutionWidth / 2 - 380, 15, 760, 100), errorMessage);
        }
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
                errorMessage = "  " + Languages.Instance.GetTranslation("Name is blank") + ".\n  " + Languages.Instance.GetTranslation("Please enter a valid name") + ".";
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
                errorMessage = "  " + Languages.Instance.GetTranslation("Birthdate is invalid") + ".\n  " + Languages.Instance.GetTranslation("Please follow the given format");
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
                errorMessage = "  " + Languages.Instance.GetTranslation("Invalid ulna length") + ".\n  " + "Please enter a correct length" + ".";
                invalidInput = true;
                return;
            }

            try
            {
                ID = int.Parse(IDstring);

                if (UserContainer.Instance.UserDictionary.ContainsKey(ID))
                {
                    invalidInput = true;
                    errorMessage = "  " + Languages.Instance.GetTranslation("ID Number") + " " + IDstring + " " + Languages.Instance.GetTranslation("exists") + ".\n  " + "Please choose a new ID number" + ".";
                }
                else
                {
                    invalidInput = false;
                }
            }
            catch (Exception)
            {
                errorMessage = "  " + Languages.Instance.GetTranslation("Invalid identification number") + ".\n  " + Languages.Instance.GetTranslation("Please enter a correct identification number") + ".";
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
                errorMessage = "  " + Languages.Instance.GetTranslation("Invalid ulna length") + ".\n  " + "Please enter a correct length" + ".";
                invalidInput = true;
                return;
            }

            try
            {
                ID = int.Parse(IDstring);

                if (!UserContainer.Instance.UserDictionary.ContainsKey(ID))
                {
                    invalidInput = true;
                    errorMessage = "  " +  Languages.Instance.GetTranslation("No user with ID Number") + ": " + IDstring + " " +  Languages.Instance.GetTranslation("exists") + ".\n  " +  Languages.Instance.GetTranslation("Please enter a correct identification number") + ".";
                }
                else
                {
                    invalidInput = false;
                }
            }
            catch (Exception)
            {
                errorMessage = "  " + Languages.Instance.GetTranslation("Invalid identification number") + ".\n  " + Languages.Instance.GetTranslation("Please enter a correct identification number") + ".";
                invalidInput = true;
                return;
            }


        }
    }
}
