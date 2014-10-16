using UnityEngine;
using System.Collections;

public class CountdownGUI : MonoBehaviour
{

    public Texture2D one, two, three;

    private float verticalResolution = 1080.0f, horizontalResolution, updateGUI = 0.5f;
    private float numberSize = 1.0f, timer = 0.0f;
    private bool started = false;

    void Start()
    {
        horizontalResolution = verticalResolution / Screen.height * Screen.width;
        this.enabled = false;
    }

    void OnEnable()
    {
        started = false;
        timer = 0;
        numberSize = 1.0f;
    }

    void Update()
    {

        timer += Time.deltaTime;

        if (timer < 3.0f)
        {
            numberSize = Mathf.Lerp(numberSize, 0.0f, 0.0075f);
        }

    }

    void OnGUI()
    {

        // Scale the GUI to any resolution based on 1920 x 1080 base resolution
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / verticalResolution, Screen.height / verticalResolution, 1));

        if (!started)
        {
            if (timer < 1.0f)
            {
                GUI.DrawTexture(new Rect(horizontalResolution / 2 - (450 * numberSize), verticalResolution / 2 - (450 * numberSize), 900 * numberSize, 900 * numberSize), three);
            }
            else if (timer < 2.0f)
            {
                GUI.DrawTexture(new Rect(horizontalResolution / 2 - (450 * numberSize), verticalResolution / 2 - (450 * numberSize), 900 * numberSize, 900 * numberSize), two);
            }
            else if (timer < 3.0f)
            {
                GUI.DrawTexture(new Rect(horizontalResolution / 2 - (450 * numberSize), verticalResolution / 2 - (450 * numberSize), 900 * numberSize, 900 * numberSize), one);
            }
        }
    }

    void ScreenResize()
    {
        horizontalResolution = verticalResolution / Screen.height * Screen.width;
    }
}
