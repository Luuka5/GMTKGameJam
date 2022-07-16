using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour, ICanDie, IDamageable
{
    [SerializeField] public EnemySettings enemySettings;



	[SerializeField] private int _health;
	private bool _isAlive = true;
	public Animator animator;
	public EnemyRagdollController enemyRagdollController;
	private bool _canTakeDamage = true;
	[SerializeField] ParticleSystem damageParticles;

    private void Start()
    {
	
    }
    public void Die()
	{	
		_isAlive = false;
		enemyRagdollController.Die();
	}

	public void ChangeHealth(int amount)
	{
		_health += amount;
	}

	public void TakeDamage(int amount)
	{
		Debug.Log(amount);

		if (_canTakeDamage)
			ChangeHealth(-amount);

		if (checkDeath()) Die();
		//else damageParticles.Emit(amount / 5);

		StartCoroutine(DamagePause());

	}

	private bool checkDeath()
    {
		if (_health <= 0)
			return true;
		

		return false;

	}

	IEnumerator DamagePause()
	{
		_canTakeDamage = false;
		yield return new WaitForSeconds(enemySettings.damagePauseTime);
		_canTakeDamage = true;
	}

	public bool GetAliveState()
	{
		return _isAlive;
	}
    
}
