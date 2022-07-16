 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedController : MonoBehaviour,ICanShoot
{
	

	[SerializeField] float projectileSpeed;
	[SerializeField] Transform targetTransform;

	[SerializeField] bool autoShoot;

	[SerializeField] bool burstFire;
	[SerializeField] float burstShotsAmount;
	[SerializeField] float timeBetweenBurstShoots;

	[SerializeField] float timeBetweenShots;
	 
	[SerializeField] GameObject projectile;
	[SerializeField] Transform aimTransform;
	IEnumerator burstShootCorutine;
	IEnumerator autoShootCorutine;
	[SerializeField] EnemyCore enemyController;

	bool isShooting = false;

	private void Start()
	{
		burstShootCorutine = BurstShoot(burstShotsAmount);
		autoShootCorutine = AutoShoot();


		StartCoroutine(AutoShoot());
	}

	private void Update()
	{

		if (!enemyController.GetAliveState()) Die();
	}

	public void Shoot(float speed, Vector3 direction)
	{
	
		GameObject bullet = Instantiate(projectile, aimTransform.position,aimTransform.rotation);
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		if (rb)
		{

			rb.useGravity = false;
			rb.AddForce(direction * projectileSpeed, ForceMode.VelocityChange);
			
		}
	}

	


	IEnumerator BurstShoot(float amountOfShots)
	{
		for (float i= 0;i< amountOfShots;i++)
		{
			Shoot(projectileSpeed, aimTransform.forward);
			yield return new WaitForSeconds(timeBetweenBurstShoots);
		}
	}


	IEnumerator AutoShoot()
	{

		while (true)
		{
			isShooting = false;

			yield return new WaitForSeconds(timeBetweenShots);
			if (!autoShoot) continue;

			isShooting = true;

			if (burstFire)
				StartCoroutine( BurstShoot(burstShotsAmount));
			else
				Shoot(projectileSpeed, aimTransform.forward);
			
			
		}

	}

	void Die()
	{
		StopAllCoroutines();


	}

	public bool AimedOnPlayer()
	{
		Ray ray = new Ray(aimTransform.position, aimTransform.forward);
		 Physics.Raycast(ray, Mathf.Infinity, targetTransform.gameObject.layer);
		return true;
	}

	public bool GetShootingState()
	{
		return isShooting;
	}

	public void SetAutoShootState(bool newSate)
	{
		autoShoot = newSate;
	}
}
