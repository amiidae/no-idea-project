using System;
using UnityEngine;

// Question:
// hidden dependency by passing information in the event?
public class HeroProgressManager : MonoBehaviour, IProgressReader, IProgressWriter
{
    // public event Action ProgressSaved;
    public event Action<Vector3> ProgressLoaded;

    // the line is drawn on the handling of ProgressData class

    public void SaveProgress(ProgressData progressData)
    {
        progressData.HeroProgressData.Position = gameObject.transform.position.ToVector3Data();

        // ProgressSaved.Invoke();
    }

    public void LoadProgress(ProgressData progressData)
    {
        Vector3 coordinates = progressData.HeroProgressData.Position.ToUnityVector3();

        ProgressLoaded.Invoke(coordinates);
    }
}
