using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStationHolder : MonoBehaviour
{
    [SerializeField] protected Rigidbody holdedRigidbody;
    [SerializeField] protected Transform holdPoint;
    [SerializeField] protected float force;
    [SerializeField] protected bool isHolding = true;
    [SerializeField] protected float maxDistanceToRelease;
    [SerializeField] private bool _respawnObject;
    private int _originalLayer;
    private bool _isHoldedObjectDestroyable;
    private IDestroyable _holdedObjectIDestroyable;
    [SerializeField] private Vector3 addedRotation;

    private void Start()
    {
        _originalLayer = holdedRigidbody.gameObject.layer;

        _holdedObjectIDestroyable = holdedRigidbody.GetComponent<IDestroyable>();
        if (_holdedObjectIDestroyable == null)
        {
            _respawnObject = false;
            Debug.Log("Holded object is not destractable< so _respawnObject was disabled");
            _isHoldedObjectDestroyable = false;
        }
        else
            _isHoldedObjectDestroyable = true;



        StartCoroutine(Hold());

    }

    void Respawn()
    {

        _holdedObjectIDestroyable.Respawn();

        holdedRigidbody.gameObject.SetActive(true);
        holdedRigidbody.position = holdPoint.position;
        holdedRigidbody.rotation = holdPoint.rotation;
        holdedRigidbody.velocity = Vector3.zero;
        holdedRigidbody.gameObject.layer = _originalLayer;
        isHolding = true;
    }

    IEnumerator WaitToRespawn()
    {
        while (!_holdedObjectIDestroyable.GetDestroyedState())
        {
           // Debug.Log("WAIT TO RESPAWN" + _holdedObjectIDestroyable.GetDestroyedState());
            yield return new WaitForSeconds(0.5f);
        }

        Respawn();

    }

    IEnumerator Hold()
    {
        while (true)
        {
            if (isHolding)
            {
             
                Vector3 direction = holdPoint.position - holdedRigidbody.position;
                direction = direction.normalized;



                //ROTATION
                AddRotation(holdedRigidbody);

                //ANTIGRAVITY
                Antigravity(holdedRigidbody);






                holdedRigidbody.AddForce(direction * force, ForceMode.Force);

                CheckRadius();
            }

            yield return new WaitForFixedUpdate();
        }
    }



    void AddRotation(Rigidbody rb)
    {
        rb.AddTorque(addedRotation);
    }

    void Antigravity(Rigidbody rb)
    {
        if (rb.useGravity == true)
            rb.AddForce(-Physics.gravity, ForceMode.Acceleration);
    }

    void CheckRadius()
    {
        float disntace = Vector3.Distance(holdedRigidbody.position, holdPoint.position);
        if (disntace > maxDistanceToRelease) Release();
    }


    void Unparent()
    {
        if (holdedRigidbody != null)
            if (holdedRigidbody.transform.parent == transform)
                holdedRigidbody.transform.parent = null;
    }

    void Release()
    {
        isHolding = false;

        if (_respawnObject) StartCoroutine(WaitToRespawn()); 

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(holdPoint.position, maxDistanceToRelease);
    }


}
