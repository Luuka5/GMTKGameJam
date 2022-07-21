using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public static string lastCheckPointName;

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
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CheckPoint.lastCheckPointName = name;
            Debug.Log("Last checkPoint changed");
        }
    }
}
