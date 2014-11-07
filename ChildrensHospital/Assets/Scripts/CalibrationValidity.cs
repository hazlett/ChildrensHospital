using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CalibrationValidity {

    private static CalibrationValidity instance = new CalibrationValidity();
    public static CalibrationValidity Instance { get { return instance; } }

    private string[] errors = new string[] {
        "No validity check called for the calibration matrix",
        "No calibration matrix validity error",
        "Calibration box orientation error",
        "Unknown error",
        "Unknown error",
        "Rotate box around x axis. Try tilting the kinect sensor if the box is upright",
        "Rotate box around y axis",
        "Rotate box around z axis",
        "This error message should never happen. Contact developers with an out of bounds exception in calibration validity"
    };
    private int errorCode;

    private CalibrationValidity()
    {
        errorCode = 0;
    }
    
    public bool CheckValidity(Matrix4x4 matrix)
    {
        float xAxis, yAxis, zAxis;
        yAxis = Mathf.Asin(matrix.m20);
        if (matrix.m20 >= 1)
        {
            errorCode = 4;
            EventLogger.Instance.LogData("Transform Matrix Error: " + errors[errorCode] + "  :  " + MatrixToString(matrix));    
            return false;
        }
        xAxis = Mathf.Asin((-matrix.m21) / (Mathf.Sqrt(1 - Mathf.Pow(matrix.m20, 2))));
        zAxis = Mathf.Acos((-matrix.m00) / (Mathf.Sqrt(1 - Mathf.Pow(matrix.m20, 2))));
        EventLogger.Instance.LogData("X_AXIS: " + xAxis + " : " + Mathf.Rad2Deg * xAxis);
        EventLogger.Instance.LogData("Y_AXIS: " + yAxis + " : " + Mathf.Rad2Deg * yAxis);
        EventLogger.Instance.LogData("Z_AXIS: " + zAxis + " : " + Mathf.Rad2Deg * zAxis);
        EventLogger.Instance.LogData("X: " + matrix.m03);
        EventLogger.Instance.LogData("Y: " + matrix.m13);
        EventLogger.Instance.LogData("Z: " + matrix.m23);
        
        if (matrix.m33 != 1)
        {
            errorCode = 2;
            EventLogger.Instance.LogData("Transform Matrix Error: " + errors[errorCode] + "  :  " + MatrixToString(matrix));
            return false;
        }
        if (!Approx(xAxis * Mathf.Rad2Deg, 0.0f, 45.0f))
        {
            errorCode = 5;
            EventLogger.Instance.LogData("Transform Matrix Error: " + errors[errorCode] + "  :  " + MatrixToString(matrix));
            return false;
        }
        if (!Approx(yAxis * Mathf.Rad2Deg, 0.0f, 45.0f))
        {
            errorCode = 6;
            EventLogger.Instance.LogData("Transform Matrix Error: " + errors[errorCode] + "  :  " + MatrixToString(matrix));
            return false;
        }
        if (!Approx(zAxis * Mathf.Rad2Deg, 180.0f, 45.0f))
        {       
            errorCode = 7;
            EventLogger.Instance.LogData("Transform Matrix Error: " + errors[errorCode] + "  :  " + MatrixToString(matrix));
            return false;
        }
        //if (!Approximately(matrix.m00, 0.9f, 1.0f))
        //{
        //    errorCode = 2;
        //    return Error2(matrix);
        //}
        //if (!Approximately(matrix.m11, 0.9f, 1.0f))
        //{
        //    errorCode = 2;
        //    return Error2(matrix);
        //}
        //if (!Approximately(matrix.m22, 0.9f, 1.0f))
        //{
        //    errorCode = 2;
        //    return Error2(matrix);
        //}
        errorCode = 1;
        return true;
    }
    private bool Error2(Matrix4x4 matrix)
    {
        errorCode = 2;
        EventLogger.Instance.LogData("Transform Matrix Error: " + errors[errorCode] + "  :  " + MatrixToString(matrix));
        return false;
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
    public string GetError()
    {
        return Languages.Instance.GetTranslation(errors[errorCode]);
    }
    private bool Approximately(float value, float min, float max)
    {
        return ((value >= min) && (value <= max));
    }
    private bool Approx(float value, float constant, float deviation)
    {
        return ((value >= constant - deviation) && (value <= constant + deviation));
    }
}
