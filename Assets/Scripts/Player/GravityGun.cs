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

    [Header("Throw")]

    [Header("Push")]
    [SerializeField] private float _pushSpeed;
   

    [Header("Additional")]
    [SerializeField] private LayerMask _diceLayer;




    private GravityGunStates _gravGunState;
    [SerializeField] private Camera cam;
    private GrabThrowObject selectedObject;
    bool _clickLMB;




    private void Start()
    {
        ChangeGravGunState(AimOff());
    }

    private void Update()
    {
        _clickLMB = false;
        if (Input.GetMouseButtonDown(0))
            _clickLMB = true;
            
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


            if (_clickLMB)
            {
                _clickLMB = false;
                ChangeGravGunState(Push());
               
            }

            if (Input.GetButtonDown("Fire2"))
                ChangeGravGunState(Grab());


            yield return new WaitForEndOfFrame();
        }
    }

IEnumerator Grab()
    {
        CheckForSelected();
        _gravGunState = GravityGunStates.Grab;
        while (true)
        {
            if (Input.GetButtonDown("Fire2"))
                ChangeGravGunState(Release());

            float distance = Vector3.Distance(_holdGrabPoint.position, selectedObject.transform.position);

            if (distance<_grabRadius)
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

        while (true)
        {




            if (Input.GetButtonDown("Fire1"))
                ChangeGravGunState(Throw());

            if (Input.GetButtonDown("Fire2"))
                ChangeGravGunState(Release());

            yield return new WaitForEndOfFrame();

        }
    }


    IEnumerator Throw()
    {
        CheckForSelected();
        _gravGunState = GravityGunStates.Throw;
        while (true)
        {



            ChangeGravGunState(AimOff());
            yield return new WaitForEndOfFrame();

        }
    }
IEnumerator Push()
    {
        Debug.Log("Push!");
        CheckForSelected();
        _gravGunState = GravityGunStates.Push;
       
            selectedObject.Push(cam.transform.forward*_pushSpeed);
      
            ChangeGravGunState(AimOff());
            yield return new WaitForEndOfFrame();
        
    }

IEnumerator Release()
    {
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
            ChangeGravGunState(Release());
    }
}
