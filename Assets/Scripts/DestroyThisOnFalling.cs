using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(DestroyableObject))]
public class DestroyThisOnFalling : MonoBehaviour
{
    DestroyableObject destroyableObject;
    [SerializeField] private float _yCoordToDeastroy;


    private void Start()
    {
        destroyableObject = GetComponent<DestroyableObject>();    
    }

    private void Update()
    {
        if (transform.position.y < _yCoordToDeastroy)
            destroyableObject.Destroy();
    }



}
