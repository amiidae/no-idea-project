public interface IActor
{
    public IActorController actorController { get; }

    public ActorContext actorContext { get; }
}
