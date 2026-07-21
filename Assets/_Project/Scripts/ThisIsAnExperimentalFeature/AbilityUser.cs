using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityUser : MonoBehaviour
{
    [SerializeField]
    private HeroController heroController;
    private HeroContext heroContext;

    private List<AbilityLayer> abilityLayers;

    void Start()
    {
        heroContext = heroController.HeroContext;
        // take services
        abilityLayers = new List<AbilityLayer>()
        {
            new AbilityLayer(
                new Idle(heroContext),
                new Walk(heroContext),
                new Run(heroContext),
                new Jump(heroContext),
                new Fall(heroContext)
            ),
        };
    }

    void Update()
    {
        CheckForTriggeredAbilities();

        TickAbilityLayers();
    }

    private void CheckForTriggeredAbilities()
    {
        foreach (AbilityLayer abilityLayer in abilityLayers)
        {
            foreach (IAbility ability in abilityLayer.Abilities)
            {
                if (ability.IsTriggered && ability.CanBeDone)
                {
                    if (ability == abilityLayer.activeAbility)
                    {
                        break;
                    }
                    else
                    {
                        abilityLayer.ClearActiveAbility();
                        abilityLayer.SetActiveAbility(ability);
                        abilityLayer.UseActiveAbility();
                        break;
                    }
                }
            }
        }
    }

    private void TickAbilityLayers()
    {
        foreach (AbilityLayer abilityLayer in abilityLayers)
        {
            //wouldnt it be good to also have a default ability on each layer? it could also be null for the layers that dont assume any default behaviour
            AbilityStatus abilityStatus = abilityLayer.UpdateActiveAbility();

            if (abilityStatus == AbilityStatus.Complete)
            {
                abilityLayer.ClearActiveAbility();
            }
        }
    }
}
