using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyBrain : MonoBehaviour
{
	[SerializeField] NavMeshAgent agent;
	[SerializeField] public  Animator animator;
	[SerializeField] float runAwayDistance;
	[SerializeField] float shootDistance;
	[SerializeField] float chaseDistance;
	[SerializeField] float additionalDistance;

	[SerializeField] Vector3 playerPosition;
	[SerializeField] float playerPositionUpdateTime = 0.1f;

	[SerializeField] float speed;
	[SerializeField] float rotationSpeed = 10f;
	[SerializeField] float aimTime;

	[SerializeField] EnemyRangedController enemyRangedController;

	[SerializeField] public Transform player;

	[SerializeField] float maxLookingAngle = 50f;

	[SerializeField] WeaponIK weaponIK;

	[SerializeField] float distanceBehaviourRandomness;
	[SerializeField] float speedRandomness;

	float distanceToPlayer;

	public enum States {Idle, Chase, Attack, RunAway, Aim, Die};
	States state = States.Idle;


    private void Awake()
    {
		runAwayDistance += Random.Range(-distanceBehaviourRandomness, distanceBehaviourRandomness);
		shootDistance += Random.Range(-distanceBehaviourRandomness, distanceBehaviourRandomness);
		chaseDistance += Random.Range(-distanceBehaviourRandomness, distanceBehaviourRandomness);
		speed += Random.Range(-speedRandomness, speedRandomness);
		enemyRangedController._rangedEnemyBrain = this;
	}

    private void FixedUpdate()
	{
		distanceToPlayer = GetDistanceToPlayer();

		//if (state == States.Attack) RotateTowardsPlayer();
	}

	
	private void Start()
	{
		player = enemyRangedController.enemyController.enemySettings.playerData.playerCore.transform;
		agent.updateRotation = true;
			distanceToPlayer = GetDistanceToPlayer();
		
		ChangeState(States.Idle);
	}

	float GetDistanceToPlayer()
	{
		return Vector3.Distance(transform.position, player.position);
	}

	bool LookingAtPlayer()
	{
		Vector3 directionAtPlayer = player.position - transform.position;
		Vector3 forwardDirection = transform.forward;
		directionAtPlayer = Vector3.Scale(directionAtPlayer, new Vector3(1, 0, 1));
		forwardDirection = Vector3.Scale(forwardDirection, new Vector3(1, 0, 1));
		float angle = Vector3.Angle(forwardDirection, directionAtPlayer);
		
		return angle < maxLookingAngle;
		
	}
	
	public void Die()
    {
		ChangeState(States.Die);
    }

	public void ChangeState(States newState)
	{
		state = newState;

		switch (state)
		{
			case States.Idle:
				StopAllCoroutines();
				StartCoroutine(Idle());
				break;

			case States.Chase:
				StopAllCoroutines();
				StartCoroutine(Chase());
				break;

			case States.RunAway:
				StopAllCoroutines();
				StartCoroutine(RunAway());
				break;

			case States.Attack:
				StopAllCoroutines();
				StartCoroutine(Attack());
				break;

			case States.Die:
				agent.speed = 0;
				StopAllCoroutines();
				break;
		}
	}




	IEnumerator Idle()
	{
		weaponIK.ChangeIKState(false);
		agent.speed = 0;
		animator.SetBool("Running", false);
		agent.destination = transform.position;

		while (distanceToPlayer > chaseDistance)
			yield return new WaitForEndOfFrame();

		ChangeState(States.Chase);
	}

	IEnumerator Chase()
	{
		agent.speed = speed;
		
		animator.SetBool("Running", true);
		while ((distanceToPlayer > shootDistance - additionalDistance || !LookingAtPlayer()) && distanceToPlayer > runAwayDistance-additionalDistance)
		{
			weaponIK.ChangeIKState(false);
			agent.destination = player.position;
			yield return new WaitForEndOfFrame();
		}

		if (distanceToPlayer <= runAwayDistance)
			ChangeState(States.RunAway);
		else
			ChangeState(States.Attack);

	
			

	}



		IEnumerator Attack()
	{
		agent.speed = 0;
		weaponIK.ChangeIKState(true);
		animator.SetBool("Running", false);
		agent.destination = transform.position;
		enemyRangedController.SetAutoShootState(true);

		while ((distanceToPlayer < shootDistance + additionalDistance && distanceToPlayer > runAwayDistance - additionalDistance && LookingAtPlayer()))
		{
			


			


			yield return new WaitForEndOfFrame();
		}

		enemyRangedController.SetAutoShootState(false);

		if (distanceToPlayer <= runAwayDistance)
			ChangeState(States.RunAway);
		else
			ChangeState(States.Chase);


	}

	IEnumerator RunAway()
	{
		weaponIK.ChangeIKState(false);
		//agent.speed = speed;
		agent.speed = speed;
		animator.SetBool("Running", false);
		while (distanceToPlayer < runAwayDistance)
		{
				Vector3 playerDirection = player.position - transform.position;
				agent.destination = transform.position - playerDirection.normalized*(runAwayDistance+1); 
			//	yield return new WaitForFixedUpdate();


			yield return new WaitForEndOfFrame();
		}

			if (distanceToPlayer<shootDistance && LookingAtPlayer())
			ChangeState(States.Attack);
			else
			ChangeState(States.Chase);
		
	}


    private void OnDrawGizmosSelected()
 
	{
		Gizmos.DrawWireSphere(transform.position, chaseDistance);
		Gizmos.DrawWireSphere(transform.position, shootDistance);

		Gizmos.DrawWireSphere(transform.position, runAwayDistance);

		Gizmos.color = Color.red;
		Gizmos.DrawCube(agent.destination, Vector3.one * 0.5f + Vector3.up * 5);

	}

	IEnumerator UpdatePlayerDestination()
	{
		while (true)
		{
			playerPosition = player.position;
			yield return new WaitForSeconds(playerPositionUpdateTime);
		}

	}

	void RotateTowardsPlayer()
	{
		Vector3 directionAtPlayer = player.position - transform.position;
		Vector3 forwardDirection = transform.forward;
		directionAtPlayer = Vector3.Scale(directionAtPlayer, new Vector3(1, 0, 1));
		forwardDirection = Vector3.Scale(forwardDirection, new Vector3(1, 0, 1));
		Vector3 newDirection = Vector3.MoveTowards(forwardDirection, directionAtPlayer, rotationSpeed);
		transform.LookAt(newDirection);
	}

   
}
