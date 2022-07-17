using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GravityGunStates {AimOff, AimOn, Grab, Hold, Release,Throw,Push}
public class GravityGun : MonoBehaviour
{
    [Header("Grab")]
    [SerializeField] private float _grabSpeed;
    [SerializeField] private Transform _holdGrabPoint;
    [SerializeField] private float _grabRadius;
    [SerializeField] private int _holdLayerId;

    [Header("Hold")]
    [SerializeField] private float _maxHoldSpeed;


    [Header("Throw")]
    [SerializeField] private LayerMask _throwCheckRayLayer;
    [SerializeField] private float _throwSpeed;


    [Header("Push")]
    [SerializeField] private float _pushSpeed;
   

    [Header("Additional")]
    [SerializeField] private LayerMask _diceLayer;




    private GravityGunStates _gravGunState;
    [SerializeField] private Camera cam;
    private GrabThrowObject selectedObject;
    bool _clickLMB;
    bool _clickRMB;




    private void Start()
    {
        ChangeGravGunState(AimOff());
    }
    public void ChangeSelected(GrabThrowObject newSelected)
    {
        selectedObject = newSelected;
    }

    public void ActivateGrabbing()
    {
        ChangeGravGunState(Grab());
    }

    private void Update()
    {
        _clickLMB = false;
        if (Input.GetMouseButtonDown(0))
            _clickLMB = true;

        if (Input.GetMouseButtonUp(0))
            _clickLMB = false;


        if (Input.GetMouseButtonDown(1))
            _clickRMB = true;

        if (Input.GetMouseButtonUp(1))
            _clickRMB = false;

    }
    IEnumerator AimOff()
    {
        selectedObject = null;

        _gravGunState = GravityGunStates.AimOff;

        while (true)
        {



            RaycastHit hit;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _diceLayer))
            {

                if (hit.rigidbody == null)
                    continue;

                if (hit.rigidbody.gameObject.tag != "Dice")
                    continue;

                    selectedObject = hit.rigidbody.gameObject.GetComponent<GrabThrowObject>();
                    ChangeGravGunState(AimOn());
                
                   
            }



            yield return new WaitForEndOfFrame();
        }
    }
    
IEnumerator AimOn()
    {
        _gravGunState = GravityGunStates.AimOn;

        while (true)
        {



            RaycastHit hit;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _diceLayer))
            {

                if (hit.rigidbody == null)
                    ChangeGravGunState(AimOff());

                if (hit.rigidbody.gameObject.tag != "Dice")
                    ChangeGravGunState(AimOff());


                    selectedObject = hit.rigidbody.gameObject.GetComponent<GrabThrowObject>();

            }
            else ChangeGravGunState(AimOff());


            if (CheckLMB())    
                ChangeGravGunState(Push());

            if (CheckRMB())
                ChangeGravGunState(Grab());


            yield return new WaitForEndOfFrame();
        }
    }

IEnumerator Grab()
    {
        CheckForSelected();
        _gravGunState = GravityGunStates.Grab;

        selectedObject.Grab(_holdGrabPoint, _grabSpeed);



        while (true)
        {
            if (CheckRMB())
                ChangeGravGunState(Release());


            float _distance = Vector3.Distance(_holdGrabPoint.position, selectedObject.transform.position);

            if (_distance<_grabRadius)
            {
                ChangeGravGunState(Hold());
            }

            yield return new WaitForEndOfFrame();


        }
    }


    IEnumerator Hold()
    {
        CheckForSelected();
        _gravGunState = GravityGunStates.Hold;
        selectedObject.Grabbed(_holdGrabPoint,_holdLayerId);

        while (true)
        {

            selectedObject.Hold(_holdGrabPoint, _maxHoldSpeed);


            if (CheckLMB())
                ChangeGravGunState(Throw());

            if (CheckRMB())
                ChangeGravGunState(ReleasePush());



            yield return new WaitForEndOfFrame();

        }
    }


    IEnumerator Throw()
    {
        CheckForSelected();
        _gravGunState = GravityGunStates.Throw;


        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity,_throwCheckRayLayer))
        {

            selectedObject.Throw(hit.point, _throwSpeed);

            

            ChangeGravGunState(AimOff());

        }
        else
        {

            selectedObject.Throw(cam.transform.forward * _throwSpeed);
         
            ChangeGravGunState(AimOff());

            yield return new WaitForEndOfFrame();

        }
    }
IEnumerator Push()
    {
       
        CheckForSelected();
        _gravGunState = GravityGunStates.Push;
       
            selectedObject.Push(cam.transform.forward*_pushSpeed);
      
            ChangeGravGunState(AimOff());
            yield return new WaitForEndOfFrame();
        
    }

    IEnumerator ReleasePush()
    {
        CheckForSelected();
 
        selectedObject.Throw(cam.transform.forward * 3 + cam.transform.up * 10);
        ChangeGravGunState(Release());
        yield return new WaitForEndOfFrame();
    }

IEnumerator Release()
    {
        CheckForSelected();
        _gravGunState = GravityGunStates.Release;

        selectedObject.Release();
        selectedObject = null;

        ChangeGravGunState(AimOff());
        yield return new WaitForEndOfFrame();
        
    }

private void ChangeGravGunState(IEnumerator enumerator)
    {
        StopAllCoroutines();
        
        StartCoroutine(enumerator);

    }

private void CheckForSelected()
    {
        if (selectedObject == null)
            ChangeGravGunState(AimOff());
    }

 bool CheckLMB()
    {
       bool  _temp = _clickLMB;
        _clickLMB = false;
        return _temp;
    }

    bool CheckRMB()
    {
        bool _temp = _clickRMB;
        _clickRMB = false;
        return _temp;
    }
}
