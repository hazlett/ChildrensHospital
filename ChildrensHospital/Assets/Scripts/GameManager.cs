using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private float timer;
    private string message;
    public MonoBehaviour rightHand, leftHand;
    private bool skeletonInitialized;
	void Start () {
        timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (KinectManager.Instance.GetUsersCount() > 0)
        {
            timer += Time.deltaTime;
            if (timer > 1.5)
            {
                message = "PLAYING GAME";
                StartGame();
            }
            else
            {
                message = "COUNTING DOWN";
            }
        }
        else
        {
            message = "SKELETON NOT FOUND";
            timer = 0.0f;
            return;
        }
        
	}
    void OnGUI()
    {
        GUILayout.Box(message);
    }
    private void StartGame()
    {
        rightHand.enabled = true;
        leftHand.enabled = true;
    }
}
