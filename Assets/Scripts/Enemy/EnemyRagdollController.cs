using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyCore))]
public class EnemyRagdollController : MonoBehaviour, ICanDie
{

	Animator animator;
	EnemyCore enemyCore;


	Vector3 tempPartPosition;

	private void Awake()
	{
		enemyCore = GetComponent<EnemyCore>();
		animator = GetComponent<Animator>();
	}

	void Start()
	{
		setRigidbodyState(false);
	}

	


	public void Die()
	{
		
		RegularDeath(); 

	}

	void RegularDeath()
	{
		animator.enabled = false;
		setRigidbodyState(true);
		
	}

	

	void setRigidbodyState(bool state)
	{
		Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

		foreach (Rigidbody rigidbody in rigidbodies)
		{
			rigidbody.isKinematic = !state;
		}

		Rigidbody parentRigidBody = GetComponent<Rigidbody>();
		if (parentRigidBody) parentRigidBody.isKinematic = state;
	}

	void setColliderState(bool state)
	{
		Collider[] colliders = GetComponentsInChildren<Collider>();

		foreach (Collider collider in colliders)
		{
			collider.enabled = state;
		}

		Collider[] parentColliders = GetComponents<Collider>();
		foreach (Collider collider in parentColliders)
		{
			collider.enabled = !state;
		}

	}

	void setParentColliderState(bool _state)
    {
		Collider[] parentColliders = GetComponents<Collider>();
		foreach (Collider collider in parentColliders)
		{
			collider.enabled = _state;
		}
	}
}
