using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using System.IO;

public class Calibration {

    private Matrix4x4 transform;
    private float timer;
    private Process calibration;
    public Calibration()
    {
        transform = new Matrix4x4();
        timer = 0.0f;
    }
    public bool Calibrate()
    {
        return Calibrate(Application.dataPath + @"/../Calibration/" + "ChessBoardWCS.exe");
    }
    public bool Calibrate(string fileName)
    {
        try
        {
            calibration = new Process();
            //calibration.StartInfo.UseShellExecute = false;
            calibration.StartInfo.FileName = fileName;
            //calibration.StartInfo.CreateNoWindow = true;
            calibration.Start();
            calibration.ErrorDataReceived += calibration_ErrorDataReceived;
            calibration.Disposed += calibration_Disposed;
            calibration.Exited += calibration_Exited;
            calibration.OutputDataReceived += calibration_OutputDataReceived;
            UnityEngine.Debug.Log("Calibration Started");
            calibration.WaitForExit(5000);
            calibration.CloseMainWindow();
            calibration.Dispose();
            return true;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("Calibration Error: " + e.Message);
            return false;
        }
    }

    void calibration_ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Calibration Error Data");
        GameControl.Instance.Calibrated();
    }

    void calibration_Disposed(object sender, EventArgs e)
    {
        UnityEngine.Debug.Log("Calibration Disposed");
        GameControl.Instance.Calibrated();
    }

    void calibration_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Calibration Data Received");
        GameControl.Instance.Calibrated();
    }

    void calibration_Exited(object sender, EventArgs e)
    {
        UnityEngine.Debug.Log("Calibration Exited");
        GameControl.Instance.Calibrated();
    }
   

}
