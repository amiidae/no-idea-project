using System;
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

#if  ENABLE_INPUT_SYSTEM
        inputService = new InputSystemService();
#elif ENABLE_LEGACY_INPUT_MANAGER
        inputService = new LegacyInputManagerService();
#endif

        ServiceLocator.RegisterService<IInputService>(inputService);

        ITimeService timeService = new TimeService();
        ServiceLocator.RegisterService<ITimeService>(timeService);

        IPhysics2DService physics2DService = new Physics2DService();
        ServiceLocator.RegisterService<IPhysics2DService>(physics2DService);

        IDataService dataRepository = new DataRepository();
        ServiceLocator.RegisterService<IDataService>(dataRepository);

        ISerializer serializer = new NewtonsoftSerializer();
        ServiceLocator.RegisterService<ISerializer>(serializer);

        // Question: is it passing  same references?
        ISaveLoadService saveLoadService = new SaveLoadService(serializer, inputService);
        ServiceLocator.RegisterService<ISaveLoadService>(saveLoadService);
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
