using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DataValidity {

    private static DataValidity instance = new DataValidity();
    public static DataValidity Instance { get { return instance; } }
   
    private DataValidity()
    {

    }

    public string CheckValidity(List<float> volumes)
    {
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
                return Languages.Instance.GetTranslation("Your volume is") + " " + ((GameControl.Instance.totalVolume / DocumentManager.Instance.PredictedVolume) * 100).ToString("F1") + "% " + Languages.Instance.GetTranslation("of the predicted volume");
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
                exception += Languages.Instance.GetTranslation("Your volume is") + " " + ((GameControl.Instance.totalVolume / averageVolume) * 100).ToString("F1") + "% " + Languages.Instance.GetTranslation("of your previous average volumes");
            }
            //if (!Approximately(GameControl.Instance.totalVolume, DocumentManager.Instance.PredictedVolume, 0.25f, true))
            //{
            //    exception += Languages.Instance.GetTranslation("Your volume is") + " " + ((GameControl.Instance.totalVolume / DocumentManager.Instance.PredictedVolume) * 100).ToString("F1") + "% " + Languages.Instance.GetTranslation("of the predicted volume");         
            //}
            if (!CheckDeviation())
            {
                exception += Languages.Instance.GetTranslation("Your volume is") + " " + ((GameControl.Instance.totalVolume / DocumentManager.Instance.PredictedVolume) * 100).ToString("F1") + "% " + Languages.Instance.GetTranslation("of the predicted volume");
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
                    return Approximately(GameControl.Instance.totalVolume, 180.6f, 28.1f, false);
                }
            case 1:
                {
                    return Approximately(GameControl.Instance.totalVolume, 133.2f, 25.6f, false);
                }
            case 2:
                {
                    return Approximately(GameControl.Instance.totalVolume, 97.5f, 24.0f, false);
                }
            case 3:
                {
                    return Approximately(GameControl.Instance.totalVolume, 62.4f, 18.4f, false);
                }
            case 4:
                {
                    return Approximately(GameControl.Instance.totalVolume, 31.0f, 6.7f, false);
                }
            case 5:
                {
                    return Approximately(GameControl.Instance.totalVolume, 10.1f, 4.8f, false);
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
