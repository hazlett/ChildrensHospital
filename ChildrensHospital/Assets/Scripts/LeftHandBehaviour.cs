using UnityEngine;
using System.Collections;

public class LeftHandBehaviour : MonoBehaviour {

    public float LowerYBound, UpperYBound, xOffset, yOffset, zOffset;
    public float lowerXMax, middleXMax, upperXMax, yMax, lowerZMax, middleZMax, upperZMax, lowerHeight, middleHeight, upperHeight;
    public GameObject LowerLeft, MiddleLeft, UpperLeft, TopLeft, LowerFarLeft, MiddleFarLeft, UpperFarLeft;
    private float scale, lerpScale = 0.5f;

    void Start()
    {
        scale = 3.0f;
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
        if (gameObject.transform.position.y < LowerYBound)
        {
            if (gameObject.transform.position.z > lowerZMax)
            {
                lowerZMax = gameObject.transform.position.z;
            }
            if (gameObject.transform.position.x < lowerXMax)
            {
                lowerXMax = gameObject.transform.position.x;
            }
        }
        else if ((gameObject.transform.position.y >= LowerYBound) && (gameObject.transform.position.y < UpperYBound))
        {
            if (gameObject.transform.position.z > middleZMax)
            {
                middleZMax = gameObject.transform.position.z;
            }
            if (gameObject.transform.position.x < middleXMax)
            {
                middleXMax = gameObject.transform.position.x;
            }
        }
        else if (gameObject.transform.position.y >= UpperYBound)
        {
            if (gameObject.transform.position.z > upperZMax)
            {
                upperZMax = gameObject.transform.position.z;
            }
            if (gameObject.transform.position.x < upperXMax)
            {
                upperXMax = gameObject.transform.position.x;
            }
        }
        if (gameObject.transform.position.y > yMax)
        {
            yMax = gameObject.transform.position.y;
        }
        LowerLeft.transform.position = Vector3.Lerp(LowerLeft.transform.position, new Vector3(LowerLeft.transform.position.x, LowerLeft.transform.position.y, zOffset + scale * lowerZMax), Time.deltaTime * lerpScale);
        LowerFarLeft.transform.position = Vector3.Lerp(LowerFarLeft.transform.position, new Vector3(xOffset + lowerXMax, LowerFarLeft.transform.position.y, LowerFarLeft.transform.position.z), Time.deltaTime * lerpScale);
        MiddleLeft.transform.position = Vector3.Lerp(MiddleLeft.transform.position, new Vector3(MiddleLeft.transform.position.x, MiddleLeft.transform.position.y, zOffset + scale * middleZMax), Time.deltaTime * lerpScale);
        MiddleFarLeft.transform.position = Vector3.Lerp(MiddleFarLeft.transform.position, new Vector3(xOffset + middleXMax, MiddleFarLeft.transform.position.y, MiddleFarLeft.transform.position.z), Time.deltaTime * lerpScale);
        UpperLeft.transform.position = Vector3.Lerp(UpperLeft.transform.position, new Vector3(UpperLeft.transform.position.x, UpperLeft.transform.position.y, zOffset + scale * upperZMax), Time.deltaTime * lerpScale);
        UpperFarLeft.transform.position = Vector3.Lerp(UpperFarLeft.transform.position, new Vector3(xOffset + upperXMax, UpperFarLeft.transform.position.y, UpperFarLeft.transform.position.z), Time.deltaTime * lerpScale);
        TopLeft.transform.position = Vector3.Lerp(TopLeft.transform.position, new Vector3(TopLeft.transform.position.x, yOffset + yMax * yMax, TopLeft.transform.position.z), Time.deltaTime * lerpScale);
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
