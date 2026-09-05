using VContainer.Unity;

namespace Bnny.Scripts.Services.Data
{
    public interface IDataService : IInitializableService, IInitializable
    {
        public HeroData HeroData { get; }
    }
}
