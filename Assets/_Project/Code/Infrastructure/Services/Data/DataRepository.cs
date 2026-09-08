using UnityEngine;
using VContainer.Unity;

public class DataRepository : IDataRepository, IInitializable
{
    public HeroData HeroData { get; private set; }

    public void Initialize()
    {
        HeroConfig heroConfig = Resources.Load<HeroConfig>("Configs/HeroConfig");
        HeroData = heroConfig.HeroData;
    }
}