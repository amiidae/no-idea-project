using Code.AbilitySystem;
using Code.AbilitySystem.Unity;
using UnityEngine;

namespace AbilitySystem
{
    public class WallJumpAbility : JumpAbilityBase
    {
        private Vector2 _wallNormal;

        public WallJumpAbility(AbilityUser abilityUser, IDataRepository dataRepository)
            : base(abilityUser, dataRepository)
        {
        }

        public override bool CanBeUsed()
        {
            if (_heroController.IsGrounded)
            {
                return false;
            }
            
            Vector2 direction = new Vector2(_heroController.FacingX, 0);
            
            if (CheckForWallJump(direction,  out _wallNormal))
                return true;


            if (CheckForWallJump(-direction, out _wallNormal))
                return true;

            return false;
        }


        public override void Use()
        {
            _heroController.WallJump(_wallNormal);

            if (AbilityUser.Blackboard.MoveAxis2D().x == 0)
            {
                _heroController.FaceDirection(_wallNormal.x);
            }

            _heroController.Animator.Play("Jump");
        }

        private bool CheckForWallJump(Vector2 direction, out Vector2 wallNormal)
        {
            wallNormal = Vector2.zero;
            
            var bounds = _heroController.Collider.bounds;

            RaycastHit2D hit = Physics2D.BoxCast(
                bounds.center, bounds.size, 0, direction,
                _dataRepository.HeroData.DetectionDistance, LayerMasks.SurfaceMask);

            if (hit.collider != null)
            {
                wallNormal = hit.normal;
                return true;
            }
            else
                return false;
        }
    }
}
