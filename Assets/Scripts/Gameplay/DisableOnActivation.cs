using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnActivation : MonoBehaviour, IActivatable
{
    [SerializeField] private bool _inverseActivation;
    [SerializeField] private GameObject _specialObjectToActivate;
    [SerializeField] private float _waitUntilActivation=0;
    public void Activate()
    {

        StartCoroutine(ActivateCourutine());
    }

    public void Deactivate()
    {

    }

    IEnumerator ActivateCourutine()
    {
        yield return new WaitForSeconds(_waitUntilActivation);

        if (_specialObjectToActivate == null)
            this.gameObject.SetActive(_inverseActivation);
        else
            _specialObjectToActivate.SetActive(_inverseActivation);
    }
}
