using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCore : MonoBehaviour, ICanDie, IDamageable
{
    [SerializeField] public PlayerData playerData;
    private int _health;
    private Transform playerTransform;
    public CheckPointData checkPointData;


    private void Awake()
    {
        playerData.playerCore = this;
        _health = playerData.startHealth;
        playerTransform = GetComponent<Transform>();
        GameObject checkPoint = GameObject.Find(CheckPoint.lastCheckPointName);

        if (checkPoint != null)
        {
            playerTransform.position = checkPoint.transform.position;
            playerTransform.rotation = checkPoint.transform.rotation;
        }
    }

    private void ChangeHealth(int amount)
    {
        
        _health += amount;

        
        if (_health <= 0)
            Die();
    }

    public void TakeDamage(int amount)
    {
        Debug.Log(checkPointData.lastCheckPointName);
        ChangeHealth(-amount);
        
    }

    private void Update()
    {
        if (playerTransform.position.y < -50)
        {
            Die();
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
