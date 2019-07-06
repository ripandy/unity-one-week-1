/* SceneGameplayController.cs
 * Author		: Ripandy Adha (ripandy.adha@kadinche.com, ripandy.adha@gmail.com)
 **/
 
 using UnityEngine;
 using DG.Tweening;

public class SceneGameplayController : SceneControllerBase
{
    [SerializeField] PlayerController[] players;
    [SerializeField] EnergyBall energyBall;

    Camera m_mainCamera;
    Rigidbody2D[] playerRBs;

    int activePlayer = 0;

    protected override void Awake()
    {
        base.Awake();
        m_mainCamera = Camera.main;

        playerRBs = new Rigidbody2D[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            playerRBs[i] = players[i].GetComponent<Rigidbody2D>();
        }
    }

    protected override void Start()
    {
        energyBall.Reset();
        energyBall.StartGathering();
        base.Start();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0)) {
            Vector2 cursor = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pPos = players[activePlayer].transform.position;
            Vector2 v = (cursor - pPos).normalized * players[activePlayer].MoveSpeed;
            playerRBs[activePlayer].velocity = v;
        }
    }
}
