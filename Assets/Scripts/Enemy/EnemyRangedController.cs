 using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(RangedEnemyBrain))]
public class EnemyRangedController : MonoBehaviour,ICanShoot
{
	enum ShootMode {Single, Burst, Shotgun, UltraLaser}
	
	

	[Header("SHOOT MODE")]
	[SerializeField] private ShootMode _shootMode;

	[Space]

	[Header("BASE")]
	[SerializeField] GameObject projectile;
	[SerializeField] float projectileSpeed;
	[SerializeField] float timeBetweenShots;
	[SerializeField] Transform aimTransform;
	[SerializeField] ParticleSystem _shootFlashParticles;

	[Space]

	[Header("BURST")]
	[SerializeField] float burstShotsAmount;
	[SerializeField] float timeBetweenBurstShoots;
	
	IEnumerator burstShootCorutine;
	IEnumerator autoShootCorutine;

	[Space]

	[Header("SHOTGUN")]
	[SerializeField] private float _shootgunProjectileAmmount;
	[SerializeField] private float _shootgunSpread;

	[Space]

	[Header("EXTRA")]
	[SerializeField] public EnemyCore enemyController;
	[SerializeField] bool autoShoot;

	Transform targetTransform; //Transform of Player
	public RangedEnemyBrain _rangedEnemyBrain;

	bool isShooting = false;

	private void Start()
	{
		burstShootCorutine = BurstShoot(burstShotsAmount);
		autoShootCorutine = AutoShoot();

		targetTransform = enemyController.enemySettings.playerData.playerCore.transform;

		StartCoroutine(AutoShoot());
	}

	private void Update()
	{

		if (!enemyController.GetAliveState()) Die();
	}

	public void Shoot(float speed, Vector3 direction)
	{
	
		GameObject bullet = Instantiate(projectile, aimTransform.position,Quaternion.LookRotation(direction,Vector3.up));
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		if (rb)
		{

			rb.useGravity = false;
			rb.AddForce(direction * projectileSpeed, ForceMode.VelocityChange);
			
		}
		_rangedEnemyBrain.animator.SetTrigger("Shoot");
		ShootFlash();
	}

	private void StartShoot()
    {

		switch (_shootMode)
		{
			case ShootMode.Single:
				Shoot(projectileSpeed, aimTransform.forward);
				break;
			case ShootMode.Burst:
				StartCoroutine(BurstShoot(burstShotsAmount));
				break;
			case ShootMode.Shotgun:
				ShotgunShoot();
				break;
			case ShootMode.UltraLaser:
				break;
		}

	}

	private void ShotgunShoot()
    {
		for(int i=0; i< _shootgunProjectileAmmount;i++)
			Shoot(projectileSpeed, aimTransform.forward + new Vector3(Random.Range(0, _shootgunSpread), Random.Range(0, _shootgunSpread), Random.Range(0, _shootgunSpread)));
		

		
		
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

			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();

			if (!autoShoot) continue;

			isShooting = true;

			StartShoot();
			isShooting = false;

			yield return new WaitForSeconds(timeBetweenShots);
			
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

	void ShootFlash()
	{
		if (_shootFlashParticles != null)
			_shootFlashParticles.Emit(1);
	}
}
