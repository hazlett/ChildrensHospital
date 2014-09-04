using UnityEngine;
using System.Collections;

public class RightHandBehaviour : MonoBehaviour {

    public float LowerYBound, UpperYBound, xOffset, yOffset, zOffset;
    public float lowerXMax, middleXMax, upperXMax, yMax, lowerZMax, middleZMax, upperZMax, lowerHeight, middleHeight, upperHeight;
    public GameObject LowerRight, MiddleRight, UpperRight, TopRight, LowerFarRight, MiddleFarRight, UpperFarRight;
    private float scale;

    void Start()
    {
        scale = 10.0f;
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
        if (gameObject.transform.position.y < LowerYBound)
        {
            if (gameObject.transform.position.z > lowerZMax)
            {
                lowerZMax = gameObject.transform.position.z;
                LowerRight.transform.position = new Vector3(LowerRight.transform.position.x, LowerRight.transform.position.y, zOffset + scale * gameObject.transform.position.z);             
            }
            if (gameObject.transform.position.x > lowerXMax)
            {
                lowerXMax = gameObject.transform.position.x;
                LowerFarRight.transform.position = new Vector3(xOffset + gameObject.transform.position.x, LowerFarRight.transform.position.y, LowerFarRight.transform.position.z);
            }
        }
        else if ((gameObject.transform.position.y >= LowerYBound) && (gameObject.transform.position.y < UpperYBound))
        {
            if (gameObject.transform.position.z > middleZMax)
            {
                middleZMax = gameObject.transform.position.z;
                MiddleRight.transform.position = new Vector3(MiddleRight.transform.position.x, MiddleRight.transform.position.y, zOffset + scale * gameObject.transform.position.z);
            }
            if (gameObject.transform.position.x > middleXMax)
            {
                middleXMax = gameObject.transform.position.x;
                MiddleFarRight.transform.position = new Vector3(xOffset + gameObject.transform.position.x, MiddleFarRight.transform.position.y, MiddleFarRight.transform.position.z);
            }
        }
        else if (gameObject.transform.position.y >= UpperYBound)
        {
            if (gameObject.transform.position.z > upperZMax)
            {
                upperZMax = gameObject.transform.position.z;
                UpperRight.transform.position = new Vector3(UpperRight.transform.position.x, UpperRight.transform.position.y, zOffset + scale * gameObject.transform.position.z);
            }
            if (gameObject.transform.position.x > upperXMax)
            {
                upperXMax = gameObject.transform.position.x;
                UpperFarRight.transform.position = new Vector3(xOffset + gameObject.transform.position.x, UpperFarRight.transform.position.y, UpperFarRight.transform.position.z);
            }
        }
        if (gameObject.transform.position.y > yMax)
        {
            yMax = gameObject.transform.position.y;
            TopRight.transform.position = new Vector3(TopRight.transform.position.x, yOffset + gameObject.transform.position.y * gameObject.transform.position.y, TopRight.transform.position.z);
        }
    }
    public float LowerVolume
    {
        get { return lowerXMax * lowerZMax * lowerHeight; }
    }
    public float MiddleVolume
    {
        get { return middleXMax * middleZMax * middleHeight; }
    }
    public float UpperVolume
    {
        get { return upperXMax * upperZMax * upperHeight; }
    }
    public float TopVolume
    {
        get { return yMax; }
    }
}
