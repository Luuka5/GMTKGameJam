using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int count = 1;
    public float spawnWaitTime = 2;
    private Transform _spawnPoint;
    private EnemyCore[] _enemies;

    private void Awake()
    {
        _spawnPoint = GetComponent<Transform>();
        _enemies = new EnemyCore[count];
    }

    public void Activate()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, _spawnPoint.position, _spawnPoint.rotation) as GameObject;
            _enemies[i] = enemy.GetComponent<EnemyCore>();
            yield return new WaitForSeconds(spawnWaitTime);
        }
    }

    public bool areEnemiesAlive()
    {
        foreach (EnemyCore enemy in _enemies)
        {
            if (enemy == null) continue;
            if (enemy.GetAliveState()) return true;
        }
        return false; 
    }
}
