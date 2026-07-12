using UnityEngine;

public class TimeService : ITimeService
{
    public float DeltaTime
    {
        get
        {
            float deltaTime = Time.deltaTime;
            return deltaTime;
        }
    }
}
