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
    private bool finished = false;

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
        Debug.Log("wave set to  " + _waveIndex);
        StopCoroutine(_coroutine);
        _coroutine = StartWave();
        StartCoroutine(_coroutine);
    }

    public void NextWave()
    {
        if (_waveIndex >= _waves.Length)
        {
            finished = true;
            return;
        }
        SetWave(_waveIndex + 1);
    }

    public bool IsFinished()
    {
        return finished;
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
        Debug.Log("start wave");
        if (_waves[_waveIndex] != null)
        {
            if (_waves[_waveIndex].activationTime != -1)
            {
                yield return new WaitForSeconds(_waves[_waveIndex].activationTime);
                Debug.Log("activate wave " + _waveIndex);
                _waves[_waveIndex].Activate();
            }
        }
    }
}
