using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCore : MonoBehaviour, ICanDie, IDamageable
{
    [SerializeField] public PlayerData playerData;
    private int _health;


    private void Awake()
    {
        playerData.playerCore = this;
        _health = playerData.startHealth;
    }

    private void ChangeHealth(int amount)
    {
        
        _health += amount;

        
        if (_health <= 0)
            Die();
    }

    public void TakeDamage(int amount)
    {

        ChangeHealth(-amount);
        
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
