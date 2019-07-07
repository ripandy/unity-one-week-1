/* SceneGameplayController.cs
 * Author		: Ripandy Adha (ripandy.adha@kadinche.com, ripandy.adha@gmail.com)
 **/

using System.Collections; 
using UnityEngine;
using UniRx;
using TMPro;

public class SceneGameplayController : SceneControllerBase
{
    [SerializeField] PlayerController[] players;
    [SerializeField] EnergyBall energyBall;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] TMP_Text titleText;
    [SerializeField] GameObject overlay;
    [SerializeField] UnityEngine.UI.Image[] lifes;
    [SerializeField] Color[] lifesColor;

    static readonly string TAP_TO_START = "Tap to Start";
    static readonly string GAME_OVER = "Game Over";

    Camera m_mainCamera;
    Rigidbody2D[] playerRBs;

    int activePlayer = 0;

    GameState gameState;

    int life = 3;

    protected override void Awake()
    {
        base.Awake();
        m_mainCamera = Camera.main;

        activePlayer = 0;
        playerRBs = new Rigidbody2D[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            players[i].IsActiveCharacter = (activePlayer == i);
            playerRBs[i] = players[i].GetComponent<Rigidbody2D>();
            players[i].OnEnemyKilled += Player_OnEnemyKilled;
            players[i].OnDamaged += Player_OnDamaged;
        }

        SetState(GameState.Pregame);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) {
            if (gameState == GameState.Pregame)
            {
                ScheduleState(GameState.Gameplay);
            }
            else if (gameState == GameState.Endgame)
            {
                ScheduleState(GameState.Pregame);
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0)) {
            if (gameState == GameState.Gameplay)
            {
                Vector2 cursor = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 pPos = players[activePlayer].transform.position;
                Vector2 v = (cursor - pPos).normalized * players[activePlayer].MoveSpeed;
                playerRBs[activePlayer].velocity = v;
            }
        }

        if (life == 0 || energyBall.Energy == 0)
            ScheduleState(GameState.Endgame);
    }

    void SetState(GameState newState)
    {
        Debug.Log("changing state to " + newState.ToString());
        if (newState == GameState.Pregame)
        {
            titleText.text = TAP_TO_START;
            titleText.gameObject.SetActive(true);
            overlay.SetActive(true);
            enemySpawner.StopSpawning();
            SetLife(3);
        }
        else if (newState == GameState.Gameplay)
        {
            players[activePlayer].transform.position = new Vector3(-7.5f, -2.385f, 0f);
            playerRBs[activePlayer].velocity = Vector2.zero;

            titleText.gameObject.SetActive(false);
            overlay.SetActive(false);

            energyBall.Reset();
            energyBall.StartGathering();

            enemySpawner.SpawnRate = 0.5f;
            enemySpawner.StartSpawning();
        }
        else if (newState == GameState.Endgame && gameState != GameState.Endgame)
        {
            string tp = GAME_OVER;
            tp += "\n<size=10>Energy Collected : " + Mathf.Floor(energyBall.Energy * 1000) + "</size>";
            titleText.text = tp;
            titleText.gameObject.SetActive(true);
            overlay.SetActive(true);
            enemySpawner.StopSpawning();
        }
        gameState = newState;
    }
    
    void ScheduleState(GameState newState, float delay = 0f)
    {
        if (delay > 0f)
            StartCoroutine(ScheduleNextState(newState, delay));
        else
            StartCoroutine(ScheduleStateNextFrame(newState));
    }

    IEnumerator ScheduleStateNextFrame(GameState newState)
    {
        yield return null;
        SetState(newState);
    }

    IEnumerator ScheduleNextState(GameState newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        SetState(newState);
    }

    void SetLife(int newlife)
    {
        life = newlife;
        for (int i = 0; i < lifes.Length; i++)
        {
            if (i < life)
            {
                lifes[i].color = lifesColor[0];
            }
            else
            {
                lifes[i].color = lifesColor[1];
            }
        }
    }

    void Player_OnDamaged()
    {
        SetLife(life - 1);
        energyBall.AddEnergy(-enemySpawner.SpawnRate);
    }

    void Player_OnEnemyKilled()
    {
        energyBall.AddEnergy(enemySpawner.SpawnRate * 0.1f);
    }
}
