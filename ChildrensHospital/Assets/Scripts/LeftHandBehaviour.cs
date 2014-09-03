using UnityEngine;
using System.Collections;

public class LeftHandBehaviour : MonoBehaviour {

    public float LowerYBound, UpperYBound;
    public float lowerXMax, middleXMax, upperXMax, yMax, lowerZMax, middleZMax, upperZMax;
    public GameObject LowerLeft, MiddleLeft, UpperLeft, TopLeft, LowerFarLeft, MiddleFarLeft, UpperFarLeft;
    private float scale;

    void Start()
    {
        scale = 10.0f;
        //ResetMaxes();
    }

    private void ResetMaxes()
    {
        lowerXMax = -0.10f;
        middleXMax = -0.10f;
        upperXMax = -0.10f;
        lowerZMax = 0.10f;
        middleZMax = 0.10f;
        upperZMax = 0.10f;
        yMax = 1.5f;
    }

    void Update()
    {
        if (gameObject.transform.position.y < LowerYBound)
        {
            if (gameObject.transform.position.z > lowerZMax)
            {
                lowerZMax = gameObject.transform.position.z;
                LowerLeft.transform.position = new Vector3(LowerLeft.transform.position.x, LowerLeft.transform.position.y, scale * scale * gameObject.transform.position.z * gameObject.transform.position.z);
            }
            if (gameObject.transform.position.x < lowerXMax)
            {
                lowerXMax = gameObject.transform.position.x;
                LowerFarLeft.transform.position = new Vector3(scale * scale * -gameObject.transform.position.x * gameObject.transform.position.x, LowerFarLeft.transform.position.y, LowerFarLeft.transform.position.z);
            }
        }
        else if ((gameObject.transform.position.y >= LowerYBound) && (gameObject.transform.position.y < UpperYBound))
        {
            if (gameObject.transform.position.z > middleZMax)
            {
                middleZMax = gameObject.transform.position.z;
                MiddleLeft.transform.position = new Vector3(MiddleLeft.transform.position.x, MiddleLeft.transform.position.y, scale * scale * gameObject.transform.position.z * gameObject.transform.position.z);
            }
            if (gameObject.transform.position.x < middleXMax)
            {
                middleXMax = gameObject.transform.position.x;
                MiddleFarLeft.transform.position = new Vector3(scale * scale * -gameObject.transform.position.x * gameObject.transform.position.x, MiddleFarLeft.transform.position.y, MiddleFarLeft.transform.position.z);
            }
        }
        else if (gameObject.transform.position.y >= UpperYBound)
        {
            if (gameObject.transform.position.z > upperZMax)
            {
                upperZMax = gameObject.transform.position.z;
                UpperLeft.transform.position = new Vector3(UpperLeft.transform.position.x, UpperLeft.transform.position.y, scale * scale * gameObject.transform.position.z * gameObject.transform.position.z);
            }
            if (gameObject.transform.position.x < upperXMax)
            {
                upperXMax = gameObject.transform.position.x;
                UpperFarLeft.transform.position = new Vector3(scale * scale * -gameObject.transform.position.x * gameObject.transform.position.x, UpperFarLeft.transform.position.y, UpperFarLeft.transform.position.z);
            }
        }
        if (gameObject.transform.position.y > yMax)
        {
            yMax = gameObject.transform.position.y;
            TopLeft.transform.position = new Vector3(TopLeft.transform.position.x, gameObject.transform.position.y * gameObject.transform.position.y, TopLeft.transform.position.z);
        }
    }
}
