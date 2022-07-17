using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public Transform checkPointSpawn;

    public EnemySpawner enemySpawner;
    public int waveIndex;

    private void Awake()
    {
        checkPointSpawn = GetComponent<Transform>();
    }

    public void Respawn()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        enemySpawner.SetWave(waveIndex);
    }
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision");
        CheckPointPlayer player = collider.gameObject.GetComponent<CheckPointPlayer>();
        if (player == null) return;
        player.lastCheckPoint = this;
        Debug.Log("Last checkPoint changed");
    }
}
