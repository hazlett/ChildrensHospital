using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class SplashGUI : MonoBehaviour
{
    public GUISkin mainMenuSkin;
    public Texture2D splash;

    private float nativeVerticalResolution, scaledResolutionWidth, updateGUI, timer;

    void Start()
    {
        updateGUI = 0.5f;
        timer = 0.0f;
        nativeVerticalResolution = 1080.0f;
        scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
    }

    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer > 5.0f)
        {
            Application.LoadLevel("MainMenu");
        }
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

        GUI.DrawTexture(new Rect(scaledResolutionWidth / 2 - splash.width / 2, nativeVerticalResolution / 2 - splash.height / 2, splash.width, splash.height), splash);
    }

    private void TimedScreenResize()
    {
        if (Time.time > updateGUI)
        {
            scaledResolutionWidth = nativeVerticalResolution / Screen.height * Screen.width;
        }
    }
}
