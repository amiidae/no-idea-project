using System;
using System.Collections.Generic;
using Bnny.Scripts.AbilitySystem.Abilities;
using Bnny.Scripts.AbilitySystem.Abilities.Jumps;
using Bnny.Scripts.AbilitySystem.Abilities.Locomotions;
using Bnny.Scripts.AbilitySystem.Core;
using Bnny.Scripts.DI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Bnny.Scripts.AbilitySystem.Unity
{
    public class AbilityUser : MonoBehaviour, IAbilityUser
    {
        [field: SerializeField]
        public HeroController HeroController { get; private set; }

        [field: SerializeField]
        public IAbilityUserBlackboard AbilityUserBlackboard { get; private set; }

        public List<AbilityLayer> AbilityLayers { get; private set; } = new List<AbilityLayer>();

        private IObjectResolver container;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            this.container = container;
        }

        void Start()
        {
            AbilityUserBlackboard = gameObject.GetComponent<IAbilityUserBlackboard>();

            PopulateAbilityLayers();
            InitializeAbilities();
        }

        private void OnDestroy()
        {
            DestroyAbilities();
        }

        void Update()
        {
            for (int i = AbilityLayers.Count - 1; i >= 0; i--)
            {
                AbilityLayer abilityLayer = AbilityLayers[i];

                if (IsSuppressed(i))
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

                        Debug.Log(abilityLayer.ActiveAbility);

                        break;
                    }
                }

                if (abilityLayer.ActiveAbility != null)
                {
                    abilityLayer.ActiveAbility.Update();

                    if (abilityLayer.ActiveAbility.CanComplete())
                    {
                        CompleteActiveAbility(abilityLayer);
                    }
                }
            }
        }

        void FixedUpdate()
        {
            for (int i = AbilityLayers.Count - 1; i >= 0; i--)
            {
                if (IsSuppressed(i))
                {
                    continue;
                }

                AbilityLayers[i].ActiveAbility?.FixedUpdate();
            }
        }

        private bool IsSuppressed(int layerIndex)
        {
            for (int i = layerIndex + 1; i < AbilityLayers.Count; i++)
            {
                if (AbilityLayers[i].ActiveAbility != null)
                {
                    return true;
                }
            }

            return false;
        }

        private void UseAbility(AbilityLayer abilityLayer, IAbility ability)
        {
            abilityLayer.ActiveAbility = ability;
            abilityLayer.ActiveAbility.Use();
        }

        private void CompleteActiveAbility(AbilityLayer abilityLayer)
        {
            if (abilityLayer.ActiveAbility == null)
                return;

            abilityLayer.ActiveAbility.Complete();
            abilityLayer.ActiveAbility = null;
        }

        private void PopulateAbilityLayers()
        {
            AbilityLayers.Add(
                new AbilityLayer(
                    container.Instantiate<IdleAbility>(this),
                    container.Instantiate<WalkAbility>(this),
                    container.Instantiate<RunAbility>(this),
                    container.Instantiate<FallAbility>(this)
                )
            );

            AbilityLayers.Add(new AbilityLayer(container.Instantiate<WallSlideAbility>(this)));

            AbilityLayers.Add(
                new AbilityLayer(
                    container.Instantiate<JumpAbility>(this),
                    container.Instantiate<DoubleJumpAbility>(this),
                    container.Instantiate<LongJumpAbility>(this),
                    container.Instantiate<LandAbility>(this)
                )
            );

            AbilityLayers.Add(new AbilityLayer(container.Instantiate<WallJumpAbility>(this)));
        }

        private void InitializeAbilities()
        {
            LoopAbilities(InitAbilityOperation);

            void InitAbilityOperation(IAbility ability)
            {
                ability.Init();
            }
        }

        private void DestroyAbilities()
        {
            LoopAbilities(DestroyAbilityOperation);

            void DestroyAbilityOperation(IAbility ability)
            {
                ability.Destroy();
            }
        }

        private void LoopAbilities(Action<IAbility> operation)
        {
            foreach (AbilityLayer abilityLayer in AbilityLayers)
            {
                foreach (IAbility ability in abilityLayer.Abilities)
                {
                    operation.Invoke(ability);
                }
            }
        }
    }
}
