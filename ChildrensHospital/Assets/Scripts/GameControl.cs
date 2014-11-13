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
    private bool isPlaying = false;
    public bool IsPlaying { get { return isPlaying; } set { isPlaying = value; } }
    private int gemsCollected;
    public int GemsCollected { get { return gemsCollected; } }
    private int totalGems;
    public int TotalGems { get { return totalGems; } set { totalGems = value; } }
    internal float totalVolume;
    public bool InGame, Gems;
    public void CollectGem()
    {
        gemsCollected = Mathf.RoundToInt(totalVolume * 243.1187f);
    }
    public void Calibrated()
    {
        isCalibrated = true;
        isCalibrating = false;
        calibrationTime = DateTime.Now;
        EventLogger.Instance.LogData("Calibrated Successfully.");
    }
    public void ResetGemCount()
    {
        gemsCollected = 0;
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
            if (!CalibrationValidity.Instance.CheckValidity(matrixCalibration))
            {
                UnityEngine.Debug.Log("Calibration Error: " + CalibrationValidity.Instance.GetError());
                EventLogger.Instance.LogData("Calibration Error: " + CalibrationValidity.Instance.GetError());
                isCalibrated = false;
                matrixCalibration = Matrix4x4.zero;
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("Error getting transform: " + e.Message);
            EventLogger.Instance.LogData("Error reading calibration matrix: " + e.Message);
            matrixCalibration = Matrix4x4.zero;
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