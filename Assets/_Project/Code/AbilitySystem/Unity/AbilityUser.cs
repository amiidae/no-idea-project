using System;
using System.Collections.Generic;
using AbilitySystem;
using Code.AbilitySystem.Core;
using UnityEngine;

namespace Code.AbilitySystem.Unity
{
    public class AbilityUser : MonoBehaviour, IAbilityUser
    {
        public IAbilityBlackboard Blackboard { get; private set; }
        
        public List<AbilityLayer> Layers { get; private set; }
        
        
        private void Start()
        {
            Blackboard = GetComponent<IAbilityBlackboard>();
            
            Layers = new List<AbilityLayer>();

            Layers.Add(new AbilityLayer(
                new IdleAbility(this),
                new WalkAbility(this, ServiceLocator.GetService<IDataRepository>()),
                new RunAbility(this, ServiceLocator.GetService<IDataRepository>()),
                new FallAbility(this, ServiceLocator.GetService<IDataRepository>())
            ));

            Layers.Add(new AbilityLayer(
                new JumpAbility(this, ServiceLocator.GetService<IDataRepository>()),
                new AirJumpAbility(this, ServiceLocator.GetService<IDataRepository>()),
                new LandAbility(this)
            ));

            Layers.Add(new AbilityLayer(
                new WallSlideAbility(this, ServiceLocator.GetService<IDataRepository>())
            ));

            foreach (AbilityLayer abilityLayer in Layers)
            {
                foreach (IAbility ability in abilityLayer.Abilities)
                {
                    ability.Init();
                }
            }
        }

        private void OnDestroy()
        {
            foreach (AbilityLayer abilityLayer in Layers)
            {
                foreach (IAbility ability in abilityLayer.Abilities)
                {
                    ability.Destroy();
                }
            }
        }

        private void Update()
        {
            for (int i = Layers.Count - 1; i >= 0; i--)
            {
                AbilityLayer abilityLayer = Layers[i];

                if (IsSuppressed(i)) // guard clause
                {
                    CompleteActiveAbility(abilityLayer);
                    continue;
                }

                foreach (IAbility ability in abilityLayer.Abilities)
                {
                    if (ability == abilityLayer.ActiveAbility)
                    {
                        continue;
                    }

                    if (ability.IsTriggered() && ability.CanBeUsed())
                    {
                        CompleteActiveAbility(abilityLayer);
                        UseAbility(abilityLayer, ability);
                        break;
                    }
                }

                if (abilityLayer.ActiveAbility != null)
                {
                    abilityLayer.ActiveAbility.Tick();

                    if (abilityLayer.ActiveAbility.CanComplete())
                    {
                        CompleteActiveAbility(abilityLayer);
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            for (int i = Layers.Count - 1; i >= 0; i--)
            {
                if (IsSuppressed(i))
                {
                    continue;
                }

                Layers[i].ActiveAbility?.FixedTick();
            }
        }

        private bool IsSuppressed(int layerIndex)
        {
            for (int i = layerIndex + 1; i < Layers.Count; i++)
            {
                if (Layers[i].ActiveAbility != null)
                {
                    return true;
                }
            }

            return false;
        }

        private void UseAbility(AbilityLayer abilityLayer, IAbility ability)
        {
            abilityLayer.ActiveAbility = ability;
            ability.Use();
        }

        private void CompleteActiveAbility(AbilityLayer abilityLayer)
        {
            if(abilityLayer.ActiveAbility == null)
                return;

            abilityLayer.ActiveAbility.Complete();
            abilityLayer.ActiveAbility = null;
        }
    }
}