using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private float _timeToReactivate;
    private PlayerCore _playerCore;
    private TeleportPlayer _teleportPlayer;
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Collider _collider;
    private GameObject _collidedDice;


    private void Start()
    {

        _teleportPlayer = _playerData.playerCore.gameObject.GetComponent<TeleportPlayer>();

        if (_teleportPlayer == null)
        { Debug.Log("Can't find Teleport Player in PlayerCore object"); return; }

        if (_playerData == null)
        {
            _playerData = FindObjectOfType<PlayerData>();
            if (_playerData == null) Debug.Log(gameObject + " Can't find PlayerData.");

        }

        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();

        
    }
    private void OnTriggerEnter(Collider other)
   
    {
        if (other.gameObject.tag == "Dice")
        {
           
            _collidedDice = other.gameObject;
           

            _teleportPlayer.Teleport(this, _collidedDice);
        }
    }
    
    public void DisablePoint()
    {
        //_renderer.enabled = false;  

        transform.localScale = Vector3.one * 0.25f;
        _collider.enabled = false;

        StartCoroutine(ReactivatePoint());
    }

    public void EnablePoint()
    {
        // _renderer.enabled = true;

        transform.localScale = Vector3.one * 1f;
        _collider.enabled = true;
    }

    IEnumerator ReactivatePoint()
    {
        if (_timeToReactivate == -1) yield break;

        yield return new WaitForSeconds(_timeToReactivate);

        EnablePoint();
    }
}
