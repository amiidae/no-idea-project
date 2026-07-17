using UnityEngine;

public interface IPhysics2DService
{
    public Collider2D OverlapCircle(Vector2 point, float radius, int layerMask);
}
