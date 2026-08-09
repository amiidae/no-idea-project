using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackboard : MonoBehaviour, IAbilityUserBlackboard
{
    private Dictionary<int, float> axes = new Dictionary<int, float>();
    private Dictionary<int, Vector2> axes2D = new Dictionary<int, Vector2>();
    private Dictionary<int, bool> states = new Dictionary<int, bool>();

    public float GetAxis(int abilityId)
    {
        return axes.GetValueOrDefault(abilityId);
    }

    public Vector2 GetAxis2D(int abilityId)
    {
        return axes2D.GetValueOrDefault(abilityId);
    }

    public bool GetState(int abilityId)
    {
        return states.GetValueOrDefault(abilityId);
    }

    public void SetAxis(int abilityId, float value)
    {
        axes[abilityId] = value;
    }

    public void SetAxis2D(int abilityId, Vector2 value)
    {
        axes2D[abilityId] = value;
    }

    public void SetState(int abilityId, bool value)
    {
        states[abilityId] = value;
    }
}
