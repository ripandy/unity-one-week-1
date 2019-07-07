/* Enemy.cs
 * Author		: Ripandy Adha (ripandy.adha@kadinche.com, ripandy.adha@gmail.com)
 **/

using UnityEngine;

public class Enemy : UnitBase
{
    Transform currentTarget;

    public void SetTarget(Transform target)
    {
        currentTarget = target;
        UpdateSpeed();
    }

    public void UpdateSpeed()
    {
        Vector2 pos = transform.position;
        Vector2 tpos = currentTarget.position;
        Vector2 v = (tpos - pos).normalized * MoveSpeed;
        rigidBody.velocity = v;
    }
}
