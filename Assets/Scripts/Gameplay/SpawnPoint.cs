using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int totalCount = 1;
    public float spawnWaitTime = 2;
    private bool _isActive = false;
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
        _isActive = true;
        for (int i = 0; i < totalCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, _spawnPoint.position, _spawnPoint.rotation) as GameObject;
            _enemies[i] = enemy.GetComponent<EnemyCore>();
            yield return new WaitForSeconds(spawnWaitTime);
        }
    }

    public bool AreEnemiesAlive()
    {
        if (_isActive) return true;
        foreach (EnemyCore enemy in _enemies)
        {
            if (enemy == null) continue;
            if (enemy.GetAliveState()) return true;
        }
        return false;
    }
}
