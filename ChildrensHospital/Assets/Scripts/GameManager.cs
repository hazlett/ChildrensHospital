using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private float timer;
    private string message;
    private float lowerLeftVolume, lowerRightVolume, middleLeftVolume, middleRightVolume, upperLeftVolume, upperRightVolume, yRightValue, yLeftValue;
    public MonoBehaviour rightHand, leftHand;
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
        CalculateVolumes();
	}
    void OnGUI()
    {
        GUILayout.Box(message);
        GUILayout.Box("LowerLeftVolume: " + lowerLeftVolume);
        GUILayout.Box("MiddleLeftVolume: " + middleLeftVolume);
        GUILayout.Box("UpperLeftVolume: " + upperLeftVolume);
        GUILayout.Box("LowerRightVolume: " + lowerRightVolume);
        GUILayout.Box("MiddleRightVolume: " + middleRightVolume);
        GUILayout.Box("UpperRightVolume: " + upperRightVolume);
        GUILayout.Box("Left Y Value: " + yLeftValue);
        GUILayout.Box("Right Y Value: " + yRightValue);
    }
    private void StartGame()
    {
        rightHand.enabled = true;
        leftHand.enabled = true;
    }
    private void CalculateVolumes()
    {
        lowerRightVolume = ((RightHandBehaviour)rightHand).LowerVolume;
        middleRightVolume = ((RightHandBehaviour)rightHand).MiddleVolume;
        upperRightVolume = ((RightHandBehaviour)rightHand).UpperVolume;
        lowerLeftVolume = Mathf.Abs(((LeftHandBehaviour)leftHand).LowerVolume);
        middleLeftVolume = Mathf.Abs(((LeftHandBehaviour)leftHand).MiddleVolume);
        upperLeftVolume = Mathf.Abs(((LeftHandBehaviour)leftHand).UpperVolume);
        yLeftValue = ((LeftHandBehaviour)leftHand).TopVolume;
        yRightValue = ((RightHandBehaviour)rightHand).TopVolume;
    }
}
