using Code.Services.Input;
using Code.Services.Progress;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.DI
{
    public class AppLifetimeScope : LifetimeScopeBase
    {
        protected override void InstallBindings()
        {
            RegisterDataRepository();
            RegisterInputService();
            RegisterSerializer();
            RegisterPhysicsService();
            RegisterSaveLoadService();
        }
        
        private void RegisterSerializer()
        {
            ContainerBuilder
                .Register<NewtonsoftSerializer>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                ;
        }

        private void RegisterPhysicsService()
        {
            ContainerBuilder
                .Register<Physics2DService>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                ;
        }

        private void RegisterDataRepository()
        {
            ContainerBuilder
                .Register<DataRepository>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                ;
        }

        private void RegisterInputService()
        {
            ContainerBuilder
#if ENABLE_INPUT_SYSTEM
                .Register<InputService>(Lifetime.Singleton)
#elif ENABLE_LEGACY_INPUT_MANAGER
                .Register<LegacyInputManagerService>(Lifetime.Singleton)
#endif
                .AsImplementedInterfaces()
                ;

   
        }

        private void RegisterSaveLoadService()
        {
            ContainerBuilder
                .Register<SaveLoadService>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                ;
            
            ContainerBuilder
                .Register<SaveProgressByInput>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                ;
        }
    }
}