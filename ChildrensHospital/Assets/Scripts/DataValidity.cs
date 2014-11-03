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
            if (Approximately(GameControl.Instance.totalVolume, DocumentManager.Instance.PredictedVolume, 0.25f, true))
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
            if (Approximately(GameControl.Instance.totalVolume, averageVolume, 0.10f, true))
            {
                return null;
            }
            else
            {
                return Languages.Instance.GetTranslation("Your volume is") + " " + ((GameControl.Instance.totalVolume / averageVolume) * 100).ToString("F1") + "% " + Languages.Instance.GetTranslation("of your previous average volumes");
            }
        }
        else
        {
            return Languages.Instance.GetTranslation("Unknown error occurred while validating the volume data");
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
            return ((value >= compareValue * (1 - check)) && (value <= value * (1 + check)));
        }
        else
        {
            return ((value >= value - check) && (value <= value + check));
        }
        
    }
}
