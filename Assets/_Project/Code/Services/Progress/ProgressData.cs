using System;
using UnityEngine;

public enum ProgressVersion
{
    v1
}

[Serializable]
public struct Vector2Data
{
    public float X;
    public float Y;
}

[Serializable]
public class HeroProgressData
{
    public Vector2Data Position;
    public float FacingX;
}

[Serializable]
public class ProgressData
{
    public ProgressVersion Version;
    public HeroProgressData HeroProgressData;
    public bool NewGame;

    public ProgressData()
    {
        Version = ProgressVersion.v1;
        HeroProgressData = new HeroProgressData();
        NewGame = true;
    }
}
