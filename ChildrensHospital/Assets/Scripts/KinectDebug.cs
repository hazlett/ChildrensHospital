using UnityEngine;
using System.Collections;

public class KinectDebug : MonoBehaviour {

    public KinectManager kinectManager;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}
    void OnGUI()
    {
        Vector3 leftHand, rightHand, leftHandK, rightHandK;
        rightHand = kinectManager.GetJointPosition(kinectManager.GetPrimaryUserID(), 11);
        leftHand = kinectManager.GetJointPosition(kinectManager.GetPrimaryUserID(), 7);
        rightHandK = kinectManager.GetJointKinectPosition(kinectManager.GetPrimaryUserID(), 11);
        leftHandK = kinectManager.GetJointKinectPosition(kinectManager.GetPrimaryUserID(), 7);
        GUILayout.Box("LeftHand: " + leftHand);
        GUILayout.Box("RightHand: " + rightHand);
        GUILayout.Box("LeftHandKinectPos: " + leftHandK);
        GUILayout.Box("RightHandKinectPos: " + rightHandK);
    }
}
