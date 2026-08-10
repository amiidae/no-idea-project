using UnityEngine;

public class DataRepository : IDataRepository
{
    public HeroData HeroData { get; private set; }

    public void Initialize()
    {
        HeroConfig heroConfig = Resources.Load<HeroConfig>("Configs/HeroConfig");
        HeroData = heroConfig.HeroData;
    }
}