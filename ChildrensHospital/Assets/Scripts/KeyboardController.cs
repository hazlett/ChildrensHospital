using UnityEngine;
using System.Collections;

public class KeyboardController : MonoBehaviour {

    public PauseGUI pauseGUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pause");
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseGUI.enabled = true;
                pauseGUI.gameManager.GUIon = false;
            }
            else
            {
                Time.timeScale = 1;
                pauseGUI.enabled = false;
                pauseGUI.gameManager.GUIon = true;
            }
        }
    }
}
