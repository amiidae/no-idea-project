using System;
using UnityEngine;

[Serializable]
public abstract class Actor : MonoBehaviour, IActor
{
    [field: SerializeField]
    public virtual IActorController actorController { get; protected set; }

    [field: SerializeField]
    public virtual ActorContext actorContext { get; protected set; }
}
