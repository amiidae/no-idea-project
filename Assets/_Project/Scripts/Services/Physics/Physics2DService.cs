using UnityEngine;

public class Physics2DService : IPhysics2DService
{
    public Collider2D OverlapCircle(Vector2 point, float radius, int layerMask)
    {
        Collider2D collision = Physics2D.OverlapCircle(point, radius, layerMask);
        return collision;
    }

    public RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(origin, direction, distance, layerMask);
        return raycastHit;
    }
}
