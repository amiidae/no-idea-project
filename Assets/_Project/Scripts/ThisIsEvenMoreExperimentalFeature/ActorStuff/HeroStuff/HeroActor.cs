using System;
using UnityEngine;

public class HeroActor : Actor
{
    [field: SerializeField]
    public override IActorController actorController { get; protected set; }

    [field: SerializeField]
    public HeroContext actorContext { get; protected set; }

    void Awake()
    {
        actorController = new HeroController();
    }
}
