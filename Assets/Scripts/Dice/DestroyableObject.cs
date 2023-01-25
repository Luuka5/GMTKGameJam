using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour, IDestroyable
{
    [SerializeField] private bool _disableOnDetroy;
    private bool _isDestroyed = false;
    private void Start()
    {
       
            _isDestroyed = false;
    }


    public void Destroy()
    {
        if (_disableOnDetroy)
        {
         //   gameObject.SetActive(false);
            _isDestroyed = true;
            return;
        }

        Destroy(gameObject);
    }

    public void Respawn()
    {
        if (_disableOnDetroy)
        {
            //   gameObject.SetActive(true);
            _isDestroyed = false;
            return;
        }
    }

    public bool GetDestroyedState()
    {
        return _isDestroyed;
    }
}
