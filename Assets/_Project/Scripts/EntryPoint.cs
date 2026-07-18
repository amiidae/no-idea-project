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
        IInputService inputService = new InputService();
        ServiceLocator.RegisterService<IInputService>(inputService);

        IPhysics2DService physics2DService = new Physics2DService();
        ServiceLocator.RegisterService<IPhysics2DService>(physics2DService);

        IHeroDataRepository heroDataRepository = new HeroDataRepository();
        ServiceLocator.RegisterService<IHeroDataRepository>(heroDataRepository);
    }

    private static void InitializeServices()
    {
        List<IInitializableService> initializableServices =
            ServiceLocator.GetServices<IInitializableService>();

        foreach (IInitializableService initializableService in initializableServices)
        {
            initializableService.Initialize();
        }
        { }
    }
}
