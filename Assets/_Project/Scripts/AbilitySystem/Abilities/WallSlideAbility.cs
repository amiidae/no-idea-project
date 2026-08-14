using UnityEngine;

public class WallSlideAbility : Ability
{
    private IAbilityUserBlackboard abilityUserBlackboard;
    private IDataService dataService;
    private HeroController heroController;

    public WallSlideAbility(
        AbilityUser abilityUser,
        IAbilityUserBlackboard abilityUserBlackboard,
        IDataService dataService
    )
    {
        this.abilityUserBlackboard = abilityUserBlackboard;
        this.dataService = dataService;
        this.heroController = abilityUser.HeroController;
    }

    public override void Init()
    {
        // LedgeHang;
    }

    public override bool IsTriggered()
    {
        return heroController.IsFacedAgainstWall == true && heroController.IsGrounded == false; // hero collides with a wall with a raycast
    }

    public override bool CanBeUsed()
    {
        return heroController.IsFacedAgainstWall == true
            && heroController.IsGrounded == false
            && heroController.VerticalVelocity < 0;
    }

    public override bool CanComplete()
    {
        return heroController.IsFacedAgainstWall == false && heroController.IsGrounded == true;
    }

    public override void Use()
    {
        heroController.Animator.Play("LedgeHang");
    }

    public override void Update() { }

    public override void FixedUpdate() { }

    public override void Complete() { }

    public override void Destroy() { }
}


/*
i want to cast a raycast
raycast should change position to be in sync with characters line of sight
look left -> raycast to the left, look right -> raycast to the right
in the moment, when raycast hits the wall
and previously character was not hitting a wall (genius) => hop on the wall
when character is on the wall character isGrounded == false;
also, when on the wall character moves down with the gravity - friction speed (whaaaat? you okay physicist777)
when character-raycast does not hit a wall,
but previously it was hitting a wall => dismount a wall (that formulation is mental, girl)
wall dismounting can also happen when a character becomes isGrounded == true
or when the character jumps from the wall, so jumpInputReceived == true

feels like wall climbing stuff should have its own ability layer
*/
