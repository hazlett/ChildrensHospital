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
    private int age;
    private float percentVolume, percentArea, predictedVolume, predictedArea;
    public float PredictedVolume { get { return predictedVolume; } }
    public float PredictedSurfaceArea { get { return predictedArea; } }
    public float PercentVolume { get { return percentVolume; } }
    public float PercentArea { get { return percentArea; } }
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
    public void InitializePercents(GameManager gameManager)
    {
        CalculatePercents(gameManager);
    }
    public void CreateArgs()
    {
        if (ArgsCreated)
            return;

        args = "";
        args += UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].ID.ToString() + " ";
        args += UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].UlnaLength.ToString() + " ";
        if (UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Diagnosis == "" || UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Diagnosis == null)
        {
            args += "n/a" + " ";
        }
        else
        {
            args += UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Diagnosis.ToString() + " ";
        }
        args += DateTime.Today.ToString("dd-MM-yyyy") + " ";
        args += UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].BrookeScale.ToString() + " ";
        DateTime currentDate = DateTime.Today;
        age = currentDate.Year - UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Birthdate.Year;
        if (UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Birthdate > currentDate.AddYears(-age))
        {
            age--;
        }
        args += age.ToString() + " ";
        args += GameControl.Instance.CalibrationTime.ToString("dd-MM-yyyyThh:mm:ss") + " ";
        ArgsCreated = true;
    }

    public void AppendArgs(GameManager gameManager)
    {

        CalculatePercents(gameManager);

        args += UserContainer.Instance.time.ToString() + " ";
        args += gameManager.Tracker.GetVolumes().TotalVolume().ToString("G8") + " ";
        args += percentVolume.ToString("G8") + " ";
        args += gameManager.Tracker.GetSurfaceArea().ToString("G8") + " ";
        args += percentArea.ToString("G8") + " ";
        args += gameManager.Tracker.TrunkLeft + " ";
        args += gameManager.Tracker.TrunkForward + " ";
        args += gameManager.Tracker.TrunkRight + " ";

        //args += "TrialTime" + " ";
        //args += "TotalVolume" + " ";
        //args += "VolumePredicted" + " ";
        //args += "TotalSurfaceArea" + " ";
        //args += "AreaPredicted" + " ";
    }

    private void CalculatePercents(GameManager gameManager) {
        
        float height;

        if (UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].Gender)
        {
            height = 28.003f + (4.605f * UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].UlnaLength);
            if (age < 18)
            {
                height += 1.308f * age;
            }
            else
            {
                height += 1.308f * 18;
            }
        }
        else
        {
            height = 31.485f + (4.459f * UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].UlnaLength);
            if (age < 18)
            {
                height += 1.315f * age;
            }
            else
            {
                height += 1.315f * 18;
            }
        }

        predictedVolume = height * height * height * 0.21586025f;
        predictedArea = (height * height * 0.5585f) + (2 * height * 0.5585f * 0.3865f * height);

        predictedVolume *= 0.000001f;
        predictedArea *= 0.0001f;

        percentVolume = (gameManager.Tracker.GetVolumes().TotalVolume() / predictedVolume) * 100.0f;
        percentArea = (gameManager.Tracker.GetSurfaceArea() / predictedArea) * 100.0f;
    }

}
