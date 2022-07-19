using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCore : MonoBehaviour, ICanDie, IDamageable
{
    [SerializeField] public PlayerData playerData;
    public int _health;
    private Transform playerTransform;
    


    private void Awake()
    {
        playerData.playerCore = this;
        _health = playerData.startHealth;
        playerTransform = GetComponent<Transform>();
      

      
    }

    private void ChangeHealth(int amount)
    {
        
        _health += amount;

        
        if (_health <= 0)
            Die();
        if (_health >= playerData.startHealth)
            _health = playerData.startHealth;
    }

    public void TakeDamage(int amount)
    {
        
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
