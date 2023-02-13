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
    [SerializeField] VisualEffect _laserEffect;
    

   public void Shoot(float _prepareTime, float _shootTime)
    {
        StartCoroutine(PrepareToShoot(_prepareTime, _shootTime));
        if (_enemyCore == null) _enemyCore.GetComponent<EnemyCore>();
    }

    IEnumerator PrepareToShoot(float _prepareTime, float _shootTime)
    {
        _enemyCore.weaponIK.PauseIK();
        yield return new WaitForSeconds(_prepareTime);

        _isShooting = true;
        StartCoroutine(Shoot(_shootTime));
        yield return new WaitForSeconds(_shootTime);
        _isShooting = false;

    }

  


    IEnumerator Shoot(float _shootTime)
    {
        _laserEffect.Play();


        while (_isShooting)
        {

            Collider[] _hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, +_laserLayersToHit);
            foreach (Collider _collider in _hitColliders)
            {
                _collider.GetComponent<IDamageable>()?.TakeDamage(50);
            }

            yield return new WaitForFixedUpdate();
        }
        _enemyCore.weaponIK.UnPauseIK();
    }
}
