using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class GrabThrowObject : MonoBehaviour
{
    public Rigidbody _rigidbody;
    private bool _beingGrabbed;
    private int _defaultLayerID;
    private DiceCore _diceCore;
    [SerializeField] private int _tempLayerID;
    [SerializeField] private float  _timeBeforeDiceReturn;
    [SerializeField] private float _timeToChangelayer;
    [SerializeField] private float _rotationMagnotide;
    [SerializeField]private Vector3 holdUpRotation;
    GravityGun gravityGun;
    
    private void Awake()
    {
        
        _diceCore = GetComponent<DiceCore>();
        _rigidbody = GetComponent<Rigidbody>();
        _defaultLayerID = gameObject.layer;
        _rigidbody.maxAngularVelocity = Mathf.Infinity;
    }
    private void Start()
    {
        gravityGun = _diceCore.playerData.playerCore.GetComponent<GravityGun>();
    }

    public void OnKill()
    {
        StartCoroutine(ReturnDiceAfterTime());
    
    }

    IEnumerator ReturnDiceAfterTime()
    {
        yield return new WaitForSeconds(_timeBeforeDiceReturn);
        RetreveDice();
    }

    void RetreveDice()
    {
        gravityGun.ChangeSelected(this);
        gravityGun.ActivateGrabbing();
    }

    public void Grab(Transform _whereToGrab, float _grabSpeed)
    {
        _diceCore.OnGrab();
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
        switch (_diceCore.ActiveDiceSide)
        {
            case 1: holdUpRotation = new Vector3(0, 90, 0); break;
            case 2: holdUpRotation = new Vector3(0, -270, -270); break;
            case 3: holdUpRotation = new Vector3(-90, -180, 0); break;
            case 4: holdUpRotation = new Vector3(-270, 0, -180);  break;
            case 5: holdUpRotation = new Vector3(0, 90, 90); break;
            case 6: holdUpRotation = new Vector3(-180, -90, 0); break;
        }
        transform.localRotation = Quaternion.Euler( holdUpRotation); //Vector3.MoveTowards(transform.eulerAngles, holdUpRotation, 1f);
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

       


        Vector3 _rotationDirection = new Vector3(Random.Range(0.9f, 1) * Mathf.Sign(Random.Range(-1, 1)), Random.Range(0.9f, 1) * Mathf.Sign(Random.Range(-1, 1)), Random.Range(0.9f, 1) * Mathf.Sign(Random.Range(-1, 1)));
        
        _rigidbody.AddTorque(transform.up*_rotationMagnotide, ForceMode.VelocityChange);
        _rigidbody.AddForce(_direction*_throwSpeed, ForceMode.VelocityChange);
     }

    public void Throw(Vector3 _throwVector)
    {    
        Release();

        Vector3 _rotationDirection = new Vector3(Random.Range(0.3f, 1) * Mathf.Sign(Random.Range(-1, 1)), Random.Range(0.3f, 1) * Mathf.Sign(Random.Range(-1, 1)), Random.Range(0.3f, 1) * Mathf.Sign(Random.Range(-1, 1)));
        _rigidbody.AddTorque(_rotationDirection * _rotationMagnotide, ForceMode.VelocityChange);
        _rigidbody.AddForce(_throwVector, ForceMode.VelocityChange);
    }




}
