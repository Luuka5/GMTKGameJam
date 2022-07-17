using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Wave[] _waves;
    public GameObject prefab;
    private int _waveIndex = 0;
    private Coroutine _startCoroutine;
    private IEnumerator _coroutine;

    private void Awake()
    {
        Transform transform = GetComponent<Transform>();
        _waves = new Wave[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _waves[i] = transform.GetChild(i).gameObject.GetComponent<Wave>();
        }

        _coroutine = StartWave();

        StartCoroutine(_coroutine);
    }

    public void SetWave(int index)
    {
        _waveIndex = index;
        StopCoroutine(_coroutine);
        StartCoroutine(_coroutine);
    }

    public void NextWave()
    {
        SetWave(_waveIndex + 1);
    }

    private void Update()
    {
        if (_waves[_waveIndex].isWaveOver())
        {
            NextWave();
        }
    }

    private IEnumerator StartWave()
    {
        if (_waves[_waveIndex] != null)
        {
            if (_waves[_waveIndex].activationTime != -1)
            {
                yield return new WaitForSeconds(_waves[_waveIndex].activationTime);
            }
            _waves[_waveIndex].Activate();
        }
    }
}
