using UnityEngine;

public class Physics2DService : IPhysics2DService
{
    public Collider2D OverlapCircle(Vector2 point, float radius, int layerMask)
    {
        Collider2D collision = Physics2D.OverlapCircle(point, radius, layerMask);
        return collision;
    }
}
