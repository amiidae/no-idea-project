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
        IInputService inputService;

#if ENABLE_LEGACY_INPUT_MANAGER
        inputService = new LegacyInputManagerService();
#endif
#if ENABLE_INPUT_SYSTEM
        inputService = new InputSystemService();
#endif

        ServiceLocator.RegisterService<IInputService>(inputService);

        IPhysics2DService physics2DService = new Physics2DService();
        ServiceLocator.RegisterService<IPhysics2DService>(physics2DService);

        IDataService heroDataRepository = new DataRepository();
        ServiceLocator.RegisterService<IDataService>(heroDataRepository);
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
