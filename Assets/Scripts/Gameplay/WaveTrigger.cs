using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public Wave wave;
    public EnemySpawner enemySpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerData.playerCore.gameObject.layer)
        {
            enemySpawner.SetWave(wave);
            gameObject.SetActive(false);
        }
    }
}
