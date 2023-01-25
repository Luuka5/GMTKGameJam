using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoover : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravityValue;
    [SerializeField] private Transform _orientation;
    [SerializeField] private float _coyoteFrames;
    private CharacterController _characterController;
    private Vector3 _moveVector;
    private bool _grounded;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        if (_characterController == null) Debug.Log("No Character Controller Connected");
        StartCoroutine(isGroundedCheck());
    }

    private void Update()
    {
        Debug.Log("_grounded="+_grounded);

        if (_characterController == null) return;


        GravitySolver();

        _moveVector = new Vector3(0,_moveVector.y,0);

       // _grounded = _characterController.isGrounded;

        float _verticalInput = Input.GetAxisRaw("Vertical");
        float _horizontalInput = Input.GetAxisRaw("Horizontal");



     

        _moveVector += Vector3.right * _horizontalInput * _speed + Vector3.forward * _verticalInput * _speed; //Move Vectors
     

        if (Input.GetButtonDown("Jump") && _grounded)
        {
            _moveVector.y += Mathf.Sqrt(_jumpHeight * -1 * _gravityValue);
          
        }

        GravityApplyer();

        Move(_moveVector);
    }


    private void Move(Vector3 _direction)
    {
        _direction = Quaternion.Euler(0, _orientation.eulerAngles.y, 0)*_direction;
        _characterController.Move(_direction*Time.deltaTime);
    }

    private void GravitySolver()
    {
        if (_grounded && _moveVector.y < 0)
        {
            _moveVector.y = _gravityValue*3 * Time.deltaTime;
        }

    }

    private void GravityApplyer()
    {

        _moveVector.y += _gravityValue * Time.deltaTime;

    }

    IEnumerator isGroundedCheck()
    {
        while (true)
        {
            _grounded = _characterController.isGrounded;

            if (_grounded)
            {
                for (int i = 0; i < _coyoteFrames; i++)
                    yield return new WaitForEndOfFrame();
            }

            yield return new WaitForFixedUpdate();        }
    }
   
}
