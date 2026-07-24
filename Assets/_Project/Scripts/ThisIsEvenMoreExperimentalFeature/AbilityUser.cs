using System.Collections.Generic;
using UnityEngine;

public class AbilityUser : MonoBehaviour
{
    [SerializeField]
    private Actor actor;
    public List<AbilityLayer> AbilityLayers;

    void Update() { }

    void FixedUpdate() { }

    private bool IsSuppressed(int number)
    {
        return true;
    }

    private void UseAbility(AbilityLayer abilityLayer, IAbility ability) { }

    private void CompleteActiveAbility(AbilityLayer abilityLayer) { }
}
