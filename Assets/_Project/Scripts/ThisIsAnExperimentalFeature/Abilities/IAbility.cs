public interface IAbility
{
    bool IsTriggered { get; }
    bool CanBeDone { get; }

    void OnEnter();
    AbilityStatus OnUpdate();

    void OnExit();
}
