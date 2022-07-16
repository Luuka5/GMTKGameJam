using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class GrabThrowObject : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _beingGrabbed;
    private int _defaultLayerID;
    [SerializeField] private int _tempLayerID;
    [SerializeField] private float _timeToChangelayer;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _defaultLayerID = gameObject.layer;
    }

    public void Grab(Transform _whereToGrab, float _grabSpeed)
    {
        Vector3 direction = _whereToGrab.position - transform.position;
       _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(direction * _grabSpeed, ForceMode.VelocityChange);
        _beingGrabbed = true;
        StartCoroutine(GrabFlyCorrector(_whereToGrab));

    }

    public void Grabbed(Transform holdPoint, int holdLayerID)
    {
        _beingGrabbed = false;
        transform.SetParent(holdPoint);
        gameObject.layer = holdLayerID;
        _rigidbody.isKinematic = true;
        

    }

    public void Hold(Transform _holdPoint,float _maxSpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, _holdPoint.position,_maxSpeed);

    }

    public void Release()
    {
        transform.SetParent(null);
        gameObject.layer = _tempLayerID;
        StartCoroutine(changeToDefaultLayer());
        _rigidbody.isKinematic = false;
        _beingGrabbed = false;
    }


    IEnumerator changeToDefaultLayer()
    {
        yield return new WaitForSeconds(_timeToChangelayer);
        gameObject.layer = _defaultLayerID;
    }


    public void Push(Vector3 _impulse)
    {
        _rigidbody.AddForce(_impulse, ForceMode.VelocityChange);
    }


    IEnumerator GrabFlyCorrector(Transform _whereToGrab)
    {
        while (_beingGrabbed)
        {
         
            Vector3 _newDirection = Vector3.Normalize( _whereToGrab.position - transform.position);
            _rigidbody.velocity = _newDirection * _rigidbody.velocity.magnitude;

            yield return new WaitForEndOfFrame();

        }


    }


    public void Throw(Vector3 _whereToThrow, float _throwSpeed)
     {
        Vector3 _direction = Vector3.Normalize(_whereToThrow - transform.position);


        Release();
        _rigidbody.AddForce(_direction*_throwSpeed, ForceMode.VelocityChange);
     }

    public void Throw(Vector3 _throwVector)
    {    Release();
        _rigidbody.AddForce(_throwVector, ForceMode.VelocityChange);
    }




}
