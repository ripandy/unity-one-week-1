/* PlayerController.cs
 * Author		: Ripandy Adha (ripandy.adha@kadinche.com, ripandy.adha@gmail.com)
 **/

using UnityEngine;

public class PlayerController : UnitBase
{
    public System.Action OnDamaged;
    public System.Action OnEnemyKilled;

    public bool IsActiveCharacter { get; set; }
    
    const float DEFAULT_DAMAGE_TIMEOUT = 1f;

    float damagedTimeout = 0f;

    void FixedUpdate()
    {
        var movement = rigidBody.velocity;

        // Check for a collision along our path.
        RaycastHit2D collision = Physics2D.BoxCast(transform.TransformPoint (boxCollider.offset), boxCollider.size, movement.magnitude, movement);

        if(collision.collider != null) {
            if (collision.transform.CompareTag("Enemy")) {
                if (IsActiveCharacter)
                {
                    collision.transform.gameObject.SetActive(false);
                    if (OnEnemyKilled != null)
                        OnEnemyKilled.Invoke();
                }
                else
                {
                    if (damagedTimeout == 0f)
                    {
                        if (OnDamaged != null)
                            OnDamaged.Invoke();
                        damagedTimeout = DEFAULT_DAMAGE_TIMEOUT;
                    }
                }
            }
        }

        if (!IsActiveCharacter) {
            if (damagedTimeout > 0)
            {
                damagedTimeout -= Time.deltaTime;
            }
            else
            {
                damagedTimeout = 0f;
            }
        }
    }
}
