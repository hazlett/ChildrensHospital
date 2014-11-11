using UnityEngine;
using System.Collections;

public class LeftHandBehaviour : MonoBehaviour {

    public float LowerYBound, UpperYBound, xOffset, yOffset, zOffset;
    public float lowerXMax, middleXMax, upperXMax, yMax, lowerZMax, middleZMax, upperZMax, lowerHeight, middleHeight, upperHeight;
    private GameObject LowerLeft, MiddleLeft, UpperLeft, TopLeft, LowerFarLeft, MiddleFarLeft, UpperFarLeft;
    private float scale, lerpScale = 0.5f;
    private int brookeScale = 0;
    private float slope;
    private VolumeTracker tracker;

    public AudioSource audioPlay;

    void Start()
    {
        tracker = GameObject.Find("GameManager").GetComponent<GameManager>().Tracker;
        scale = 3.0f;
        LowerLeft = GameObject.Find("LowerLeft");
        MiddleLeft = GameObject.Find("MiddleLeft");
        UpperLeft = GameObject.Find("UpperLeft");
        TopLeft = GameObject.Find("TopLeft");
        LowerFarLeft = GameObject.Find("LowerFarLeft");
        MiddleFarLeft = GameObject.Find("MiddleFarLeft");
        UpperFarLeft = GameObject.Find("UpperFarLeft");
        brookeScale = UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].BrookeScale;
        ScaleFunction();
       // ResetMaxes();
    }

    private void ResetMaxes()
    {
        lowerXMax = -0.10f;
        middleXMax = -0.10f;
        upperXMax = -0.10f;
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
        if (tracker.BoxStateLeft == VolumeTracker.BoxStates.LOWER)
        {
            if (gameObject.transform.position.z > lowerZMax)
            {
                lowerZMax = gameObject.transform.position.z * slope;
                if (!audioPlay.isPlaying)
                {
                    audioPlay.Play();
                }
            }
            if (gameObject.transform.position.x < lowerXMax)
            {
                lowerXMax = gameObject.transform.position.x * slope;
                if (!audioPlay.isPlaying)
                {
                    audioPlay.Play();
                }
            }
        }
        else if (tracker.BoxStateLeft == VolumeTracker.BoxStates.MIDDLE)
        {
            if (gameObject.transform.position.z > middleZMax)
            {
                middleZMax = gameObject.transform.position.z * slope;
                if (!audioPlay.isPlaying)
                {
                    audioPlay.Play();
                }
            }
            if (gameObject.transform.position.x < middleXMax)
            {
                middleXMax = gameObject.transform.position.x * slope;
                if (!audioPlay.isPlaying)
                {
                    audioPlay.Play();
                }
            }
        }
        else if (tracker.BoxStateLeft == VolumeTracker.BoxStates.UPPER)
        {
            if (gameObject.transform.position.z > upperZMax)
            {
                upperZMax = gameObject.transform.position.z * slope;
                if (!audioPlay.isPlaying)
                {
                    audioPlay.Play();
                }
            }
            if (gameObject.transform.position.x < upperXMax)
            {
                upperXMax = gameObject.transform.position.x * slope;
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
        if (upperXMax < middleXMax)
        {
            middleXMax = upperXMax;
        }
        if (middleZMax > lowerZMax)
        {
            lowerZMax = middleZMax;
        }
        if (middleXMax < lowerXMax)
        {
            lowerXMax = middleXMax;
        }
        if (UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].BrookeScale == 5 || UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].BrookeScale == 4)
        {
            upperXMax = middleXMax = lowerXMax;
            upperZMax = middleZMax = lowerZMax;
        }
        LowerLeft.transform.position = Vector3.Lerp(LowerLeft.transform.position, new Vector3(LowerLeft.transform.position.x, LowerLeft.transform.position.y, zOffset + scale * lowerZMax), Time.deltaTime * lerpScale);
        LowerFarLeft.transform.position = Vector3.Lerp(LowerFarLeft.transform.position, new Vector3(xOffset + lowerXMax, LowerFarLeft.transform.position.y, LowerFarLeft.transform.position.z), Time.deltaTime * lerpScale);
        MiddleLeft.transform.position = Vector3.Lerp(MiddleLeft.transform.position, new Vector3(MiddleLeft.transform.position.x, MiddleLeft.transform.position.y, zOffset + scale * middleZMax), Time.deltaTime * lerpScale);
        MiddleFarLeft.transform.position = Vector3.Lerp(MiddleFarLeft.transform.position, new Vector3(xOffset + middleXMax, MiddleFarLeft.transform.position.y, MiddleFarLeft.transform.position.z), Time.deltaTime * lerpScale);
        UpperLeft.transform.position = Vector3.Lerp(UpperLeft.transform.position, new Vector3(UpperLeft.transform.position.x, UpperLeft.transform.position.y, zOffset + scale * upperZMax), Time.deltaTime * lerpScale);
        UpperFarLeft.transform.position = Vector3.Lerp(UpperFarLeft.transform.position, new Vector3(xOffset + upperXMax, UpperFarLeft.transform.position.y, UpperFarLeft.transform.position.z), Time.deltaTime * lerpScale);
        TopLeft.transform.position = Vector3.Lerp(TopLeft.transform.position, new Vector3(TopLeft.transform.position.x, yOffset + yMax * yMax, TopLeft.transform.position.z), Time.deltaTime * lerpScale);
    }
    private void ScaleFunction()
    {
        slope = 1.0f;
        switch (brookeScale)
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
    public float RelativeLowerVolume { get { return Mathf.Abs(lowerXMax * lowerZMax * 1.0f); } }
    public float MiddleVolume
    {
        get { return middleXMax * middleZMax * middleHeight; }
    }
    public float RelativeMiddleVolume { get { return Mathf.Abs(middleXMax * middleZMax * 1.0f); } }
    public float UpperVolume
    {
        get { return upperXMax * upperZMax * upperHeight; }
    }
    public float RelativeUpperVolume { get { return Mathf.Abs(upperXMax * upperZMax * 1.0f); } }
    public float TopVolume
    {
        get { return yMax; }
    }
}
