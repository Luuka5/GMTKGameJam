using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyCore))]
public class EnemyRagdollController : MonoBehaviour, ICanDie
{
	[SerializeField] private float _frozenTimeOnDeath;

	Animator animator;
	EnemyCore enemyCore;


	Vector3 tempPartPosition;

	Rigidbody[] rigidbodies;
	Collider[] colliders;

	private void Awake()
	{
		enemyCore = GetComponent<EnemyCore>();
		animator = GetComponent<Animator>();
	}

	void Start()
	{
		rigidbodies = GetComponentsInChildren<Rigidbody>();
		 colliders = GetComponentsInChildren<Collider>();


		setRigidbodyState(false);

		enemyCore.OnDeath += Die;
	}

	private void OnDestroy()
	{
		enemyCore.OnDeath += Die;
	}



	public void Die()
	{
		
		RegularDeath();
		

	}

	void RegularDeath()
	{
		Sleep(_frozenTimeOnDeath);
		animator.enabled = false;
		setRigidbodyState(true);
		setParentColliderState(false);
    }
	
	public void Sleep(float _time)
    {
		StartCoroutine(SleepEveryRigidbody(_time));
    }

	IEnumerator SleepEveryRigidbody(float _time)
    {
	
		

			foreach (Rigidbody rigidbody in rigidbodies)
			{
			rigidbody.constraints = RigidbodyConstraints.FreezePosition;
			}


		yield return new WaitForSeconds(_time);


			foreach (Rigidbody rigidbody in rigidbodies)
			{
			rigidbody.constraints = RigidbodyConstraints.None;
		}

		


	}

    void setRigidbodyState(bool state)
	{
		

		foreach (Rigidbody rigidbody in rigidbodies)
		{
			rigidbody.isKinematic = !state;
		}

		Rigidbody parentRigidBody = GetComponent<Rigidbody>();
		if (parentRigidBody) parentRigidBody.isKinematic = state;
	}

	void setColliderState(bool state)
	{
		

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
