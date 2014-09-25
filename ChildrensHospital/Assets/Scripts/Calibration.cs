using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using System.IO;

public class Calibration {

    private Process calibration;
    private bool forcedKill;
    public bool Calibrate()
    {
        return Calibrate(Application.dataPath + @"/../Calibration/" + "ChessBoardWCS.exe");
    }
    public bool Calibrate(string fileName)
    {
        try
        {
            calibration = new Process();
            calibration.StartInfo.FileName = fileName;
            calibration.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            calibration.EnableRaisingEvents = true;
            calibration.Exited += calibration_Exited;
            calibration.Start();

            return true;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Calibration Error: " + e.Message);
            EventLogger.Instance.LogData("Calibration Failed: " + e.Message);
            return false;
        }
    }
    public void Kill()
    {
        try
        {
            forcedKill = true;
            calibration.Kill();
            EventLogger.Instance.LogData("Calibration Restarted");
        }
        catch (Exception) { }
    }
    private void TimedCalibrate()
    {
        UnityEngine.Debug.Log("Timed Calibrate");
        GameControl.Instance.Calibrated();
        GameControl.Instance.ReadCalibration();
        EventLogger.Instance.LogData("Calibration Restarted " + GameControl.Instance.TransformMatrix);
    }

    void calibration_Exited(object sender, EventArgs e)
    {
        if (!forcedKill)
        {
            GameControl.Instance.Calibrated();
        }
        else
        {
            forcedKill = false;
        }
    }
   

}
