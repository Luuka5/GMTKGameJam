using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	EnemyRagdollController enemyRagdollController;
	[SerializeField] EnemyCore enemyCore;
	[SerializeField] Rigidbody rb;
	float maxAngleToDamage = 400f;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		enemyRagdollController = enemyCore.enemyRagdollController;

	}

	private void OnCollisionEnter(Collision collision)
	{
		Vector3 otherSpeed = collision.relativeVelocity - rb.velocity;




		ObjectThatCanDamage objectDamage = collision.gameObject.GetComponent<ObjectThatCanDamage>();


		if (objectDamage)
		{
			Vector3 directionToHitBox = transform.position - collision.transform.position;
			float angle = Vector3.Angle(objectDamage.direction, directionToHitBox);

			if (angle <= maxAngleToDamage)
			{
				
				if (objectDamage.GetDamage() > enemyCore.enemySettings.damageTreasHold)
				{
					int _damage = objectDamage.GetDamage(enemyCore.enemyNumber);//Get real damage
					enemyCore.TakeDamage(_damage);
						


					if (enemyCore.GetAliveState() == false && _damage == 10)
					{
						objectDamage.OnKill();
					}
				}
			}
		}




	}


	public void TakeDamage(int damage)
	{
		enemyCore.TakeDamage(damage);

	}

	public void AffectedByExplosion(float force, float radius, Vector3 position, float damage)
	{




		enemyCore.TakeDamage(Mathf.CeilToInt(damage));



	}

}
