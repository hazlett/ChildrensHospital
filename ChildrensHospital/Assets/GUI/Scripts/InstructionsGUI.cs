using UnityEngine;
using System.Collections;

public class InstructionsGUI : MonoBehaviour
{

    public GUISkin mainMenuSkin;
    public MainMenuGUI mainMenu;
    public Texture2D[] instructions;

    private string instuctionsList;
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;
    private int textureNumber;

    void Start()
    {
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        this.enabled = false;
    }

    void Update()
    {
        InstructionsLanguage();
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

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 810, nativeVerticalResolution / 2 - 520, 1620, 920), instructions[textureNumber], "Window");

        if(GUI.Button(new Rect(scaledResolutionWidth - 325, nativeVerticalResolution - 125, 300, 100), Languages.Instance.GetTranslation("Back to Menu")))
        {
            this.enabled = false;
            mainMenu.enabled = true;
        }
        
    }

    private void InstructionsLanguage()
    {
        if (LanguageUsed.Instance.CurrentLanguage > instructions.Length || instructions[LanguageUsed.Instance.CurrentLanguage] == null)
        {
            textureNumber = 0;
        }
        else
        {
            textureNumber = LanguageUsed.Instance.CurrentLanguage;
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
