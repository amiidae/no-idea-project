using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    void Awake()
    {
        IInputService inputService = new InputService();
        ServiceLocator.RegisterService<IInputService>(inputService);

        IPhysics2DService physics2DService = new Physics2DService();
        ServiceLocator.RegisterService<IPhysics2DService>(physics2DService);
    }
}
