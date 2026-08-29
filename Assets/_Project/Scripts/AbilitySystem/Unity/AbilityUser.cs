using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUser : MonoBehaviour, IAbilityUser
{
    [field: SerializeField]
    public HeroController HeroController { get; private set; }

    [field: SerializeField]
    public IAbilityUserBlackboard AbilityUserBlackboard { get; private set; }

    public List<AbilityLayer> AbilityLayers { get; private set; } = new List<AbilityLayer>();

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
                new IdleAbility(this, AbilityUserBlackboard),
                new WalkAbility(
                    this,
                    AbilityUserBlackboard,
                    ServiceLocator.GetService<IDataService>()
                ),
                new RunAbility(
                    this,
                    AbilityUserBlackboard,
                    ServiceLocator.GetService<IDataService>()
                ),
                new FallAbility(
                    this,
                    AbilityUserBlackboard,
                    ServiceLocator.GetService<IDataService>()
                )
            )
        );

        AbilityLayers.Add(
            new AbilityLayer(
                new WallSlideAbility(
                    this,
                    AbilityUserBlackboard,
                    ServiceLocator.GetService<IDataService>()
                )
            )
        );

        AbilityLayers.Add(
            new AbilityLayer(
                new JumpAbility(
                    this,
                    AbilityUserBlackboard,
                    ServiceLocator.GetService<IDataService>()
                ),
                new DoubleJumpAbility(
                    this,
                    AbilityUserBlackboard,
                    ServiceLocator.GetService<IDataService>()
                ),
                new LongJumpAbility(
                    this,
                    AbilityUserBlackboard,
                    ServiceLocator.GetService<IDataService>()
                ),
                new LandAbility(this)
            )
        );

        AbilityLayers.Add(
            new AbilityLayer(
                new WallJumpAbility(
                    this,
                    AbilityUserBlackboard,
                    ServiceLocator.GetService<IDataService>()
                )
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
