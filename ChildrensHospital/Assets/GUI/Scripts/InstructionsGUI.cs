using UnityEngine;
using System.Collections;

public class InstructionsGUI : MonoBehaviour
{

    public GUISkin mainMenuSkin;
    public MainMenuGUI mainMenu;
    public Texture2D instructions;

    private string instuctionsList;
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI;

    void Start()
    {
        instuctionsList = "CHOOSE NEW USER OR EXISTING USER\nEnter all data for new user.\nFor existing user, confirm that Brooke Scale and Ulna Length are correct." +
        " \nTrial Length is defaulted to 60 seconds. Enter in a new duration if needed." +
        " \n\nCALIBRATION\nCheckered box is precisely aligned with the back edge of the table.  Center dot in center of the table.  Click calibrate." +
        " If unsuccessful, check to see that the box is located within the red square on the screen and recalibrate.  You will need to recalibrate if" +
        " you move the table or camera." +
        "\n\nGAMEPLAY\n1. Before clicking Start Trial\nEnsure the patient is in the proper sitting position.\nInstruct the subject that they are in a cave" +
        " searching for jewels.  To get the jewels he/she must use their arm movements (lasers in the game) to push the walls back.  There are 3 sections to each wall" +
        " (tabletop, mid, over the head).  Be sure to push all 3 sections.  The ceiling will glow yellow first.  Continue to push each wall as it glows." +
        " Push it up as high as you can (by leaning as far as possible) to get the most jewels.\nRemember:  Don't stand up, don't hold on to the table or chair." +
        "\n2. Click Start Trial and the countdown begins.  When the bell rings the avatar on the screen will mimic the subject's movements." +
        "\n\nEND GAME\nIf the trial was NOT valid, uncheck the Save Results box.  Play a minimum of 2 times.  Play additional trials until the subject achieves their " +
        "high score.  When achieved, click Quit." +
        "\n\nREPORT\nThe report generats automatically when the game is done and you click Quit.  It is automatically saved in My Documents\\ACTIVE-Seated\\Reports. ";
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        Debug.Log(instuctionsList);
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

        GUI.Box(new Rect(scaledResolutionWidth / 2 - 810, nativeVerticalResolution / 2 - 520, 1620, 920), instructions, "Window");

        if(GUI.Button(new Rect(scaledResolutionWidth - 325, nativeVerticalResolution - 125, 300, 100), Languages.Instance.GetTranslation("Back to Menu")))
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
