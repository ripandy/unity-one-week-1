/* EnemySpawner.cs
 * Author		: Ripandy Adha (ripandy.adha@kadinche.com, ripandy.adha@gmail.com)
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefabs;
    [SerializeField] List<Vector3> spawnPoints = new List<Vector3>();
    
    List<GameObject> enemies = new List<GameObject>();
    List<int> inactiveEnemies = new List<int>();

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
        spawnInterval = 1f / SpawnRate;
        StartCoroutine(SpawnCoroutine());
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
        if (inactiveEnemies.Count > 0) {
            // reuse
            int idx = inactiveEnemies[0];
            inactiveEnemies.RemoveAt(0);
            enemies[idx].transform.position = spawnPos;
            enemies[idx].SetActive(true);
        } else {
            // spawn new
            var enemy = Instantiate(enemyPrefabs, spawnPos, Quaternion.identity);
            enemies.Add(enemy);
        }
    }
}
