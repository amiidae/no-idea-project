using Unity.VisualScripting;
using UnityEngine;

public class DataRepository : IDataService
{
    public HeroData HeroData { get; private set; }

    public void Initialize()
    {
        HeroConfig heroConfig = Resources.Load<HeroConfig>("Configs/HeroConfig");
        HeroData = heroConfig.HeroData;
    }
}
