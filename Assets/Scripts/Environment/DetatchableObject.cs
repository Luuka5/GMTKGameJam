using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetatchableObject : MonoBehaviour
{
    public int damageThreshold;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectThatCanDamage objectThatCanDamage = collision.gameObject.GetComponent<ObjectThatCanDamage>();
        if (objectThatCanDamage == null) return;

        if (objectThatCanDamage.GetDamage() >= damageThreshold)
        {
            _rigidbody.isKinematic = false;
        }
    }
}
