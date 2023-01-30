using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObjectToAim : MonoBehaviour
{

	[SerializeField] private float _range;
	[SerializeField] private Camera _cam;
	[SerializeField] private LayerMask _layer;
	[SerializeField] private float _radius;
	[SerializeField] private float baseLengthOfOverlapCapsule;
	[SerializeField] public GameObject objectToAim;
	private bool _searchingEnabled;

	


	private void Start()
	{
		//StartCoroutine(FindObject());
	}

	private void Update()
	{
		if (_searchingEnabled == false) return;

		RaycastHit hit;
		Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));


		//Raycast to Point to place a OverLapSphere
		if (Physics.Raycast(ray, out hit, _range))
		{

			Collider[] hitColliders = Physics.OverlapBox(transform.position + (hit.point - transform.position) / 2, new Vector3(3,3, Vector3.Distance(transform.position, hit.point)), _cam.transform.rotation); //Physics.OverlapSphere(hit.point, _radius, _layer); //Find All closest Objects to hitpoint;
		


			float distanceToCol = Mathf.Infinity;
			objectToAim = null;


			foreach (Collider collider in hitColliders)
			{

				if (collider.GetComponent<GrabThrowObject>() != null)
				{
					Renderer _render = collider.GetComponent<Renderer>();
					if (_render == null) _render = collider.GetComponentInChildren<Renderer>();

					if (_render != null)
					{
						if (true)//(_render.isVisible)
						{

							float newDistance = Vector3.Distance(collider.transform.position, transform.position);
							if (newDistance < distanceToCol)
							{
								distanceToCol = newDistance;
								objectToAim = collider.gameObject;
							}
						}
					}
				}

			}


			/*
			Collider[] hitColliders2 = Physics.OverlapSphere(hit.point, _radius);

			foreach (Collider collider in hitColliders2)
			{

				if (collider.GetComponent<GrabThrowObject>() != null)
				{

					float newDistance = Vector3.Distance(collider.transform.position, transform.position);
					if (newDistance < distanceToCol)
					{
						distanceToCol = newDistance;
						objectToAim = collider.gameObject;
					}
				}

			}

			if (objectToAim == null)
						Debug.Log(hitColliders.Length);
			*/

		}
	}

    private void OnDrawGizmos()
    {

		RaycastHit _hit;
		Ray _ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

		if (Physics.Raycast(_ray, out _hit, _range))
		{

		

			Collider[] hitColliders = Physics.OverlapSphere(_hit.point, _radius, _layer); //Find All closest Objects to hitpoint;

			//Gizmos.DrawLine(_cam.transform.position, _hit.point);
		//	Gizmos.DrawWireSphere(_hit.point, _radius);

			

			//Gizmos.DrawCube(transform.position + (_hit.point - transform.position) / 2, new Vector3(Vector3.Distance(transform.position, _hit.point), 10, 10));






		}
	}

	public void DisableSearching()
    {
		_searchingEnabled = false;
		objectToAim = null;
	}

	public void EnableSearching()
    {
		_searchingEnabled = true;
    }
}

