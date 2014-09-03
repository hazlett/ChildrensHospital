using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class SettingsGUI : MonoBehaviour
{

    public GUISkin mainMenuSkin;
    public MainMenuGUI mainMenu;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI, ulnaLength, brooksScale;
    private string birthdate, IDstring, name, brooksScaleString, ulnaLengthString, errorMessage;
    private int ID, textBoxWidth = 300, textBoxHeight = 50;
    private DateTime birthDateTime;
    
    internal XmlSettings settings = new XmlSettings();
    internal bool newUser, invalidInput = false;

    // Use this for initialization
    void Start()
    {
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        birthdate = IDstring = name = brooksScaleString = ulnaLengthString = "";
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
            ID = UnityEngine.Random.Range(0, 2000);
        }
        birthdate = IDstring = name = brooksScaleString = ulnaLengthString = "";
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
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 200, textBoxWidth, textBoxHeight), "Name");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 90, textBoxWidth, textBoxHeight), "ID (Generated)");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 20, textBoxWidth, textBoxHeight), "Birthdate (mm/dd/yy)");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 130, textBoxWidth, textBoxHeight), "Brook's Scale");

            name = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 165, textBoxWidth, textBoxHeight), name);
            GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 55, textBoxWidth, textBoxHeight), ID.ToString());
            birthdate = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 55, textBoxWidth, textBoxHeight), birthdate);
            brooksScaleString = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 165, textBoxWidth, textBoxHeight), brooksScaleString);

        }
        else
        {
            // Text fields for new users
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 310, textBoxWidth, textBoxHeight), "Name");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 200, textBoxWidth, textBoxHeight), "Identification Number");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 90, textBoxWidth, textBoxHeight), "Birthdate (mm/dd/yy)");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 20, textBoxWidth, textBoxHeight), "Brook's Scale");
            GUI.Label(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 130, textBoxWidth, textBoxHeight), "Ulna Length");

            // Text fields for exitsting users
            name = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 275, textBoxWidth, textBoxHeight), name);
            IDstring = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 165, textBoxWidth, textBoxHeight), IDstring);
            birthdate = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 - 55, textBoxWidth, textBoxHeight), birthdate);
            brooksScaleString = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 55, textBoxWidth, textBoxHeight), brooksScaleString);
            ulnaLengthString = GUI.TextField(new Rect(scaledResolutionWidth / 2 - (textBoxWidth / 2), nativeVerticalResolution / 2 + 165, textBoxWidth, textBoxHeight), ulnaLengthString);
        }

        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution - 255, 300, 100), "Save"))
        {
            ApplyChanges();
            if (!invalidInput)
            {
                mainMenu.enabled = true;
                this.enabled = false;
            }
        }
        if (GUI.Button(new Rect(scaledResolutionWidth / 2 - 150, nativeVerticalResolution - 150, 300, 100), "Cancel"))
        {
            invalidInput = false;
            mainMenu.enabled = true;
            this.enabled = false;
        }

        if (invalidInput)
        {
            GUI.Box(new Rect(scaledResolutionWidth / 2 - 380, 15, 760, 100), errorMessage);
        }

        brooksScaleString = Regex.Replace(brooksScaleString, @"[^0-9.]", "");
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

    private void ApplyChanges()
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
            brooksScale = float.Parse(brooksScaleString);
            invalidInput = false;
        }
        catch (Exception)
        {
            errorMessage = "  Invalid Brook's Scale Format.\n  Please enter the correct format.";
            invalidInput = true;
            return;
        }

        if (!newUser)
        {
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
                invalidInput = false;
            }
            catch (Exception)
            {
                errorMessage = "  Invalid identification number.\n  Please enter a correct identification number.";
                invalidInput = true;
                return;
            }
        }

        settings.SetXmlSettings(name, ID, birthDateTime.Month.ToString() + '/' + birthDateTime.Day.ToString() +'/' + birthDateTime.Year.ToString(), brooksScale, ulnaLength);
    }
}
