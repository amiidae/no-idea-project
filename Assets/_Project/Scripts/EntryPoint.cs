using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    void Awake()
    {
        IInputService inputService = new InputService();
        ServiceLocator.RegisterService<IInputService>(inputService);

        ITimeService timeService = new TimeService();
        ServiceLocator.RegisterService<ITimeService>(timeService);
    }
}
