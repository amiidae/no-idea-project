using Bnny.Scripts.Services.Data;
using Bnny.Scripts.Services.Input;
using Bnny.Scripts.Services.Physics;
using Bnny.Scripts.Services.SaveLoad;
using Bnny.Scripts.Services.Serializer;
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
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
#if ENABLE_INPUT_SYSTEM
            builder
                .Register<IInputService, InputSystemService>(Lifetime.Singleton)
                .As<IInitializable>();
#elif ENABLE_LEGACY_INPUT_MANAGER
            builder.Register<IInputService, LegacyInputManagerService>(Lifetime.Singleton);
# endif

            builder.Register<ITimeService, TimeService>(Lifetime.Singleton);

            builder.Register<IPhysics2DService, Physics2DService>(Lifetime.Singleton);

            builder.Register<IDataService, DataRepository>(Lifetime.Singleton).As<IInitializable>();

            builder.Register<ISerializer, NewtonsoftSerializer>(Lifetime.Singleton);

            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
        }
    }
}


// Question:
// what is the deal with keys
// https://vcontainer.hadashikick.jp/resolving/constructor-injection#key-attribute

// WithParameters takes a type of arg and the value of arg and adds them to the list. Then it takes the value of arg from the list to pass it as argument to the instance, created by resolver.Resolve
