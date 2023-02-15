using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    private float _prepareTime;
    private float _shootTime;
    private bool _isShooting;
    [SerializeField] private EnemyCore _enemyCore;
    [SerializeField] private LayerMask _laserLayersToStop;
    [SerializeField] private LayerMask _laserLayersToHit;
    [SerializeField] private VisualEffect _laserEffect;
    [SerializeField] private string _distanceEffectPropertyName = "Distance";
    [SerializeField] private float _maxDistnaceLaser;
    [SerializeField] private Transform _shootingPointTransform;
    [SerializeField] private bool _autoShootAtStart;

    private void Start()
    {
        if (_autoShootAtStart)
        Shoot(0, 50);
    }

    public void Shoot(float _prepareTime, float _shootTime)
    {
        StartCoroutine(PrepareToShoot(_prepareTime, _shootTime));
       
    }

    IEnumerator PrepareToShoot(float _prepareTime, float _shootTime)
    {
        _enemyCore?.weaponIK.PauseIK();



        yield return new WaitForSeconds(_prepareTime);

        _isShooting = true;
        StartCoroutine(Shoot(_shootTime));
        yield return new WaitForSeconds(_shootTime);
        _isShooting = false;

    }

  
   


    IEnumerator Shoot(float _shootTime)
    {
        _laserEffect.Play();
        float _distance = _maxDistnaceLaser;
        
        while (_isShooting)
        {
            RaycastHit ray;
            if (Physics.Raycast(_shootingPointTransform.position, _shootingPointTransform.forward, out ray, Mathf.Infinity, _laserLayersToStop,QueryTriggerInteraction.Ignore))
                _distance = ray.distance;
                
                
                _laserEffect.SetFloat(_distanceEffectPropertyName, _distance);
            


            Collider[] _hitColliders = Physics.OverlapBox(_shootingPointTransform.position+_shootingPointTransform.forward*_distance/2, new Vector3(1,1,_distance),_shootingPointTransform.rotation, _laserLayersToHit);
            foreach (Collider _collider in _hitColliders)
            {
                _collider.GetComponent<IDamageable>()?.TakeDamage(1);
            }

            yield return new WaitForFixedUpdate();
        }
        _enemyCore?.weaponIK.UnPauseIK();
    }
    
    
}
