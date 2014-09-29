using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

public class GameControl : MonoBehaviour
{

    private static GameControl instance = new GameControl();
    public static GameControl Instance
    {
        get
        {
            return instance;
        }
    }
    private bool isCalibrated = false;
    public bool IsCalibrated { get { return isCalibrated; } set { isCalibrated = value; } }
    private bool isCalibrating = false;
    public bool IsCalibrating { get { return isCalibrating; } set { isCalibrating = value; } }
    internal int score = 0;
    private Matrix4x4 transformMatrix;
    public Matrix4x4 TransformMatrix { get { return transformMatrix; } }
    private int idKey;
    private DateTime calibrationTime;
    public DateTime CalibrationTime { get { return calibrationTime; } }
    public int IdKey { get { return idKey; } }

    public void Calibrated()
    {
        isCalibrated = true;
        isCalibrating = false;
        calibrationTime = DateTime.Now;
        EventLogger.Instance.LogData("Calibrated Successfully.");
    }

    internal Matrix4x4 ReadCalibration(string fileName)
    {
        Matrix4x4 matrixCalibration = new Matrix4x4();
        try
        {
            using (StreamReader sr = new StreamReader(Application.dataPath + @"/../transform_matrix.txt"))
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector4 row = new Vector4();
                    String line = sr.ReadLine();
                    String[] tokens = line.Split();
                    int j = 0;
                    foreach (string token in tokens)
                    {
                        float matrixCoord = Single.Parse(token);
                        row[j++] = matrixCoord;
                    }
                    matrixCalibration.SetRow(i, row);
                }
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("Error getting transform: " + e.Message);
            EventLogger.Instance.LogData("Error reading calibration matrix: " + e.Message);
        }
        transformMatrix = matrixCalibration;
        EventLogger.Instance.LogData("Calibration Matrix: " + transformMatrix.ToString());
        return matrixCalibration;
    }
    internal Matrix4x4 ReadCalibration()
    {
        return ReadCalibration(Application.dataPath + @"/../transform_matrix.txt");
    }
}