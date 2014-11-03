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
        "Calibration box rotated 180 degrees",
        "Calibration box rotated 90 degrees",
        "Unknown error"

    };
    private int errorCode;

    private CalibrationValidity()
    {
        errorCode = 0;
    }
    
    public bool CheckValidity(Matrix4x4 matrix)
    {
        Debug.Log("Transformation Matrix: " + MatrixToString(matrix));
        if (matrix.m33 != 1)
        {
            errorCode = 5;
            return false;
        }
        if (!Approximately(matrix.m00, 0.9f, 1.0f))
        {
            errorCode = 2;
            return Error2(matrix);
        }
        if (!Approximately(matrix.m11, 0.9f, 1.0f))
        {
            errorCode = 2;
            return Error2(matrix);
        }
        if (!Approximately(matrix.m22, 0.9f, 1.0f))
        {
            errorCode = 2;
            return Error2(matrix);
        }
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
}
