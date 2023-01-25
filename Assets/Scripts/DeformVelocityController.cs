using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformVelocityController : MonoBehaviour
{
    enum Plane { XY, XZ, YZ };
    [SerializeField] private Transform _deformer;
    [SerializeField] private Plane _plane;
    [SerializeField] private float _minDistanceToObject=0.5f;
    [SerializeField] private float _maxDistanceToObject = 0.7f;
    [SerializeField] private Vector2 _maxPositionsOfDeformer;
    
    [SerializeField] private Rigidbody _rigidbodyForSpeed;
    [SerializeField] private Transform _deformableObject;

    [SerializeField] private Vector3 _objectVelocity;
    //[SerializeField] private Vector3 _33objectVelocity;

    [SerializeField] private float _minVelocity;
    [SerializeField] private float _maxVelocity;
    private float _minDistanceToObjectOriginal;
    private float _maxDistanceToObjectOriginal;
    float _additionToMinMaxPos;
    [SerializeField] private SmearFramesController _smearFramesController;
    [SerializeField] private float _additionToMinMaxPosMultiplier;

    private void Awake()
    {
        _minDistanceToObjectOriginal = _minDistanceToObject;
        _maxDistanceToObjectOriginal = _maxDistanceToObject;
    }

    private void Update()
    {

        if (_rigidbodyForSpeed.velocity.magnitude<_minVelocity)
         { _deformer.gameObject.SetActive(false);
            return;
        }


        _deformer.gameObject.SetActive(true);

        _objectVelocity = _rigidbodyForSpeed.velocity;

       Vector3 _projectedObjectVelocity = -Vector3.ProjectOnPlane(_objectVelocity, _deformableObject.transform.forward);

        float _magnitudeDirection = _projectedObjectVelocity.magnitude;

        Vector3 _normilizedRBVelocity = _projectedObjectVelocity.normalized;



        if (_smearFramesController != null)
            UpdateMinMaxPosBasedOnSmeerScale(); 

        

        _magnitudeDirection = Mathf.InverseLerp(_minVelocity, _maxVelocity, _magnitudeDirection);
        _magnitudeDirection = Mathf.Lerp(_minDistanceToObject, _maxDistanceToObject, 1 - _magnitudeDirection);


        Vector3 _deformerPositionDirection = _magnitudeDirection * _normilizedRBVelocity;

        _deformer.position = new Vector3(_deformableObject.position.x + _deformerPositionDirection.x, _deformableObject.position.y + _deformerPositionDirection.y, _deformableObject.position.z + _deformerPositionDirection.z);


    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawRay(_deformableObject.position, _deformableObject.forward);
        Gizmos.DrawLine(_deformableObject.position, _deformableObject.position +_objectVelocity);

        switch (_plane)
        {
            case Plane.XY:
                Gizmos.DrawWireCube(_deformableObject.position, new Vector3(_maxPositionsOfDeformer.x * 2, _maxPositionsOfDeformer.y * 2, 0.1f));
                break;

            case Plane.XZ:
                Gizmos.DrawWireCube(_deformableObject.position, new Vector3(_maxPositionsOfDeformer.x * 2, 0.1f, _maxPositionsOfDeformer.y * 2));
                break;

            case Plane.YZ:
                Gizmos.DrawWireCube(_deformableObject.position, new Vector3(0.1f, _maxPositionsOfDeformer.x * 2, _maxPositionsOfDeformer.y * 2));
                break;

        }
        
        
    }

    private void UpdateMinMaxPosBasedOnSmeerScale()
    {
        _additionToMinMaxPos = (_smearFramesController.GetSmearScaleMultiplier()-1) * _additionToMinMaxPosMultiplier;
        _minDistanceToObject = _minDistanceToObjectOriginal + _additionToMinMaxPos;
        _maxDistanceToObject = _maxDistanceToObjectOriginal + _additionToMinMaxPos*1.2f;
    }
}
