using System.Collections.Generic;
using UnityEngine;

public class AbilityBlackboard : MonoBehaviour, IAbilityUserBlackboard
{
    private Dictionary<int, float> axes = new Dictionary<int, float>();
    private Dictionary<int, Vector2> axes2D = new Dictionary<int, Vector2>();
    private Dictionary<int, bool> states = new Dictionary<int, bool>();

    public float GetAxis(int inputTypeId)
    {
        return axes.GetValueOrDefault(inputTypeId);
    }

    public Vector2 GetAxis2D(int inputTypeId)
    {
        return axes2D.GetValueOrDefault(inputTypeId);
    }

    public bool GetState(int inputTypeId)
    {
        return states.GetValueOrDefault(inputTypeId);
    }

    public void SetAxis(int inputTypeId, float value)
    {
        axes[inputTypeId] = value;
    }

    public void SetAxis2D(int inputTypeId, Vector2 value)
    {
        axes2D[inputTypeId] = value;
    }

    public void SetState(int inputTypeId, bool value)
    {
        states[inputTypeId] = value;
    }
}
