using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class VolumeTracker {

    private KinectManager kinect;
    public bool DebugMode;
    public enum BoxStates
    {
        LOWER,
        MIDDLE,
        UPPER
    };
    private BoxStates boxStateLeft, boxStateRight;
    public BoxStates BoxStateLeft { get {return boxStateLeft; } }
    public BoxStates BoxStateRight { get { return boxStateRight; } }
    private float lowerXLeft, lowerXRight, middleXLeft, middleXRight, upperXLeft, upperXRight,
        lowerZLeft, lowerZRight, middleZLeft, middleZRight, upperZLeft, upperZRight,
        yLeft, yRight, trunkLeft, trunkRight, trunkForward;
    internal float lowerFarLeft, middleFarLeft, upperFarLeft, lowerFrontLeft, middleFrontLeft, upperFrontLeft, topLeft,
        lowerFarRight, middleFarRight, upperFarRight, lowerFrontRight, middleFrontRight, upperFrontRight, topRight,
        totalSurfaceArea;
    private float lowerBound = 0.1f, upperBound = 0.3f;
    private Matrix4x4 transformMatrix; 
    private int leftHandJoint = 7, rightHandJoint = 11, trunkJoint = 20;
    private Vector3 leftHandPosition, rightHandPosition, trunkPosition;
    private Vector3 MeterToCentimeter = new Vector3(100.0f, 100.0f, 100.0f);
    private Volumes volumes;
    private Vector3 offset;
    private Vector3 rightHandAxis = new Vector3(1.0f, 1.0f, -1.0f);
    private Vector3 leftHandAxis = new Vector3(-1.0f, 1.0f, -1.0f);
    private string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ACTIVE-seated";
    private Vector3 trunkAxis = new Vector3(1.0f, 1.0f, -1.0f);
    public string TrunkLeft { get { return (trunkLeft * -1.0f).ToString("F2"); } }
    public string TrunkRight { get { return trunkRight.ToString("F2"); } }
    public string TrunkForward { get { return trunkForward.ToString("F2"); } }
	public VolumeTracker (float ulnaLength) {
        kinect = GameObject.Find("KinectManager").GetComponent<KinectManager>();
        transformMatrix = GameControl.Instance.TransformMatrix;
        volumes = new Volumes();
        offset = new Vector3(0.0f, -0.01f, 0.07f);
        if (ulnaLength > 0)
        {
            upperBound = ulnaLength / 100.0f;
            lowerBound = upperBound / 3.0f;
        }
	}

    private void Initialize()
    {
        SetPositions();
        TranslatePositions();
        InitializeLeft();
        InitializeRight();
    }
    private void InitializeRight()
    {
        lowerXRight = middleXRight = upperXRight = rightHandPosition.x;
        lowerZRight = middleZRight = upperZRight = rightHandPosition.z;
        yRight = rightHandPosition.y;
    }
    private void InitializeLeft()
    {
        lowerXLeft = middleXLeft = upperXLeft = leftHandPosition.x;
        lowerZLeft = middleZLeft = upperZLeft = leftHandPosition.z;
        yLeft = leftHandPosition.y;
    }
	public void Update () {
        SetPositions();
        TranslatePositions();

        if (GameControl.Instance.IsPlaying)
        {
            TrackLeft();
            TrackRight();
            TrackTrunk();
        }
        if (DebugMode)
            DebugPositions();
	}
    private BoxStates FindBoxState(float yPosition)
    {
        if (yPosition < lowerBound)
            return BoxStates.LOWER;
        else if (yPosition < upperBound)
            return BoxStates.MIDDLE;
        else
            return BoxStates.UPPER;
    }
    public void DebugExtremas()
    {
        CalculateSurfaceAreas();
        List<string> lines = new List<string>();
        try
        {
            StreamReader reader = new StreamReader(File.OpenRead(path + @"\Extremas.csv"));
            while (!reader.EndOfStream)
            {
               string line = reader.ReadLine();
               lines.Add(line);
            }
            reader.Close();
        }
        catch (Exception)
        {

        }
    
        // Create a ReachVolumeGame folder if it doesn't exist
        if(!System.IO.Directory.Exists(path)){
            System.IO.Directory.CreateDirectory(path);
        }
        using (StreamWriter file = new StreamWriter(path + @"\Extremas.csv"))
        {
            if (lines.Count == 0)
            {
                file.WriteLine("User Name, User ID, Trial Date and Time, Birthdate, BrookeScale, UlnaLength,"  
                + "TotalVolume, LowerLeftVolume, LowerRightVolume, MiddleLeftVolume, MiddleRightVolume, UpperLeftVolume, UpperRightVolume,"
                + "TotalSurfaceArea, LowerFarLeft, LowerFarRight, MiddleFarLeft, MiddleFarRight, UpperFarLeft, UpperFarRight, LowerFrontLeft, LowerFrontRight, MiddleFrontLeft, MiddleFrontRight, UpperFrontLeft, UpperFrontRight,"
                + "LowerXLeft, LowerXRight, MiddleXLeft, MiddleXRight, UpperXLeft, UpperXRight, LowerZLeft, LowerZRight, MiddleZLeft, MiddleZRight, UpperZLeft, UpperZRight,"
                + "LowerY, MiddleY, LeftYReach, RightYReach, TrunkLeft, TrunkRight, TrunkForward, PercentPredictedVolume, PercentPredictedSurfaceArea, CalibrationMatrix, XOffset, YOffset, ZOffset");
            }
            foreach(string line in lines)
            {
                file.WriteLine(line);
            }
            file.WriteLine(UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Name + "," +
                UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].ID.ToString() + "," +
                DateTime.Now.ToString("g") + "," + 
                UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Birthdate.ToString("MM/dd/yy") + "," +
                UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].BrookeScale + "," +
                UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].UlnaLength + "," +
                volumes.TotalVolume().ToString("G8") + ","
                + LowerLeftVolume().ToString("G8") + "," + LowerRightVolume().ToString("G8") + ","
                + MiddleLeftVolume().ToString("G8") + "," + MiddleRightVolume().ToString("G8") + ","
                + UpperLeftVolume().ToString("G8") + "," + UpperRightVolume().ToString("G8") + ","
                + totalSurfaceArea.ToString("G8") + ","
                + lowerFarLeft.ToString("G8") + "," + lowerFarRight.ToString("G8") + ","
                + middleFarLeft.ToString("G8") + "," + middleFarRight.ToString("G8") + ","
                + upperFarLeft.ToString("G8") + "," + upperFarRight.ToString("G8") + ","
                + lowerFrontLeft.ToString("G8") + "," + lowerFrontRight.ToString("G8") + ","
                + middleFrontLeft.ToString("G8") + "," + middleFrontRight.ToString("G8") + ","
                + upperFrontLeft.ToString("G8") + "," + upperFrontRight.ToString("G8") + ","
               // + topLeft.ToString("G8") + "," + topRight.ToString("G8") + ","
                + lowerXLeft.ToString("G8") + "," + lowerXRight.ToString("G8") + "," 
                + middleXLeft.ToString("G8") + "," + middleXRight.ToString("G8") + "," 
                + upperXLeft.ToString("G8") + "," + upperXRight.ToString("G8") + "," 
                + lowerZLeft.ToString("G8") + "," + lowerZRight.ToString("G8") + "," 
                + middleZLeft.ToString("G8") + "," + middleZRight.ToString("G8") + "," 
                + upperZLeft.ToString("G8") + "," + upperZRight.ToString("G8") + "," 
                + lowerBound.ToString("G2") + "," + (upperBound - lowerBound).ToString("G2") + ","  
                + yLeft.ToString("G8") + "," + yRight.ToString("G8") + ","
                + TrunkLeft + "," + TrunkRight + "," + TrunkForward + ","
                + DocumentManager.Instance.PercentVolume.ToString("G4") + "," + DocumentManager.Instance.PercentArea.ToString("G4") + ","
                + MatrixToString(transformMatrix) + ","
                + offset.x.ToString("G4") + "," + offset.y.ToString("G4") + "," + offset.z.ToString("G4"));
        }

    }

    private void CalculateSurfaceAreas()
    {
        lowerFarLeft = lowerBound * lowerZLeft;
        middleFarLeft = (upperBound - lowerBound) * middleZLeft;
        upperFarLeft = (yLeft - upperBound) * upperZLeft;
        lowerFarRight = lowerBound * lowerZRight;
        middleFarRight = (upperBound - lowerBound) * middleZRight;
        upperFarRight = (yRight - upperBound) * upperZRight;

        lowerFrontLeft = lowerBound * lowerXLeft;
        middleFrontLeft = (upperBound - lowerBound) * middleXLeft;
        upperFrontLeft = (yLeft - upperBound) * upperXLeft;
        lowerFrontRight = lowerBound * lowerXRight;
        middleFrontRight = (upperBound - lowerBound) * middleXRight;
        upperFrontRight = (yRight - upperBound) * upperXRight;

       // topLeft = upperZLeft * upperXLeft;
       // topRight = upperZRight * upperXRight;

        totalSurfaceArea = lowerFarLeft + middleFarLeft + upperFarLeft
            + lowerFarRight + middleFarRight + upperFarRight
            + lowerFrontLeft + middleFrontLeft + upperFrontLeft
            + lowerFrontRight + middleFrontRight + upperFrontRight;
           // + topLeft + topRight;

        
    }
    private string MatrixToString(Matrix4x4 matrix)
    {
        string matrixString = "";
        for (int x = 0; x < 4; x++)
        {
            matrixString += "[ ";
            for (int y = 0; y < 4; y++)
            {
                matrixString += matrix[x, y] + " ";
            }
            matrixString += "] ";
        }
            return matrixString;
    }
    private void DebugPositions()
    {
        Debug.Log("leftHand: " + leftHandPosition);
        Debug.Log("rightHand: " + rightHandPosition);
        Debug.Log("trunk: " + trunkPosition);
    }
    private void SetPositions()
    {
        leftHandPosition = kinect.GetJointKinectPosition(kinect.GetPrimaryUserID(), leftHandJoint);
        rightHandPosition = kinect.GetJointKinectPosition(kinect.GetPrimaryUserID(), rightHandJoint);
        trunkPosition = kinect.GetJointKinectPosition(kinect.GetPrimaryUserID(), trunkJoint);
    }
    private void TranslatePositions()
    {
        leftHandPosition = transformMatrix.MultiplyPoint3x4(leftHandPosition);
        leftHandPosition -= offset;
        leftHandPosition = Vector3.Scale(leftHandPosition, leftHandAxis);
        rightHandPosition = transformMatrix.MultiplyPoint3x4(rightHandPosition);
        rightHandPosition -= offset;
        rightHandPosition = Vector3.Scale(rightHandPosition, rightHandAxis);
        trunkPosition = transformMatrix.MultiplyPoint3x4(trunkPosition);
        trunkPosition -= offset;
        trunkPosition = Vector3.Scale(trunkPosition, trunkAxis);
    }
    private void TrackTrunk()
    {
        if ((trunkPosition.x > 0) && (trunkPosition.x > trunkRight))
        {
            trunkRight = trunkPosition.x;
        }
        if ((trunkPosition.x < 0) && (trunkPosition.x < trunkLeft))
        {
            trunkLeft = trunkPosition.x;
        }
        if ((trunkPosition.z > trunkForward))
        {
            trunkForward = trunkPosition.z;
        }
    }
    private void TrackRight()
    {
        boxStateRight = FindBoxState(rightHandPosition.y);
        switch (boxStateRight)
        {
            case BoxStates.LOWER:
                if (rightHandPosition.x > lowerXRight)
                {
                    lowerXRight = rightHandPosition.x;
                }
                if (rightHandPosition.z > lowerZRight)
                {
                    lowerZRight = rightHandPosition.z;
                }
                break;
            case BoxStates.MIDDLE:
                if (rightHandPosition.x > middleXRight)
                {
                    middleXRight = rightHandPosition.x;
                }
                if (rightHandPosition.z > middleZRight)
                {
                    middleZRight = rightHandPosition.z;
                }
                break;
            case BoxStates.UPPER:
                if (rightHandPosition.x > upperXRight)
                {
                    upperXRight = rightHandPosition.x;
                }
                if (rightHandPosition.z > upperZRight)
                {
                    upperZRight = rightHandPosition.z;
                }
                break;
        }
        if (upperZRight > middleZRight)
        {
            middleZRight = upperZRight;
        }
        if (upperXRight > middleXRight)
        {
            middleXRight = upperXRight;
        }
        if (middleZRight > lowerZRight)
        {
            lowerZRight = middleZRight;
        }
        if (middleXRight > lowerXRight)
        {
            lowerXRight = middleXRight;
        }
        if (rightHandPosition.y > yRight)
        {
            yRight = rightHandPosition.y;
        }
    }
    private void TrackLeft()
    {
        boxStateLeft = FindBoxState(leftHandPosition.y);
        switch (boxStateLeft)
        {
            case BoxStates.LOWER:
                if (leftHandPosition.x > lowerXLeft)
                {
                    lowerXLeft = leftHandPosition.x;
                }
                if (leftHandPosition.z > lowerZLeft)
                {
                    lowerZLeft = leftHandPosition.z;
                }
                break;
            case BoxStates.MIDDLE:
                if (leftHandPosition.x > middleXLeft)
                {
                    middleXLeft = leftHandPosition.x;
                }
                if (leftHandPosition.z > middleZLeft)
                {
                    middleZLeft = leftHandPosition.z;
                }
                break;
            case BoxStates.UPPER:
                if (leftHandPosition.x > upperXLeft)
                {
                    upperXLeft = leftHandPosition.x;
                }
                if (leftHandPosition.z > upperZLeft)
                {
                    upperZLeft = leftHandPosition.z;
                }
                break;
        }
        if (upperZLeft > middleZLeft)
        {
            middleZLeft = upperZLeft;
        }
        if (upperXLeft > middleXLeft)
        {
            middleXLeft = upperXLeft;
        }
        if (middleZLeft > lowerZLeft)
        {
            lowerZLeft = middleZLeft;
        }
        if (middleXLeft > lowerXLeft)
        {
            lowerXLeft = middleXLeft;
        }
        if (leftHandPosition.y > yLeft)
        {
            yLeft = leftHandPosition.y;
        }
    }
    public float LowerLeftVolume() { return Mathf.Abs(lowerXLeft * lowerZLeft * lowerBound); }
    public float LowerRightVolume() { return Mathf.Abs(lowerXRight * lowerZRight * lowerBound); }
    public float MiddleLeftVolume() { return Mathf.Abs(middleXLeft * middleZLeft * (upperBound - lowerBound)); }
    public float MiddleRightVolume() { return Mathf.Abs(middleXRight * middleZRight * (upperBound - lowerBound)); }
    public float UpperLeftVolume() {
        if (yLeft > upperBound)
            return Mathf.Abs(upperXLeft * upperZLeft * (yLeft - upperBound));
        else
            return 0.0f;
    }
    public float UpperRightVolume() {
        if (yRight > upperBound)
            return Mathf.Abs(upperXRight * upperZRight * (yRight - upperBound));
        else
            return 0.0f;
    }
    public float YLeft { get { return yLeft; } }
    public float YRight { get { return yRight; } }
    public Volumes GetVolumes()
    {
        volumes.SetVolumes(LowerLeftVolume(), LowerRightVolume(), MiddleLeftVolume(), MiddleRightVolume(), UpperLeftVolume(), UpperRightVolume(), YRight, YLeft);
        return volumes;
    }

    public float GetSurfaceArea()
    {
        CalculateSurfaceAreas();
        return totalSurfaceArea;
    }
}
