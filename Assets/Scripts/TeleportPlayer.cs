using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    [SerializeField] private float _diceVelocityMultiplierHorizontal=0.25f;
    [SerializeField] private float _diceVelocityMultiplierVertical = 0.25f;
    [SerializeField] private PlayerCam _playerCam;
    [SerializeField] private GravityGun _gravityGun;
    [SerializeField] private float _teleportMoveSpeed = 5f;
    private float _minDistanceToStopTeleportMove = 0.2f;
    private CharacterMoover _characterMoover;
    private IEnumerator _teleportMove;
    private IEnumerator _moveToPoint;
    private Rigidbody _diceRigidbody;
    private Collider _diceCollider;


    private void Start()
    {
       _characterMoover = GetComponent<CharacterMoover>();
        _gravityGun = GetComponent<GravityGun>();

        
    }

    public void Teleport(TeleportPoint _teleportPoint, GameObject _collidedDice)
    {
       if(_diceRigidbody!=null) _diceRigidbody.isKinematic = false;
       _diceRigidbody = _collidedDice.GetComponent<Rigidbody>();

        if (_diceCollider != null) _diceCollider.enabled = true;
        _diceCollider = _collidedDice.GetComponent<Collider>();

        GrabThrowObject _diceGrabThrowObject = _collidedDice.GetComponent<GrabThrowObject>();
        if (_diceGrabThrowObject == null) Debug.Log("WTFFFFFF");
       


        if (_teleportMove != null) StopCoroutine(_teleportMove);
        _teleportMove = TeleportMove(_teleportPoint, _diceGrabThrowObject, _diceRigidbody);
         StartCoroutine(_teleportMove);
       


        _teleportPoint.DisablePoint();
        
    
    }

    IEnumerator TeleportMove(TeleportPoint _teleportPoint, GrabThrowObject _diceGrabThrowObject, Rigidbody _diceRigidbody)
    {
        _diceRigidbody.isKinematic = true;
        _diceCollider.enabled = false;
        _diceRigidbody.transform.position = _teleportPoint.transform.position;


        _characterMoover.EnableControls();
        _characterMoover.EnablePhysics();
        if (_moveToPoint!=null) StopCoroutine(_moveToPoint);
        _moveToPoint = _characterMoover.MoveToPoint(_teleportPoint.transform.position, _teleportMoveSpeed, _minDistanceToStopTeleportMove);
        StartCoroutine(_moveToPoint);


        while (_characterMoover.GetState() == CharacterMoover.MooverStates.TeleportMove)
        {
            yield return new WaitForEndOfFrame();
        }

        
        _characterMoover.NewPhysicsVelocity(Vector3.up * _diceVelocityMultiplierVertical);

        _diceRigidbody.isKinematic = false;
        _diceCollider.enabled = true;


        _gravityGun.ForceHold(_diceGrabThrowObject);


    }
}
