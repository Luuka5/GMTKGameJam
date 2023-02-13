using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactBanner : MonoBehaviour
{
    [SerializeField] private float _existTime;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private float _fromBaseDistance;
    [SerializeField] private GameObject _objectToMove;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] EnemyCore _enemyCore;


    [SerializeField] AnimancerComponent _animancer;
    [SerializeField] List<AnimationClip> _animations = new List<AnimationClip>();


    private void Start()
    {
        _enemyCore.OnDeath += Activate;
    
   
    }
    private void OnDestroy()
    {
        _enemyCore.OnDeath -= Activate;
    }
    

    public void Activate()
    {
       
        StartCoroutine(ActivateBanner());
    }

        private IEnumerator ActivateBanner()
    {
        Vector3 _toPlayerDirection = ToPlayerDirection();
        //RandomSprite();

        // _animation.Play(_animationsNamesList[Random.Range(0, _animationsNamesList.Count)]);


        //_objectToMove.gameObject.SetActive(true);
        _objectToMove.transform.eulerAngles = ToPlayerRotation();
        Vector3 _tempPosition = transform.position - _toPlayerDirection * _fromBaseDistance;
       // _tempPosition.y = _objectToMove.transform.position.y;
        _objectToMove.transform.position = _tempPosition;
        _animancer.Play(_animations[Random.Range(0, _animations.Count)]);
        yield return new WaitForSeconds(_existTime);

       // _objectToMove.gameObject.SetActive(false);

    }

    private Vector3 ToPlayerDirection()
    {
        Vector3 _toPlayerDirection = _playerData.playerCore.transform.position - transform.position;
        

        _toPlayerDirection = _toPlayerDirection.normalized;

        return _toPlayerDirection;

    }

    private Vector3 ToPlayerRotation()
    {
        Vector3 _toPlayerRotation = Quaternion.LookRotation(ToPlayerDirection()).eulerAngles ;
        _toPlayerRotation.z = 0;
        _toPlayerRotation.x = 0;
        _toPlayerRotation.y += 180;
        return _toPlayerRotation;

    }
}
