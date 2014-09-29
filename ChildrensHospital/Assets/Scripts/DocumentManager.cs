using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class DocumentManager {

    private static DocumentManager instance = new DocumentManager();
    public static DocumentManager Instance { get { return instance; } }
    internal DateTime calibrateTime;
    private Process reportHandler;
    private string processPath;
    public bool ArgsCreated;
    private string args;
    private DocumentManager()
    {
        processPath = Application.dataPath + @"/../Report/" + "ReportHandler.exe";
        ArgsCreated = false;
    }

    public void CalibrationSuccessful()
    {
        calibrateTime = DateTime.Now;
    }
    public void CreateReport()
    {
        try
        {
            try 
            {
                reportHandler.Kill();
            }
            catch (Exception) { }
            reportHandler = new Process();
            reportHandler.StartInfo.FileName = processPath;
            reportHandler.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            reportHandler.StartInfo.Arguments = args;
            reportHandler.Start();      
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Report ERROR: " + e.Message);
            EventLogger.Instance.LogData("ERROR Saving report: " + e.Message);
        }
    }

    public void CreateArgs()
    {
        if (ArgsCreated)
            return;

        args = "";
        args += UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].ID.ToString() + " ";
        args += UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].UlnaLength.ToString() + " ";
        args += UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].BrookeScale.ToString() + " ";
        DateTime currentDate = DateTime.Today;
        int age = currentDate.Year - UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Birthdate.Year;
        if (UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Birthdate > currentDate.AddYears(-age))
        {
            age--;
        }
        args += age.ToString() + " ";
        args += DateTime.Today.ToString("dd-MM-yyyy") + " ";
        args += GameControl.Instance.CalibrationTime.ToString("dd-MM-yyyyThh:mm:ss") + " ";
        ArgsCreated = true;
    }
    public void AppendArgs(GameManager gameManager)
    {
        args += UserContainer.Instance.time.ToString();
        args += gameManager.Tracker.GetVolumes().TotalVolume().ToString("G8") + " ";
        args += "???" + " ";
        args += gameManager.Tracker.totalSurfaceArea.ToString("G8") + " ";
        args += "???" + " ";
    }
}
