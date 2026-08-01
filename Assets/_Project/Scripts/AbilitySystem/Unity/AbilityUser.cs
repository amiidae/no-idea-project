using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUser : MonoBehaviour
{
    [field: SerializeField]
    public HeroController HeroController { get; private set; }
    public List<AbilityLayer> AbilityLayers { get; private set; } = new List<AbilityLayer>();

    void Start()
    {
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
        for (int i = 0; i < AbilityLayers.Count; i++)
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
                new IdleAbility(this, ServiceLocator.GetService<IInputService>()),
                new WalkAbility(
                    this,
                    ServiceLocator.GetService<IInputService>(),
                    ServiceLocator.GetService<IDataService>()
                ),
                new RunAbility(
                    this,
                    ServiceLocator.GetService<IInputService>(),
                    ServiceLocator.GetService<IDataService>()
                ),
                new FallAbility(
                    this,
                    ServiceLocator.GetService<IInputService>(),
                    ServiceLocator.GetService<IDataService>()
                )
            )
        );

        AbilityLayers.Add(
            new AbilityLayer(
                new JumpAbility(
                    this,
                    ServiceLocator.GetService<IInputService>(),
                    ServiceLocator.GetService<IDataService>()
                ),
                new DoubleJumpAbility(
                    this,
                    ServiceLocator.GetService<IInputService>(),
                    ServiceLocator.GetService<IDataService>()
                ),
                new LongJumpAbility(
                    this,
                    ServiceLocator.GetService<IInputService>(),
                    ServiceLocator.GetService<IDataService>()
                ),
                new LandAbility(this)
            )
        );
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
