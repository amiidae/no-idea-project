namespace Bnny.Scripts.Services.Data
{
    public interface IDataService : IInitializableService
    {
        public HeroData HeroData { get; }
    }
}
