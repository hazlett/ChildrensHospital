using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DataValidity {

    private static DataValidity instance = new DataValidity();
    public static DataValidity Instance { get { return instance; } }
    private float expected;
   
    private DataValidity()
    {

    }

    public string CheckValidity(List<float> volumes)
    {
        Debug.Log("Number of volumes: " + volumes.Count);
        if (volumes.Count == 0)
        {
            //if (Approximately(GameControl.Instance.totalVolume, DocumentManager.Instance.PredictedVolume, 0.25f, true))
            //{
            //   return null;
            //}
            //else
            //{
                //return Languages.Instance.GetTranslation("Your volume is") + " " + ((GameControl.Instance.totalVolume / DocumentManager.Instance.PredictedVolume) * 100).ToString("F1") + "% " + Languages.Instance.GetTranslation("of the predicted volume");
            //}
            if (CheckDeviation())
            {
                return null;
            }
            else
            {
                return Languages.Instance.GetTranslation("Your volume is") + " " + ((GameControl.Instance.totalVolume / expected) * 100).ToString("F1") + "% " + Languages.Instance.GetTranslation("of the expected volume");
            }
        }
        else if (volumes.Count > 0)
        {
            float averageVolume = 0;
            foreach (float volume in volumes)
            {
                averageVolume += volume;
            }
            averageVolume /= volumes.Count;
            string exception = null;
            if (!Approximately(GameControl.Instance.totalVolume, averageVolume, 0.10f, true))
            {
                exception += Languages.Instance.GetTranslation("Your volume is") + " " + ((GameControl.Instance.totalVolume / averageVolume) * 100).ToString("F1") + "% " + Languages.Instance.GetTranslation("of your previous average volumes") + "\n";
            }
            //if (!Approximately(GameControl.Instance.totalVolume, DocumentManager.Instance.PredictedVolume, 0.25f, true))
            //{
            //    exception += Languages.Instance.GetTranslation("Your volume is") + " " + ((GameControl.Instance.totalVolume / DocumentManager.Instance.PredictedVolume) * 100).ToString("F1") + "% " + Languages.Instance.GetTranslation("of the predicted volume");         
            //}
            if (!CheckDeviation())
            {
                exception += Languages.Instance.GetTranslation("Your volume is") + " " + ((GameControl.Instance.totalVolume / expected) * 100).ToString("F1") + "% " + Languages.Instance.GetTranslation("of the expected volume");
            }
            return exception;
        }
        else
        {
            return Languages.Instance.GetTranslation("Unknown error occurred while validating the volume data");
        }
    }
    private bool CheckDeviation()
    {
        switch(UserContainer.Instance.UserDictionary[UserContainer.Instance.currentUser].BrookeScale)
        {
            case 0:
                {
                    expected = 180.6f;
                    return Approximately(GameControl.Instance.totalVolume, 1.806f, 1.5f * 0.281f, false);
                }
            case 1:
                {
                    expected = 133.2f;
                    return Approximately(GameControl.Instance.totalVolume, 1.332f, 1.5f * 0.256f, false);
                }
            case 2:
                {
                    expected = 97.5f;
                    return Approximately(GameControl.Instance.totalVolume, 0.975f, 1.5f * 0.240f, false);
                }
            case 3:
                {
                    expected = 62.4f;
                    return Approximately(GameControl.Instance.totalVolume, 0.624f, 1.5f * 0.184f, false);
                }
            case 4:
                {
                    expected = 31.0f;
                    return Approximately(GameControl.Instance.totalVolume, 0.310f, 1.5f * 0.067f, false);
                }
            case 5:
                {
                    expected = 10.1f;
                    return Approximately(GameControl.Instance.totalVolume, 0.101f, 1.5f * 0.048f, false);
                }
            case 6:
                {
                    expected = 0.0f;
                    return true;
                }
            default:
                {
                    return false;
                }
        }
    }
    private bool Approximately(float value, float min, float max)
    {
        return ((value >= min) && (value <= max));
    }
    private bool Approximately(float value, float compareValue, float check, bool percentage)
    {
        if (percentage)
        {
            return ((value >= compareValue * (1 - check)) && (value <= compareValue * (1 + check)));
        }
        else
        {
            return ((value >= compareValue - check) && (value <= compareValue + check));
        }
        
    }
}
