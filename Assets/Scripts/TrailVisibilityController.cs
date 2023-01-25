using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailVisibilityController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbodyForSpeed;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private bool _disableIfMinVelocity;
    [SerializeField] private bool _disableIfRendererNotVisible;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _linecasterLayerMask;
    [SerializeField] private GameObject _originalObject;
    float _timeOriginal;

    [SerializeField] private float _minSpeed; 
    
    //min Speed to start drawing trail
    //float _maxSpeed;

    private void Start()
    {
        _timeOriginal = _trailRenderer.time;
    }

    private void Update()
    {

        UpdateTrailVisibility();
    }

   private void DisableTrail()
    {      
        _trailRenderer.time = 0;
    }

    private void EnableTrail()
    {
        _trailRenderer.time = _timeOriginal;
    }


    private void UpdateTrailVisibility()
    {

        Debug.Log(_originalObject.gameObject.layer + " = " + LayerMask.NameToLayer("Hold"));

        if ((_originalObject.gameObject.layer == LayerMask.NameToLayer("Hold")) || (_originalObject.gameObject.layer == LayerMask.NameToLayer("TempDice")) )
        {
            Debug.Log("YAY!");
            DisableTrail(); return; }

        if (_disableIfRendererNotVisible && !CheckIfVisible())
        { DisableTrail(); return; }

       

        if (_disableIfMinVelocity && (_rigidbodyForSpeed.velocity.magnitude < _minSpeed))
        { DisableTrail(); return; }

       

        

        EnableTrail();
    }

    private bool CheckIfVisible()
    {


        



    return !Physics.Linecast(transform.position, _camera.transform.position, _linecasterLayerMask, QueryTriggerInteraction.Ignore); 
    }

}
