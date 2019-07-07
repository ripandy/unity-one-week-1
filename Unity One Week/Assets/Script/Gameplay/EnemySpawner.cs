/* EnemySpawner.cs
 * Author		: Ripandy Adha (ripandy.adha@kadinche.com, ripandy.adha@gmail.com)
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemySpawner : MonoBehaviour
{
    const float MAX_HEIGHT = 30f;

    [SerializeField] GameObject enemyPrefabs;
    [SerializeField] Transform[] charaTransforms;
    
    List<Enemy> enemies = new List<Enemy>();

    [Range(0f, 50f)]
    [SerializeField] float spawnRate = 1f;
    public float SpawnRate
    {
        get { return spawnRate; }
        set { spawnRate = value; spawnInterval = 1f/value; }
    }

    float spawnInterval;

    void Start()
    {
        Observable.Interval(System.TimeSpan.FromMilliseconds(1000f))
            .Subscribe(_ => {
                foreach (var enemy in enemies)
                {
                    enemy.UpdateSpeed();
                }
            }).AddTo(this);
    }

    public void StartSpawning()
    {
        spawnInterval = 1f / SpawnRate;
        StartCoroutine(SpawnCoroutine());
    }

    public void StopSpawning()
    {
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
        StopAllCoroutines();
    }

    IEnumerator SpawnCoroutine()
    {
        while(true) {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, 0f);
        int rVal = Random.Range(0, 2);
        float px = 0f;
        if (rVal == 0) {
            px = 999f;
            foreach (var chara in charaTransforms)
            {
                px = Mathf.Min(chara.position.x, px);
            }
            px -= 20f;
        } else {
            px = -999f;
            foreach (var chara in charaTransforms)
            {
                px = Mathf.Max(chara.position.x, px);
            }
            px += 20f;
        }
        spawnPos.x = px;
        spawnPos.y = Random.Range(-2.385f, MAX_HEIGHT);
        
        Enemy enemy = null;
        int idx = 0;
        bool found = false;
        while (!found && idx < enemies.Count)
        {
            if (!enemies[idx].gameObject.activeInHierarchy)
            {
                found = true;
            }
            else
            {
                idx++;
            }
        }
        if (found) {
            // reuse
            enemy = enemies[idx];
            enemy.transform.position = spawnPos;
            enemy.gameObject.SetActive(true);
        } else {
            // spawn new
            var go = Instantiate(enemyPrefabs, spawnPos, Quaternion.identity);
            enemy = go.GetComponent<Enemy>();
            enemies.Add(enemy);
        }
        enemy.SetTarget(charaTransforms[rVal]);

        SpawnRate += 0.05f;
        Debug.Log("Spawning again in " + spawnInterval + "sec");
    }
}
