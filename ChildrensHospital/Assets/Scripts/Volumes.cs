using UnityEngine;
using System.Collections;

public class Volumes {

    private float lowerLeftVolume, lowerRightVolume, middleLeftVolume, middleRightVolume, upperLeftVolume, upperRightVolume, yRightValue, yLeftValue;
    public float LowerLeftVolume { get { return lowerLeftVolume; } }
    public float LowerRightVolume { get { return lowerRightVolume; } }
    public float MiddleLeftVolume { get { return middleLeftVolume; } }
    public float MiddleRightVolume { get { return middleRightVolume; } }
    public float UpperLeftVolume { get { return upperLeftVolume; } }
    public float UpperRightVolume { get { return upperRightVolume; } }
    public float YLeftValue { get { return yLeftValue; } }
    public float YRightValue { get { return yRightValue; } }
    public Volumes (float lowerLeftVolume, float lowerRightVolume, float middleLeftVolume, float middleRightVolume, float upperLeftVolume, float upperRightVolume, float yRightValue, float yLeftValue)
    {
        SetVolumes(lowerLeftVolume, lowerRightVolume, middleLeftVolume, middleRightVolume, upperLeftVolume, upperRightVolume, yLeftValue, yRightValue);
    }
    public Volumes(float lowerLeftVolume, float lowerRightVolume, float middleLeftVolume, float middleRightVolume, float upperLeftVolume, float upperRightVolume)
    {
        SetVolumes(lowerLeftVolume, lowerRightVolume, middleLeftVolume, middleRightVolume, upperLeftVolume, upperRightVolume);
    }
    public Volumes() { }

    public void SetVolumes(float lowerLeftVolume, float lowerRightVolume, float middleLeftVolume, float middleRightVolume, float upperLeftVolume, float upperRightVolume)
    {
        this.lowerLeftVolume = lowerLeftVolume;
        this.lowerRightVolume = lowerRightVolume;
        this.middleLeftVolume = middleLeftVolume;
        this.middleRightVolume = middleRightVolume;
        this.upperLeftVolume = upperLeftVolume;
        this.upperRightVolume = upperRightVolume;
    }
    public void SetVolumes(float lowerLeftVolume, float lowerRightVolume, float middleLeftVolume, float middleRightVolume, float upperLeftVolume, float upperRightVolume, float yRightValue, float yLeftValue)
    {
        SetVolumes(lowerLeftVolume, lowerRightVolume, middleLeftVolume, middleRightVolume, upperLeftVolume, upperRightVolume);
        this.yLeftValue = yLeftValue;
        this.yRightValue = yRightValue;
    }

}
