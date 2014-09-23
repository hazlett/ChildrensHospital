using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FocusedCameraControl : MonoBehaviour {

    public Camera mainCamera, shoulderCamera;
    private float timer;
    private float alphaTimer;
    public GameManager gameManager;
    internal bool automating;
    private float relativeRightVolume, relativeLeftVolume;
    private GameObject LowerRight, MiddleRight, UpperRight, TopRight, LowerFarRight, MiddleFarRight, UpperFarRight;
    private GameObject LowerLeft, MiddleLeft, UpperLeft, TopLeft, LowerFarLeft, MiddleFarLeft, UpperFarLeft;
    private Vector3 lookRight = new Vector3(0, 45.0f, 0), lookLeft = new Vector3(0, -45.0f, 0), look;
    private float lerpScale = 50.0f;
    private List<GameObject> objects;
    private bool appear = false;
    private enum LookDirection
    {
        CENTER,
        RIGHT,
        LEFT
    }
    private LookDirection direction;

	void Start () {
        timer = 0;
        objects = new List<GameObject>();
        LowerRight = GameObject.Find("LowerRight");
        MiddleRight = GameObject.Find("MiddleRight");
        UpperRight = GameObject.Find("UpperRight");
        TopRight = GameObject.Find("TopRight");
        LowerFarRight = GameObject.Find("LowerFarRight");
        MiddleFarRight = GameObject.Find("MiddleFarRight");
        UpperFarRight = GameObject.Find("UpperFarRight");
        LowerLeft = GameObject.Find("LowerLeft");
        MiddleLeft = GameObject.Find("MiddleLeft");
        UpperLeft = GameObject.Find("UpperLeft");
        TopLeft = GameObject.Find("TopLeft");
        LowerFarLeft = GameObject.Find("LowerFarLeft");
        MiddleFarLeft = GameObject.Find("MiddleFarLeft");
        UpperFarLeft = GameObject.Find("UpperFarLeft");
        objects.Add(LowerRight);
        objects.Add(MiddleRight);
        objects.Add(UpperRight);
        objects.Add(LowerLeft);
        objects.Add(MiddleLeft);
        objects.Add(UpperLeft);
        objects.Add(TopRight);
        objects.Add(TopLeft);
        objects.Add(LowerFarLeft);
        objects.Add(MiddleFarLeft);
        objects.Add(UpperFarLeft);
        objects.Add(LowerFarRight);
        objects.Add(MiddleFarRight);
        objects.Add(UpperFarRight);

	}
    void OnEnable()
    {
        timer = 0;
    }
	
    private void ResetAlphas()
    {
        foreach (GameObject go in objects)
        {
            Color color = go.renderer.material.color;
            color.a = 0.0f;
            go.renderer.material.color = color;
        }
    }
    private void Blink()
    {
        if (appear)
        {
            if (alphaTimer < 1.5)
                alphaTimer += Time.deltaTime;
            else
            {
                appear = false;
            }
        }
        else
        {
            if (alphaTimer > 0)
                alphaTimer -= Time.deltaTime;
            else
            {
                appear = true;
            }
        }
        Color color;
        switch (direction)
        {
            case LookDirection.LEFT:
                color = LowerFarLeft.renderer.material.color;
                color.a = alphaTimer / 3.0f;
                LowerFarLeft.renderer.material.color = color;
                MiddleFarLeft.renderer.material.color = color;
                UpperFarLeft.renderer.material.color = color;
                break;
            case LookDirection.RIGHT:
                color = LowerFarRight.renderer.material.color;
                color.a = alphaTimer / 4.0f;
                LowerFarRight.renderer.material.color = color;
                MiddleFarRight.renderer.material.color = color;
                UpperFarRight.renderer.material.color = color;
                break;
        }

    }
    void Update () {
        if (gameManager.Playing)
        {
            if (automating)
            {
                timer += Time.deltaTime;
                if (timer > UserContainer.Instance.time * 0.2f)
                {
                    ResetAlphas();
                    timer = 0.0f;
                    alphaTimer = 0.0f;
                    appear = true;
                    relativeRightVolume = ((RightHandBehaviour)gameManager.rightHand).RelativeLowerVolume +
                        ((RightHandBehaviour)gameManager.rightHand).RelativeMiddleVolume +
                        ((RightHandBehaviour)gameManager.rightHand).RelativeUpperVolume;
                    relativeLeftVolume = ((LeftHandBehaviour)gameManager.leftHand).RelativeLowerVolume +
                        ((LeftHandBehaviour)gameManager.leftHand).RelativeMiddleVolume +
                        ((LeftHandBehaviour)gameManager.leftHand).RelativeUpperVolume;
                    if (relativeRightVolume == 0)
                    {
                        shoulderCamera.enabled = true;
                        gameManager.message = "FOCUS ON YOUR RIGHT HAND";
                        look = lookRight;
                        direction = LookDirection.RIGHT;
                    }
                    else if (relativeLeftVolume == 0)
                    {
                        shoulderCamera.enabled = true;
                        gameManager.message = "FOCUS ON YOUR LEFT HAND";
                        look = lookLeft;
                        direction = LookDirection.LEFT;
                    }
                    else if (relativeLeftVolume < relativeRightVolume)
                    {
                        shoulderCamera.enabled = true;
                        gameManager.message = "FOCUS ON YOUR LEFT HAND";
                        look = lookLeft;
                        direction = LookDirection.LEFT;
                    }
                    else
                    {
                        shoulderCamera.enabled = true;
                        gameManager.message = "FOCUS ON YOUR RIGHT HAND";
                        look = lookRight;
                        direction = LookDirection.RIGHT;
                    }
                }
                if (gameManager.timer > UserContainer.Instance.time * 0.75f)
                {
                    automating = false;
                    direction = LookDirection.CENTER;
                }
                shoulderCamera.gameObject.transform.rotation = Quaternion.RotateTowards(shoulderCamera.transform.rotation, Quaternion.Euler(look), Time.deltaTime * lerpScale);
                Blink();
            }
            else
            {
                direction = LookDirection.CENTER;
                ResetAlphas();
                if (gameManager.timer > UserContainer.Instance.time * 0.75f)
                {
                    gameManager.message = "SESSION ENDS SOON";
                    shoulderCamera.enabled = false;
                }
                else if (gameManager.timer > UserContainer.Instance.time * 0.25f)
                {
                    automating = true;
                    timer = 0;
                }
            }
        }
	}
}
