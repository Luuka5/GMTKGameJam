using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmearFramesController : MonoBehaviour
{
    [SerializeField] private GameObject _original;
    [SerializeField] private GameObject _smearFramesBase;
    [SerializeField] private GameObject _meshRendererBase;
    [SerializeField] private float _velocityDevider;
    [SerializeField] private float _minSpeedToSmeer;
    private float _smearScaleMultiplier;
    private Rigidbody _originalRB;
    private Vector3 _baseScale;

    // Start is called before the first frame update
    void Start()
    {
        _originalRB = _original.GetComponent<Rigidbody>();
        _baseScale = _smearFramesBase.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        

        _smearFramesBase.transform.rotation = Quaternion.LookRotation(_originalRB.velocity, Vector3.up);
        _meshRendererBase.transform.rotation = _original.transform.rotation;
        _smearScaleMultiplier = Mathf.Max(_originalRB.velocity.magnitude / _velocityDevider, 1);
        float _smearRendererLocalZPosition = -(_smearScaleMultiplier - 1) / 1000;
        // _smearFramesBase.transform.localScale = new Vector3(_baseScale.x/(Mathf.Max(_smearScaleMultiplier/5,1)), _baseScale.y / (Mathf.Max(_smearScaleMultiplier / 5, 1)), _baseScale.z* _smearScaleMultiplier);

        if (_originalRB.velocity.magnitude < _minSpeedToSmeer)
            _smearScaleMultiplier = 1;

        _smearFramesBase.transform.localScale = new Vector3(_baseScale.x, _baseScale.y, _baseScale.z * _smearScaleMultiplier);
        _meshRendererBase.transform.localPosition = new Vector3(0, 0, _smearRendererLocalZPosition);

    }

    public float GetSmearScaleMultiplier()

    {
        return _smearScaleMultiplier;
    }
}
