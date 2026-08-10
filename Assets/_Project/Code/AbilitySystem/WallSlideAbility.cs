using Code.AbilitySystem;
using Code.AbilitySystem.Unity;
using UnityEngine;

namespace AbilitySystem
{
    public class WallSlideAbility : AbilityBase
    {
        private static readonly ContactPoint2D[] ContactPoints = new ContactPoint2D[8];
        
        private IHeroDataRepository heroDataRepository;
        private HeroController heroController;

        public WallSlideAbility(AbilityUser abilityUser, IHeroDataRepository heroDataRepository) : base(abilityUser)
        {
            this.heroDataRepository = heroDataRepository;
            heroController = abilityUser.GetComponent<HeroController>();
        }

        public override bool IsTriggered()
        {
            return true;
        }

        public override bool CanBeUsed()
        {
            if (heroController.IsGrounded || heroController.VerticalVelocity > 0)
                return false;
            
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(LayerMasks.SurfaceMask);

            float angle = AbilityUser.Blackboard.MoveAxis2D().x > 0 ? 180f : 0f;

            contactFilter.SetNormalAngle(angle - heroDataRepository.Data.WallSlideAngle , angle + heroDataRepository.Data.WallSlideAngle);

            int count = heroController.Rigidbody.GetContacts(contactFilter, ContactPoints);


            return count > 0;
        }

        public override bool CanComplete()
        {
            return !IsTriggered() || !CanBeUsed();
        }

        public override void Use()
        {
            heroController.Animator.Play("WallSlide");
        }

        public override void FixedTick()
        {
            float dir = AbilityUser.Blackboard.MoveAxis2D().x;

            float speed = AbilityUser.Blackboard.HasRunInput()
                ? heroDataRepository.Data.RunSpeed
                : heroDataRepository.Data.MovementSpeed;
            
            heroController.AirMove(dir, speed);

            Vector2 velocity = heroController.Rigidbody.linearVelocity;
            velocity.y *= 1 - heroDataRepository.Data.WallSlideFriction * Time.deltaTime;

            heroController.Rigidbody.linearVelocity = velocity;

        }
    }
}