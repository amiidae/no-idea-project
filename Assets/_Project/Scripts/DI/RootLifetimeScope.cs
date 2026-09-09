using System;
using Bnny.Scripts.Services.Data;
using Bnny.Scripts.Services.Input;
using Bnny.Scripts.Services.Physics;
using Bnny.Scripts.Services.SaveLoad;
using Bnny.Scripts.Services.Serializer;
using Bnny.Scripts.Services.Settings;
using Bnny.Scripts.Services.Time;
using VContainer;
using VContainer.Unity;

/*
⣿⣿⣿⣿⣿⣿⣿⣿⡿⢋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⣿⣿⣿⣿⣿⡿⢋⠐⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠒⠀⠀⠀⠀⢀⡤⡞⠁⠀⣀⣀⡀⠀⠀⠀⠀⠀⠀
⣿⣿⣿⣿⡿⢋⠔⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡴⢋⡤⠴⠋⠉⢀⠀⠀⠄⠀⠀⠀⠀⠀
⣿⣿⣿⡟⠌⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠓⠀⠀⣠⠀⠎⠴⠊⠀⠀⠀⠀⠀⠈⠁⠀⠀⠀⠀⠀⠀
⣿⣿⣿⠈⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⢤⣤⠤⠀⠀⠀⠀⠀⠀⠀⠠⠴⣋⡦⠆⠀⠀⠀⠐⢰⠄⠀⠆⠀⠀⠀⠀⠀⠀
⣿⣿⡧⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠴⠊⢁⡼⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⠀⠀⠀⠀⢰⡀⠀⠀⠀⡄⠀⠁⠀⠀⠀⠀⠀
⣿⣿⡅⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡔⠉⠀⢨⠋⠀⠀⠀⠀⡶⢠⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⢳⠚⠉⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡞⠀⠀⠀⡏⠀⠀⠠⠖⠤⢧⡀⣱⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⠀⠀⠀⠈⡇⠀⠀⠀⠑⠢⢠⡙⠂⠀⢰⠓⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡇⠀⠀⠀⠀⡇⠀⠀⠀⠀⠀⠘⡆⠀⠀⠈⢀⡼⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⡅⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢬⠀⠀⠀⠀⠸⡄⠀⠀⠀⠀⡜⠀⠀⠀⠀⠀⢇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⡷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⡄⠀⠀⠀⠀⠹⡄⠀⢀⣲⠁⠀⠀⠀⠀⢣⠈⡆⠀⠀⠀⢠⠀⠀⠀⠠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢱⡀⠀⠀⠀⠀⠙⠺⡉⡇⠀⠀⠀⠀⢠⠀⣃⠩⠀⢀⡴⡿⠁⠀⠀⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⣿⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠹⣄⠀⠀⠀⠀⠀⠈⠑⠦⠠⠄⣀⣀⣑⠥⠧⠚⢉⡼⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠢⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡰⠊⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⣿⡧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠑⠠⠄⣀⣀⠀⠀⠀⣀⣀⠤⠒⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
*/
namespace Bnny.Scripts.DI
{
    public class RootLifetimeScope : LifetimeScopeBase
    {
        protected override void InitializeBindings()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            RegisterInputService();
            RegisterTimeService();
            RegisterPhysicsService();
            RegisterDataService();
            RegisterSerializerService();
            RegisterSaveLoadService();
            RegisterSettingsService();
        }

        private void RegisterInputService()
        {
#if ENABLE_INPUT_SYSTEM
            ContainerBuilder
                .Register<IInputService, InputSystemService>(Lifetime.Singleton)
                .As<IInitializable>();
#elif ENABLE_LEGACY_INPUT_MANAGER
            ContainerBuilder.Register<IInputService, LegacyInputManagerService>(Lifetime.Singleton);
# endif
        }

        private void RegisterTimeService()
        {
            ContainerBuilder.Register<ITimeService, TimeService>(Lifetime.Singleton);
        }

        private void RegisterPhysicsService()
        {
            ContainerBuilder.Register<IPhysics2DService, Physics2DService>(Lifetime.Singleton);
        }

        private void RegisterDataService()
        {
            ContainerBuilder
                .Register<IDataService, DataRepository>(Lifetime.Singleton)
                .As<IInitializable>();
        }

        private void RegisterSerializerService()
        {
            ContainerBuilder.Register<ISerializer, NewtonsoftSerializer>(Lifetime.Singleton);
        }

        private void RegisterSaveLoadService()
        {
            ContainerBuilder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
        }

        private void RegisterSettingsService()
        {
            ContainerBuilder.Register<ISettingsService, SettingsService>(Lifetime.Singleton);
        }
    }
}


// Question:
// what is the deal with keys
// https://vcontainer.hadashikick.jp/resolving/constructor-injection#key-attribute
