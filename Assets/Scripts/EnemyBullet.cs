using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private LayerMask _destroyLayers;
    [SerializeField] private int _damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (IsInLayer(collision.gameObject.layer,_destroyLayers))
            Destroy(this.gameObject);

        PlayerCore playerCore = collision.gameObject.GetComponent<PlayerCore>();

        if (playerCore == null) return;

        playerCore.TakeDamage(_damage);
        Destroy(this.gameObject);
    }

   bool IsInLayer(int layer, LayerMask layermask)
    {

            return layermask == (layermask | (1 << layer));
    }

}
