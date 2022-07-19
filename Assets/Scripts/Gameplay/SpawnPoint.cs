using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int totalCount = 1;
    public int currentCount = 0;
    public float spawnWaitTime = 2;
    private Transform _spawnPoint;
    private EnemyCore[] _enemies;

    private void Awake()
    {
        _spawnPoint = GetComponent<Transform>();
        _enemies = new EnemyCore[totalCount];
    }

    public void Activate()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (currentCount = 0; currentCount < totalCount; currentCount++)
        {
            GameObject enemy = Instantiate(enemyPrefab, _spawnPoint.position, _spawnPoint.rotation) as GameObject;
            _enemies[currentCount] = enemy.GetComponent<EnemyCore>();
            yield return new WaitForSeconds(spawnWaitTime);
        }
    }

    public bool areEnemiesAlive()
    {
        if (currentCount < totalCount) return false;
        foreach (EnemyCore enemy in _enemies)
        {
            if (enemy == null) continue;
            if (enemy.GetAliveState()) return true;
        }
        return false;
    }
}
