using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDrawLine : MonoBehaviour
{
	[SerializeField] float Lenght=50f;

	private void OnDrawGizmos()
	{
		Debug.DrawLine(transform.position, transform.position + transform.forward * Lenght);
	}
}
