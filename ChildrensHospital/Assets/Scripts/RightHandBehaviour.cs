﻿using UnityEngine;
using System.Collections;

public class RightHandBehaviour : MonoBehaviour {

    public float LowerYBound, UpperYBound, xOffset, yOffset, zOffset;
    public float lowerXMax, middleXMax, upperXMax, yMax, lowerZMax, middleZMax, upperZMax, lowerHeight, middleHeight, upperHeight;
    private GameObject LowerRight, MiddleRight, UpperRight, TopRight, LowerFarRight, MiddleFarRight, UpperFarRight;
    private float scale, lerpScale = 0.5f;
    private VolumeTracker tracker;
    private int brookeScale = 0;
    private float slope;
    public AudioSource audioPlay;

    void Start()
    {
        tracker = GameObject.Find("GameManager").GetComponent<GameManager>().Tracker;
        scale = 3.0f;
        LowerRight = GameObject.Find("LowerRight");
        MiddleRight = GameObject.Find("MiddleRight");
        UpperRight = GameObject.Find("UpperRight");
        TopRight = GameObject.Find("TopRight");
        LowerFarRight = GameObject.Find("LowerFarRight");
        MiddleFarRight = GameObject.Find("MiddleFarRight");
        UpperFarRight = GameObject.Find("UpperFarRight");
        UpperFarRight.renderer.material.color = new Color(0,0,0,0);
        brookeScale = UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].BrookeScale;
        slope = 1.0f;
        //ScaleFunction();
       // ResetMaxes();
    }

    private void ResetMaxes()
    {
        lowerXMax = 0.10f;
        middleXMax = 0.10f;
        upperXMax = 0.10f;
        lowerZMax = 0.10f;
        middleZMax = 0.10f;
        upperZMax = 0.10f;
        yMax = 0.10f;
    }

    void Update()
    {
        if (!GameControl.Instance.InGame)
        {
            gameObject.transform.position = Vector3.zero;
            return;
        }
        if (tracker.BoxStateRight == VolumeTracker.BoxStates.LOWER)
        {
            if (gameObject.transform.position.z > lowerZMax)
            {
                lowerZMax = gameObject.transform.position.z;
                if (!audioPlay.isPlaying)
                {
                    audioPlay.Play();
                }
            }
            if (gameObject.transform.position.x > lowerXMax)
            {
                lowerXMax = gameObject.transform.position.x;
                if (!audioPlay.isPlaying)
                {
                    audioPlay.Play();
                } 
            }
        }
        else if (tracker.BoxStateRight == VolumeTracker.BoxStates.MIDDLE)
        {
            if (gameObject.transform.position.z > middleZMax)
            {
                middleZMax = gameObject.transform.position.z;
                if (!audioPlay.isPlaying)
                {
                    audioPlay.Play();
                }
            }
            if (gameObject.transform.position.x > middleXMax)
            {
                middleXMax = gameObject.transform.position.x;
                if (!audioPlay.isPlaying)
                {
                    audioPlay.Play();
                }
            }
        }
        else if (tracker.BoxStateRight == VolumeTracker.BoxStates.UPPER)
        {
            if (gameObject.transform.position.z > upperZMax)
            {
                upperZMax = gameObject.transform.position.z;
                if (!audioPlay.isPlaying)
                {
                    audioPlay.Play();
                }
            }
            if (gameObject.transform.position.x > upperXMax)
            {
                upperXMax = gameObject.transform.position.x;
                if (!audioPlay.isPlaying)
                {
                    audioPlay.Play();
                }
            } 
        }
        if (gameObject.transform.position.y > yMax)
        {
            yMax = gameObject.transform.position.y;
        }
        if (upperZMax > middleZMax)
        {
            middleZMax = upperZMax;
        }
        if (upperXMax > middleXMax)
        {
            middleXMax = upperXMax;
        }
        if (middleZMax > lowerZMax)
        {
            lowerZMax = middleZMax;
        }
        if (middleXMax > lowerXMax)
        {
            lowerXMax = middleXMax;
        }
        if (UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].BrookeScale == 5 || UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].BrookeScale == 4)
        {
            upperXMax = middleXMax = lowerXMax;
            upperZMax = middleZMax = lowerZMax;
        }
        LowerRight.transform.position = Vector3.Lerp(LowerRight.transform.position, new Vector3(LowerRight.transform.position.x, LowerRight.transform.position.y, zOffset + scale * lowerZMax * slope), Time.deltaTime * lerpScale);
        LowerFarRight.transform.position = Vector3.Lerp(LowerFarRight.transform.position, new Vector3(xOffset + lowerXMax * slope, LowerFarRight.transform.position.y, LowerFarRight.transform.position.z), Time.deltaTime * lerpScale);
        MiddleRight.transform.position = Vector3.Lerp(MiddleRight.transform.position, new Vector3(MiddleRight.transform.position.x, MiddleRight.transform.position.y, zOffset + scale * middleZMax * slope), Time.deltaTime * lerpScale);
        MiddleFarRight.transform.position = Vector3.Lerp(MiddleFarRight.transform.position, new Vector3(xOffset + middleXMax * slope, MiddleFarRight.transform.position.y, MiddleFarRight.transform.position.z), Time.deltaTime * lerpScale);
        UpperRight.transform.position = Vector3.Lerp(UpperRight.transform.position, new Vector3(UpperRight.transform.position.x, UpperRight.transform.position.y, zOffset + scale * upperZMax * slope), Time.deltaTime * lerpScale);
        UpperFarRight.transform.position = Vector3.Lerp(UpperFarRight.transform.position, new Vector3(xOffset + upperXMax * slope, UpperFarRight.transform.position.y, UpperFarRight.transform.position.z), Time.deltaTime * lerpScale);
        TopRight.transform.position = Vector3.Lerp(TopRight.transform.position, new Vector3(TopRight.transform.position.x, yOffset + yMax * yMax, TopRight.transform.position.z), Time.deltaTime * lerpScale);
       
    }
    private void ScaleFunction()
    {
        slope = 1.0f;
        switch(brookeScale)
        {
            case 2:
                {
                     slope = 1.5f;
                }
                break;
            case 3:
                {
                    slope = 2.0f;
                }
                break;
            case 4:
                {
                    slope = 2.5f;
                }
                break;
            case 5:
                {
                    slope = 3.0f;
                }
                break;
        }
    }
    public float LowerVolume
    {
        get { return lowerXMax * lowerZMax * lowerHeight; }
    }
    public float RelativeLowerVolume { get { return lowerXMax * lowerZMax * 1.0f; } }
    public float MiddleVolume
    {
        get { return middleXMax * middleZMax * middleHeight; }
    }
    public float RelativeMiddleVolume { get { return middleXMax * middleZMax * 1.0f; } }
    public float UpperVolume
    {
        get { return upperXMax * upperZMax * upperHeight; }
    }
    public float RelativeUpperVolume { get { return upperXMax * upperZMax * 1.0f; } }
    public float TopVolume
    {
        get { return yMax; }
    }
}
