using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class SettingsGUI : MonoBehaviour
{

    public GUISkin mainMenuSkin;
    public MainMenuGUI mainMenu;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI, ulnaLength;
    private string birthdate, IDstring, name, ulnaLengthString, errorMessage, loadSave;
    private int ID, textBoxWidth = 300, textBoxHeight = 50, brookeScale;
    private DateTime birthDateTime;
    
    internal XmlSettings settings = new XmlSettings();
    internal bool newUser, invalidInput = false, saving = true;

    // Use this for initialization
    void Start()
    {
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        birthdate = IDstring = name = ulnaLengthString = "";
        brookeScale = 1;
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
            NewID();
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
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 310, textBoxWidth, textBoxHeight), "Name");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 200, textBoxWidth, textBoxHeight), "ID (Generated)");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 90, textBoxWidth, textBoxHeight), "Birthdate (mm/dd/yy)");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 20, textBoxWidth, textBoxHeight), "Brooke Scale: " + brookeScale.ToString());
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 130, textBoxWidth, textBoxHeight), "Ulna Length");

            name = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 275, textBoxWidth, textBoxHeight), name);
            GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 165, textBoxWidth, textBoxHeight), ID.ToString());
            birthdate = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 55, textBoxWidth, textBoxHeight), birthdate);
            brookeScale = (int)GUI.HorizontalSlider(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 75, textBoxWidth, textBoxHeight), brookeScale, 1, 6);
            ulnaLengthString = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 165, textBoxWidth, textBoxHeight), ulnaLengthString);

            loadSave = "Save";
            saving = true;
        }
        else
        {
            // Text field labels for existing users
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 90, textBoxWidth, textBoxHeight), "Identification Number");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 20, textBoxWidth, textBoxHeight), "Brooke Scale: " + brookeScale.ToString());
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 130, textBoxWidth, textBoxHeight), "Ulna Length");

            // Text fields for exitsting users
            IDstring = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 55, textBoxWidth, textBoxHeight), IDstring);
            brookeScale = (int) GUI.HorizontalSlider(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 55, textBoxWidth, textBoxHeight), brookeScale, 0, 6);
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
                    GameControl.Instance.AddUser(name, birthDateTime, ID, brookeScale, ulnaLength);
                    GameControl.Instance.Save();
                }
                else
                {
                    GameControl.Instance.LoadUser(ID, brookeScale, ulnaLength);
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

                if (!GameControl.Instance.playerData.ContainsKey(ID))
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

        settings.SetXmlSettings(name, ID, birthDateTime.Month.ToString() + '/' + birthDateTime.Day.ToString() +'/' + birthDateTime.Year.ToString(), brookeScale, ulnaLength);
    }

    private void NewID()
    {
        ID = UnityEngine.Random.Range(0, 500);

        if (GameControl.Instance.playerData.ContainsKey(ID))
        {
            NewID();
        }
    }
}
