using UnityEngine;

namespace Bnny.Scripts.Services.Time
{
    public interface ITimeService
    {
        public float DeltaTime { get; }
    }
}
