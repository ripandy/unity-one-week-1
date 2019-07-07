/* UnitBase.cs
 * Author		: Ripandy Adha (ripandy.adha@kadinche.com, ripandy.adha@gmail.com)
 **/

using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [Range(1f, 15f)]
    [SerializeField] protected float moveSpeed = 8f;
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField] protected float hp = 10f;
    public float HP { get { return hp; } }

    protected Rigidbody2D rigidBody;
    protected BoxCollider2D boxCollider;
    
    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
}
