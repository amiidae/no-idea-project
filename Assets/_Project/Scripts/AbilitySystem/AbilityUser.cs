using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.AbilitySystem
{

    public class AbilityLayer
    {
        public IAbility ActiveAbility { get; set; }
        
        public IReadOnlyList<IAbility> Abilities { get; private set; }

        public AbilityLayer(params IAbility[] abilities)
        {
            Abilities = abilities;
        }
        
    }
    
    public class AbilityUser : MonoBehaviour
    {
        [SerializeField] private HeroController _heroController;
        
        public List<AbilityLayer> Layers { get; private set; }
        
        
        private void Start()
        {
            Layers = new List<AbilityLayer>();
        
            
            Layers.Add(new AbilityLayer(
                new IdleAbility(_heroController, ServiceLocator.GetService<IInputService>())
            ));
        }

        private void Update()
        {
            foreach (AbilityLayer abilityLayer in Layers)
            {
                foreach (IAbility ability in abilityLayer.Abilities)
                {
                    if (ability == abilityLayer.ActiveAbility)
                    {
                        continue;
                    }

                    if (ability.IsTriggered() && ability.CanBeUsed())
                    {
                        EndActive(abilityLayer);
                        abilityLayer.ActiveAbility = ability;
                        ability.OnUse();
                        break;
                    }
                }
            }

            foreach (AbilityLayer abilityLayer in Layers)
            {
                if (abilityLayer.ActiveAbility != null)
                {
                    AbilityStatus abilityStatus = abilityLayer.ActiveAbility.OnPerform();

                    if (abilityStatus == AbilityStatus.Completed)
                    {
                        EndActive(abilityLayer);
                    }
                }
            }
        }

        private void EndActive(AbilityLayer abilityLayer)
        {
            if(abilityLayer.ActiveAbility == null)
                return;
            
            abilityLayer.ActiveAbility.OnComplete();
            abilityLayer.ActiveAbility = null;
        }
    }
}