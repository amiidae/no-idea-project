using Code.Services.Input;
using Code.Services.Progress;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.DI
{
    public class AppLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterDataRepository(builder);
            RegisterInputService(builder);
            RegisterSerializer(builder);
            RegisterPhysicsService(builder);
            RegisterSaveLoadService(builder);
        }

        private void RegisterSerializer(IContainerBuilder builder)
        {
            builder
                .Register<NewtonsoftSerializer>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                ;
        }

        private void RegisterPhysicsService(IContainerBuilder builder)
        {
            builder
                .Register<Physics2DService>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                ;
        }

        private void RegisterDataRepository(IContainerBuilder builder)
        {
            builder
                .Register<DataRepository>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                ;
        }

        private void RegisterInputService(IContainerBuilder builder)
        {
            builder
#if ENABLE_INPUT_SYSTEM
                .Register<InputService>(Lifetime.Singleton)
#elif ENABLE_LEGACY_INPUT_MANAGER
                .Register<LegacyInputManagerService>(Lifetime.Singleton)
#endif
                .AsImplementedInterfaces()
                ;

   
        }

        private void RegisterSaveLoadService(IContainerBuilder builder)
        {
            builder
                .Register<SaveLoadService>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                ;
            
            builder
                .Register<SaveProgressByInput>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                ;
        }
    }
}