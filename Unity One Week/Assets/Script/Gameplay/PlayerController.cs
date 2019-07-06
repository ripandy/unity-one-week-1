/* PlayerController.cs
 * Author		: Ripandy Adha (ripandy.adha@kadinche.com, ripandy.adha@gmail.com)
 **/

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1f, 15f)]
    [SerializeField] float moveSpeed = 8f;
    public float MoveSpeed { get { return moveSpeed; } }
}
