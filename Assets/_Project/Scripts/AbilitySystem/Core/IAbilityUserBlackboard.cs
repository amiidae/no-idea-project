using System.Collections.Generic;
using UnityEngine;

public interface IAbilityUserBlackboard
{
    public float GetAxis(int abilityId);
    public Vector2 GetAxis2D(int abilityId);
    public bool GetState(int abilityId);

    public void SetAxis(int abilityId, float value);

    public void SetAxis2D(int abilityId, Vector2 value);

    public void SetBool(int abilityId, bool value);
}
