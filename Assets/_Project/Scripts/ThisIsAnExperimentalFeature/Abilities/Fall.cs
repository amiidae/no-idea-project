using UnityEngine;

public class Fall : Ability
{
    private HeroContext heroContext;

    public Fall(HeroContext heroContext)
    {
        this.heroContext = heroContext;
    }
}
