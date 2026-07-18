using Unity.VisualScripting;
using UnityEngine;

public class HeroDataRepository : IHeroDataRepository
{
    // not even being verbose much
    // because it actually feels that HeroConfigHeroDataRepository_Service
    // would make a much better name indeed

    public HeroData Data { get; private set; }

    public void Initialize()
    {
        HeroConfig heroConfig = Resources.Load<HeroConfig>("Configs/HeroConfig");
        Data = heroConfig.HeroData;
    }
}
