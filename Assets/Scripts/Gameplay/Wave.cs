using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public bool autoActivate;
    public int activationTime = -1;
    public GameObject defaultEnemyPrefab;
    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        Transform transform = GetComponent<Transform>();
        enemySpawner = transform.parent.GetComponent<EnemySpawner>();
    }

    public void AddSpawnPoint(SpawnPoint spawnPoint)
    {
        _spawnPoints.Add(spawnPoint);
    }

    public EnemySpawner GetEnemySpawner()
    {
        return enemySpawner;
    }

    public virtual void Activate()
    {
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            _spawnPoints[i].Activate();
        }
    }
}
