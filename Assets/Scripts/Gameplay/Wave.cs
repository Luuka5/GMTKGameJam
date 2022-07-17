using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

    public int activationTime = -1;
    public GameObject defaultEnemyPrefab;
    private SpawnPoint[] _spawnPoints;

    private void Awake()
    {

        Transform transform = GetComponent<Transform>();
        _spawnPoints = new SpawnPoint[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _spawnPoints[i] = transform.GetChild(i).gameObject.GetComponent<SpawnPoint>();
            if (_spawnPoints[i] != null && _spawnPoints[i].enemyPrefab == null)
            {
                _spawnPoints[i].enemyPrefab = defaultEnemyPrefab;
            }
        }
    }

    public void Activate()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            if (_spawnPoints[i] == null) continue;
            _spawnPoints[i].Activate();
        }
    }

    public bool isWaveOver()
    {
        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            if (spawnPoint == null) return false;
            if (spawnPoint.areEnemiesAlive())
            {
                return false;
            }
        }
        return true;
    }
}
