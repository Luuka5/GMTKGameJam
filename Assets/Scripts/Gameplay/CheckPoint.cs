using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public Collider checkPointCollider;
    public Transform checkPointSpawn;

    public EnemySpawner enemySpawner;
    public int waveIndex;

    public void Respawn()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        enemySpawner.SetWave(waveIndex);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckPointPlayer player = collision.gameObject.GetComponent<CheckPointPlayer>();
        if (player == null) return;
        player.lastCheckPoint = this;
    }
}
