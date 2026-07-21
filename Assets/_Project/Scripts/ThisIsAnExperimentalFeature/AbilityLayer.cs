using System.Collections.Generic;
using System.Linq;

public class AbilityLayer
{
    public IAbility activeAbility { get; set; }

    public List<IAbility> Abilities;

    public AbilityLayer(params IAbility[] abilities)
    {
        this.Abilities = abilities.ToList<IAbility>();
    }

    public void ClearActiveAbility()
    {
        if (activeAbility != null)
        {
            activeAbility.OnExit();
            activeAbility = null;
        }
    }

    public void SetActiveAbility(IAbility ability)
    {
        activeAbility = ability;
    }

    public void UseActiveAbility()
    {
        activeAbility.OnEnter();
    }

    public AbilityStatus UpdateActiveAbility()
    {
        AbilityStatus abilityStatus = activeAbility.OnUpdate();
        return abilityStatus;
    }
}
