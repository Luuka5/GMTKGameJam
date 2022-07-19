	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DiceCore))]
public class ObjectThatCanDamage : MonoBehaviour
{

	[SerializeField] float maxDamage;
	[SerializeField] float minDamageSpeed;
	[SerializeField] float maxDamageSpeed;
	private DiceCore _diceCore;



	private Rigidbody _rigidbody;

	public float speed;
	public Vector3 direction;

    private void Awake()
    {
		_rigidbody = GetComponent<Rigidbody>();
		_diceCore = GetComponent<DiceCore>();
    }
    private void LateUpdate()
	{
		speed = _rigidbody.velocity.magnitude;
		direction = _rigidbody.velocity.normalized;

	}

	public int GetDamage()
	{
		
		return Mathf.CeilToInt(Mathf.Clamp(maxDamage * speed / (maxDamageSpeed - minDamageSpeed), 0, maxDamage));
	}

	public int GetDamage(int enemyNumber)
    {
		switch(_diceCore.ActiveDiceSide)
        {
			case 1:
				if (enemyNumber == 3) return 10;
				else return 5;
			case 6:
				if (enemyNumber == 3) return 10;
				else return 5;
			case 2:
				if (enemyNumber == 2) return 10;
				else return 5;
			case 5:
				if (enemyNumber == 2) return 10;
				else return 5;
			case 4:
				if (enemyNumber == 1) return 10;
				else return 5;
			 default:_diceCore.playerData.playerCore.TakeDamage(-_diceCore.playerData.healFormCrossSide); return 10;
		}
    }

	public void OnKill()
    {
		StartCoroutine(ContinueSpeed(direction*speed));
		_diceCore.OnKill();
    }


	public void ActivateContinueSpeed()
    {
		StartCoroutine(ContinueSpeed(direction * speed));
	}


	IEnumerator ContinueSpeed(Vector3 _speed)
    {
		_rigidbody.velocity = Vector3.zero;
		yield return new WaitForEndOfFrame();
		_rigidbody.velocity = Vector3.zero;
		yield return new WaitForEndOfFrame();

		_rigidbody.velocity = _speed;
    }
}
