using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int totalCount = 1;
    public float spawnWaitTime = 2;
    private Transform _spawnPoint;
    private EnemyCore[] _enemies;
    public Wave wave;

    private void Awake()
    {
        _spawnPoint = GetComponent<Transform>();
        _enemies = new EnemyCore[totalCount];
        wave.AddSpawnPoint(this);
    }

    public void Activate()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < totalCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
            _enemies[i] = enemy.GetComponent<EnemyCore>();
            _enemies[i].enemySpawner = wave.GetEnemySpawner();
            yield return new WaitForSeconds(spawnWaitTime);
        }
    }
}
