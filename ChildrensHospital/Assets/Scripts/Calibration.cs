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
            calibration.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            calibration.EnableRaisingEvents = true;
            calibration.Exited += calibration_Exited;
            calibration.Start();

            UnityEngine.Debug.Log("Calibration Started");
            return true;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("Calibration Error: " + e.Message);
            return false;
        }
    }
    public void Kill()
    {
        try
        {
            forcedKill = true;
            calibration.Kill();
        }
        catch (Exception) { }
    }
    private void TimedCalibrate()
    {
        UnityEngine.Debug.Log("Timed Calibrate");
        GameControl.Instance.Calibrated();
    }

    void calibration_Exited(object sender, EventArgs e)
    {
        if (!forcedKill)
        {
            UnityEngine.Debug.Log("Calibration Exited");
            GameControl.Instance.Calibrated();
        }
        else
        {
            UnityEngine.Debug.Log("Calibration Forced Kill");
            forcedKill = false;
        }
    }
   

}
