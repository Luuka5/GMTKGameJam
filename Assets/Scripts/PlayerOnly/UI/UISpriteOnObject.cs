using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpriteOnObject : MonoBehaviour
{
    [SerializeField] private FindObjectToAim _findObjectToAim;
    [SerializeField] private RectTransform _transform;
    [SerializeField] private Camera _cam;


    private void Update()
    {

           if (_findObjectToAim.objectToAim==null) { _transform.gameObject.SetActive(false);  return; }

        _transform.gameObject.SetActive(true);
        _transform.position = _cam.WorldToScreenPoint(_findObjectToAim.objectToAim.transform.position);

    }

}
