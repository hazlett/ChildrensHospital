using UnityEngine;
using System.Collections;

public class DropdownUserListGUI : MonoBehaviour {

    public GUISkin dropdownSkin;
    public SettingsGUI settings;
    public int maxDropdownSize = 5;
    public float animationDuration = 1.5f;
    public Vector2 menuPosition = new Vector2(0, 0), buttonSize = new Vector2(400, 50);

    internal bool disabling;
    internal float timer, speed;
    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI, yPosition, arrowsYPos;
    private int startList;

    void Start()
    {
        timer = yPosition = startList = 0;
        updateGUI = 0.5f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        this.enabled = false;
    }

    // Makes sure the animation will function properly upon opening the menu again
    void OnDisable()
    {
        disabling = false;
        timer = yPosition = startList = 0;
        speed = 0.0f;
    }

    void Update()
    {
        AnimateDropdown();
        CheckArrowHeight();
        CheckMaxNumber();
        TimedScreenResize();
    }

    void OnGUI()
    {
        // Sets the style of the drop down menu
        GUI.skin = dropdownSkin;

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        // Draws the dropdown list
        DrawDropDownList();

        // When clicked, the menu will move toward the end of the list
        if (GUI.Button(new Rect(scaledResolutionWidth - buttonSize.x - 25, arrowsYPos, buttonSize.x / 2, buttonSize.y), " ", "Down"))
        {
            if (startList + maxDropdownSize < UserContainer.Instance.Users.Count)
            {
                startList++;
            }
        }

        // When clicked, the menu will move up towards the beginning of the list
        if (GUI.Button(new Rect(scaledResolutionWidth - buttonSize.x / 2 - 25, arrowsYPos, buttonSize.x / 2, buttonSize.y), " ", "Up"))
        {
            if (startList - 1 >= 0)
            {
                startList--;
            }
        }

    }

    private void DrawDropDownList()
    {
        for (int i = 0; i < maxDropdownSize; i++)
        {
            // Makes sure the field selected is within the range of your list, if not, it will populate as "Empty"
            if (startList + i < UserContainer.Instance.Users.Count)
            {
                // Populates the fields of the menu, up to the maximum dropdown size
                if (GUI.Button(new Rect(scaledResolutionWidth - buttonSize.x - 25, 25 + buttonSize.y * (i + 1) * yPosition, buttonSize.x, buttonSize.y), UserContainer.Instance.Users[startList + i].Name + ", ID: " + UserContainer.Instance.Users[startList + i].ID, "Dropdown"))
                {
                    // This chooses the specific field number 
                    // Put what you want to do based on which field is selected
                    // Example:  Field 6 selected -> User 6 is loaded into the game
                    settings.IDstring = UserContainer.Instance.Users[startList + i].ID.ToString();
                    //settings.user.LoadSpecificUser(UserContainer.Instance.Users[startList + i].ID, 0, 0);
                    disabling = true;
                    timer = speed = 0.0f;
                }
            }
            else
            {
                // If the dropdown menu has more fields than the list you are choosing from, it will populate the empty fields with "Empty"
                GUI.Label(new Rect(scaledResolutionWidth - buttonSize.x - 25, 25 + buttonSize.y * (i + 1) * yPosition, buttonSize.x, buttonSize.y), "Empty", "Dropdown");
            }
        }
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }

    private void CheckArrowHeight()
    {
        // Makes sure the arrows are still on screen, otherwise, draw them at the bottom of the screen
        if (25 + buttonSize.y * (maxDropdownSize + 1) * yPosition > nativeVerticalResolution)
        {
            arrowsYPos = nativeVerticalResolution - buttonSize.y;
        }
        else
        {
            arrowsYPos = 25 + buttonSize.y * (maxDropdownSize + 1) * yPosition;
        }
    }

    private void AnimateDropdown()
    {
        timer += Time.deltaTime;

        // If the dropdown is closing, animate up
        if (disabling)
        {
            if (yPosition != 0.0f)
            {
                yPosition = Mathf.Lerp(1.0f, 0.0f, speed);
            }
            else
            {
                this.enabled = false;
            }
        }
        // If the dropdown is opening, animate down
        else
        {
            yPosition = Mathf.Lerp(0.0f, 1.0f, speed);

        }

        // Animates the dropdown menu based upon the duration in seconds
        if (speed < 1.0f)
        {
            speed += Time.deltaTime / animationDuration;
        }
    }

    // Makes sure the menu fields don't exceed the screen bounds
    private void CheckMaxNumber()
    {
        if (25 + buttonSize.y * (maxDropdownSize + 1) + buttonSize.y > nativeVerticalResolution)
        {
            maxDropdownSize--;
            CheckMaxNumber();
        }
    }
}
