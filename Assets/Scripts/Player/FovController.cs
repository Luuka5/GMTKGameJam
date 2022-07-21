using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovController : MonoBehaviour
{
	float baseFOV;
	float newFov;
	Camera cam;
	[SerializeField] float fovChangeSpeed;

	private void Awake()
	{
		cam = GetComponent<Camera>();
		baseFOV = cam.fieldOfView;
		newFov = baseFOV;
	}

	private void FixedUpdate()
	{
		changeFOV();
	}
	
	void changeFOV()
	{
		cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, newFov, fovChangeSpeed);
	}

	public void changeFov(float changeToBaseFov)
	{
		newFov = baseFOV + changeToBaseFov;
	}
}
