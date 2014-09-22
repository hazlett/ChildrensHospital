using UnityEngine;
using System.Collections;

public class InstructionsGUI : MonoBehaviour
{

    public GUISkin mainMenuSkin;
    public MainMenuGUI mainMenu;

    private string instuctionsList;
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;

    void Start()
    {
        instuctionsList = "GETTING STARTED\nStart off by choosing either new user or existing user. The box below these buttons shows the currently selected user, and their information (If you just started the game there won't be a currently selected user). You may also change the duration of the trial by inputting the number of seconds into the Trial Length field.\n\nNEW USER\nHere you set the user's name, birthdate, Brooke scale, ulna length, and are given a user ID(Please keep track of this.) If everything is entered correctly, click save and this user will be updated as your current user.\n\nEXISTING USER\nHere you will enter the ID of the user you want to load.  You may also change the Brooke Scale and/or ulna length if you need to by inputting new values. Click load to make this user your current user.\n\nCALIBRATION\nWhenever a new user is saved, or an existing user is loaded, the game will need to calibrate the Kinect.  Make sure that you have the calibration checkerboard in the correct position before selecting a user or it could mess up the end data.\n\nGAMEPLAY\nThe game will take a few seconds to start, but once it does, the goal is to dig for gems.  As the user stretches, the dirt is eroded and gems are revealed and fall down.  The game will also show which side needs work if one side is less than the other by focusing the camera on it.\n\nEND GAME\nWhen the game ends, it will display the stats of the trial. You will have an option to play the trial again or quit. It will also save the results if \"Save Trial\" is selected.\n\nSAVING\nGame data is saved into a file under personal\\ReachVolumeGame\\Extremas.csv";
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        this.enabled = false;
    }

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

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 760, nativeVerticalResolution / 2 - 520, 1520, 920), instuctionsList, "Window");

        if(GUI.Button(new Rect(scaledResolutionWidth - 325, nativeVerticalResolution - 125, 300, 100), "Back to Menu"))
        {
            this.enabled = false;
            mainMenu.enabled = true;
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
