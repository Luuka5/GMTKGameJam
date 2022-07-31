using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDrawCircle : MonoBehaviour
{[SerializeField] float _radius;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
