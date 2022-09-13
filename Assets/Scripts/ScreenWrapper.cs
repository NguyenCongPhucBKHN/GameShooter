using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper: MonoBehaviour
{
    float colliderRadius;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // save for efficiency
        colliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;
    }

    /// <summary>
    /// Called when the camera can no longer see the game object
    /// </summary>
    void OnBecameInvisible()
    {
        Vector3 location = transform.position;

        // check left, right, top, and bottom sides
        if (location.x + colliderRadius < ScreenUtils.ScreenLeft)
            location.x += colliderRadius+1f;
         else if (location.x - colliderRadius > ScreenUtils.ScreenRight)
        
            location.x -= colliderRadius+1f;

        
        if (location.y - colliderRadius > ScreenUtils.ScreenTop ||
            location.y + colliderRadius < ScreenUtils.ScreenBottom)
        {
            location.y *= -1;
        }
        location.z=-0.25f;

        // move game object
        transform.position = location;
    }
}
