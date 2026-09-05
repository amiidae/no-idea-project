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
namespace Bnny.Scripts
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
            // what to do with initializable services?
            builder
                .Register<IDataService, DataRepository>(Lifetime.Singleton)
                .As<IInitializable>();

            builder.Register<ISerializer, NewtonsoftSerializer>(Lifetime.Singleton);
            // this puppy requires two other services to function. so?
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
        }
    }
}


// Question:
// what is the deal with keys
// https://vcontainer.hadashikick.jp/resolving/constructor-injection#key-attribute
