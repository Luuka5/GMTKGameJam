using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoover : MonoBehaviour
{

    public enum MooverStates {Standart, TeleportMove}

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravityValue;
    [SerializeField] private Transform _orientation;
    [SerializeField] private float _coyoteFrames;
    [SerializeField] private float _groundDrag;
    [SerializeField] private float _airDrag;
    private MooverStates _state = MooverStates.Standart;
    private Vector3 _physicsVector;
    private Vector3 _controllerVector;
    private CharacterController _characterController;
    private Vector3 _moveVector;
    private bool _grounded;
    private bool _enabledPhysics=true;
    private bool _enabledControls = true;
    private IEnumerator _moveToPoint;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        if (_characterController == null) Debug.Log("No Character Controller Connected");
        StartCoroutine(isGroundedCheck());
    }

    private void Update()
    {
       

        if (_characterController == null) return;



        if (_enabledControls)
        {
            ApplyControls();

            if (Input.GetButtonDown("Jump") && _grounded)
            {
                _physicsVector.y += Mathf.Sqrt(_jumpHeight * -1 * _gravityValue);

            }
        }


        _controllerVector = Quaternion.Euler(0, _orientation.eulerAngles.y, 0) * _controllerVector;

        _moveVector = _physicsVector + _controllerVector;



       

        

        Move(_moveVector);



        if (_enabledPhysics)
        {
            GravitySolver();
            ApplyDrag();
            GravityApplyer();
        }
    }



    public void DisableControls()
    {
        _controllerVector = Vector3.zero;
        _enabledControls = false;
    }

    public void EnableControls()
    {
        _enabledControls = true;
    }


    public void DisablePhysics()
    {
        _physicsVector = Vector3.zero;
        _enabledPhysics = false;
    }

    public void EnablePhysics()
    {
        _enabledPhysics = true;
    }



    private void Move(Vector3 _direction)
    {
        
        _characterController.Move(_direction*Time.deltaTime);
    }

    private void ApplyControls()
    {
        float _verticalInput = Input.GetAxisRaw("Vertical");
        float _horizontalInput = Input.GetAxisRaw("Horizontal");

        _controllerVector = Vector3.right * _horizontalInput * _speed + Vector3.forward * _verticalInput * _speed; ;

    }
    private void ApplyDrag()
    {
        Vector3 _horizontalPhysicsVector = new Vector3(_physicsVector.x, 0, _physicsVector.z);

        if (_grounded)
        {
            if (_horizontalPhysicsVector.magnitude<=_groundDrag * Time.deltaTime) { _physicsVector = new Vector3(0, _physicsVector.y, 0); return; }

           _physicsVector -= _horizontalPhysicsVector.normalized * _groundDrag * Time.deltaTime;

            return; 
        }

        if (!_grounded)

        {

            if (_horizontalPhysicsVector.magnitude <= _airDrag * Time.deltaTime) { _physicsVector = new Vector3(0, _physicsVector.y, 0); return; }

            _physicsVector -= _horizontalPhysicsVector.normalized * _airDrag * Time.deltaTime;

            return;

        }
      
    }

    private void GravitySolver()
    {
        if (_grounded && _moveVector.y < 0)
        {
            _physicsVector.y = _gravityValue * 3 * Time.deltaTime;
           
        }

    }

    private void GravityApplyer()
    {

        _physicsVector.y += _gravityValue * Time.deltaTime;

    }

    IEnumerator isGroundedCheck()
    {
        while (true)
        {
            _grounded = _characterController.isGrounded;

            if (_grounded)
            {
                for (int i = 0; i < _coyoteFrames; i++)
                    yield return new WaitForFixedUpdate();
            }

            yield return new WaitForFixedUpdate();        }
    }

    public MooverStates GetState()
    { return _state; }

    public void NewPhysicsVelocity(Vector3 _newVelocity)
    {
        Debug.Log("New Physics = " + _newVelocity);
        _physicsVector = _newVelocity;
    }

    public IEnumerator  MoveToPoint(Vector3 _pointDestination, float _speed, float minDistanceToStop)
    {
        _state = MooverStates.TeleportMove;

        DisablePhysics();
        DisableControls();
         while(Vector3.Distance(_pointDestination, transform.position)>minDistanceToStop)

        {
            transform.position = Vector3.MoveTowards(transform.position, _pointDestination, _speed*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        EnablePhysics();
        EnableControls();

        _state = MooverStates.Standart;

    }
   
}
