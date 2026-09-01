using UnityEngine;

namespace Bnny.Scripts.Services.Time
{
    public class TimeService : ITimeService
    {
        public float DeltaTime
        {
            get
            {
                float deltaTime = UnityEngine.Time.deltaTime;
                return deltaTime;
            }
        }
    }
}
