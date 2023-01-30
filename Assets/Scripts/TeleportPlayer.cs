using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    [SerializeField] private float _diceVelocityMultiplier=0.25f;
    [SerializeField] private PlayerCam _playerCam;
    private CharacterMoover _characterMoover;
    private void Start()
    {
       _characterMoover = GetComponent<CharacterMoover>();
    }

    public void Teleport(TeleportPoint _teleportPoint, GameObject _collidedDice)
    {

        Rigidbody _diceRigidbody = _collidedDice.GetComponent<Rigidbody>();


        transform.position = _teleportPoint.transform.position;
        _characterMoover.NewVelocity(_diceRigidbody.velocity * _diceVelocityMultiplier);


        _teleportPoint.DisablePoint();
        
    
    }
}
