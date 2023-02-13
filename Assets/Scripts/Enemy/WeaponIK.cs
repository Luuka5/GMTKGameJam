using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanBone
{

	//public HumanBodyBones bone;

	public Transform bone;
	[Range(0, 1)]
	public float weight;
}

public class WeaponIK : MonoBehaviour
{

	[SerializeField] Transform targetTransform;
	[SerializeField] Transform aimTransform;
	

	[SerializeField] float iterations;
	[Range(0, 1)]
	[SerializeField] float weight = 1f;
	[SerializeField] HumanBone[] humanBones;


	[SerializeField] float angleLimit = 90f;
	[SerializeField] float angleLimitBlendOutModifier = 50f;
	[SerializeField] float distanceLimit = 1.5f;
	[SerializeField] float rotateSpeed = 1f;

	private bool _isActive = false;
	private bool _isPaused = false;
	private Vector3 _savedTargetPosition;

	EnemyCore enemyCore;

	Transform[] boneTransforms;


    // Start is called before the first frame update
    void Start()
    {
		//Animator animator = GetComponent<Animator>(); 
		//boneTransforms = new Transform[humanBones.Length];

		//for (int i = 0; i<boneTransforms.Length; i++)
		//boneTransforms[i] = animator.GetBoneTransform(humanBones[i].bone);

		enemyCore = GetComponent<EnemyCore>();
		targetTransform = enemyCore.enemySettings.playerData.playerCore.transform;
    }

	public void PauseIK()
    {
		_savedTargetPosition = GetTargetPosition();
		_isPaused = true;
    }

	public void UnPauseIK()
    {
		_isPaused = false;
	}
	

	public void ChangeIKState(bool _newState)
	{ _isActive = _newState;
	 
	}

	Vector3 GetTargetPosition()
	{
		if (_isPaused) return _savedTargetPosition;

		Vector3 targetDirection = targetTransform.position - aimTransform.position;
		Vector3 aimDirection = aimTransform.forward;
		float blendout = 0f;

		float targetAngle = Vector3.Angle(targetDirection, aimDirection);

		if (targetAngle>angleLimit)
		{
		
			blendout += (targetAngle - angleLimit)/ angleLimitBlendOutModifier;
		}

		float targetDistance = targetDirection.magnitude;
		if (targetDistance<distanceLimit)
		{
			
			blendout += distanceLimit - targetDistance;
		}

		Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendout);

		//Vector3 newDirection =  Vector3.MoveTowards(aimDirection, direction, rotateSpeed);

		return aimTransform.position + direction;


	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.J))
          if (!_isPaused) PauseIK();
			  else UnPauseIK(); 
		

    }

    // Update is called once per frame
    void LateUpdate()
    {
		
		if (!_isActive) return;
		if (!enemyCore.GetAliveState()) return;

		

		if (aimTransform == null) return;
		if (targetTransform == null) return;



		Vector3 targetPosition = GetTargetPosition();

		for (int i = 0; i < iterations; i++)
		{
			for (int j = 0; j < humanBones.Length; j++)
			{
				float boneWeight = humanBones[j].weight * weight;
				Transform bone = humanBones[j].bone;
				AimAtTarget(bone, targetPosition, boneWeight);
			}
		}
    }

	void AimAtTarget(Transform bone, Vector3 targetPosition, float weight)
	{
		Vector3 aimDirection = aimTransform.forward;
		Vector3 targetDirection = targetPosition - aimTransform.position;
		Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
		
			Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
			bone.rotation = blendedRotation * bone.rotation;
		

	}

	
	public void SetTargetTransform(Transform target)
	{
		targetTransform = target;
	}

	public void SetAimTransform(Transform aim)
	{
		aimTransform = aim;
	}

}
