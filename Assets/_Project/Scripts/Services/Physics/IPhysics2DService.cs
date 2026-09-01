using UnityEngine;

namespace Bnny.Scripts.Services.Physics
{
    public interface IPhysics2DService
    {
        public Collider2D OverlapCircle(Vector2 point, float radius, int layerMask);
        public RaycastHit2D Raycast(
            Vector2 origin,
            Vector2 direction,
            float distance,
            int layerMask
        );
    }
}
