using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSaver : MonoBehaviour
{
    public Transform playerTransform;

    private Transform diceTransform;
    private Rigidbody diceRigidbody;

    private void Awake()
    {
        diceTransform = GetComponent<Transform>();
        diceRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (diceTransform.position.y < -50)
        {
            diceTransform.position = playerTransform.position + new Vector3(0, 2.5f, 0);
            diceRigidbody.velocity = new Vector3(0, 0, 0);
            diceRigidbody.angularVelocity = new Vector3(0, 0, 0);
        }
    }
}
