using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour, ICanDie, IDamageable
{
    [SerializeField] public EnemySettings enemySettings;
	[SerializeField] private RangedEnemyBrain rangedEnemyBrain;
	[SerializeField] private int _health;
	[SerializeField] public int enemyNumber;
	private bool _isAlive = true;
	public Animator animator;
	public EnemyRagdollController enemyRagdollController;
	private bool _canTakeDamage = true;
	[SerializeField] ParticleSystem damageParticles;

	public EnemySpawner enemySpawner;

    private void Start()
    {
		rangedEnemyBrain.player = enemySettings.playerData.playerCore.transform;
		enemySpawner.enemyList.Add(this);
	}

    public void Die()
	{
		_isAlive = false;
		enemyRagdollController.Die();
		rangedEnemyBrain.Die();
		StartCoroutine(Despawn());
		enemySpawner.enemyList.Remove(this);
	}

	public void ChangeHealth(int amount)
	{
		_health += amount;
	}

	public void TakeDamage(int amount)
	{
		Debug.Log("Damege: " + amount);

		if (_canTakeDamage)
			ChangeHealth(-amount);

		damageParticles.Emit(amount * 5);
		if (checkDeath()) Die();
		

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

	IEnumerator Despawn()
    {
		yield return new WaitForSeconds(enemySettings.timeToDespawn);

		while(true)
        {

			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();

			float distanceToPlayer = Vector3.Distance(transform.position, enemySettings.playerData.playerCore.transform.position);
			if (distanceToPlayer > enemySettings.distanceToDespawn) Destroy(this.gameObject);
		}
    }
    
}
