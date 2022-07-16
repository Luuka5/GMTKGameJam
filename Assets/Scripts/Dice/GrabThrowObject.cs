using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class GrabThrowObject : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _beingGrabbed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Grab(Vector3 _whereToGrab, float _grabSpeed)
    {

    }

    public void Grabbed()
    {

    }

    public void Hold()
    {

    }

    public void Push(Vector3 _impulse)
    {
        _rigidbody.AddForce(_impulse, ForceMode.VelocityChange);
    }


    IEnumerator GrabFlyCorrector()
    {
        while (_beingGrabbed)
        {
            yield return new WaitForEndOfFrame();

        }
    }


    public void Throw(Vector3 _throwVector)
     {
     }
    public void Release()
    {

    }

    

}
