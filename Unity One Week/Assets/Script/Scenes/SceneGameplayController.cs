/* SceneGameplayController.cs
 * Author		: Ripandy Adha (ripandy.adha@kadinche.com, ripandy.adha@gmail.com)
 **/
 
 using UnityEngine;
 using DG.Tweening;

public class SceneGameplayController : SceneControllerBase
{
    [SerializeField] PlayerController[] players;

    int activePlayer = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(0)) {
            Vector2 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // RaycastHit2D[] hits = Physics2D.RaycastAll(cursor, Vector2.zero, 0f);
            players[activePlayer].transform.DOMove(cursor, 1f);
        }
    }
}
