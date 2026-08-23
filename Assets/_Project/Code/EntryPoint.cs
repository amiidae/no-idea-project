using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    void Awake()
    {
        RegisterServices();
        InitializeServices();
    }

    private static void RegisterServices()
    {
#if ENABLE_INPUT_SYSTEM
        IInputService inputService = new InputService();
#elif ENABLE_LEGACY_INPUT_MANAGER
        IInputService inputService = new LegacyInputManagerService();
#endif
        ServiceLocator.RegisterService<IInputService>(inputService);

        IPhysics2DService physics2DService = new Physics2DService();
        ServiceLocator.RegisterService<IPhysics2DService>(physics2DService);

        IDataRepository dataRepository = new DataRepository();
        ServiceLocator.RegisterService<IDataRepository>(dataRepository);
    }

    private static void InitializeServices()
    {
        List<IInitializableService> initializableServices =
            ServiceLocator.GetServices<IInitializableService>();

        foreach (IInitializableService initializableService in initializableServices)
        {
            initializableService.Initialize();
        }
    }
}
