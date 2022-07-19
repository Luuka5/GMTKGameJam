using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Wave[] _waves;
    public int resetWave = 0;
    private int _waveIndex = 0;
    private int _waveCounter = 0;
    private Coroutine _startCoroutine;
    private IEnumerator _coroutine;
    private IEnumerator _checkIsWaveOver;

    private void Start()
    {
        Transform transform = GetComponent<Transform>();
        _waves = new Wave[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _waves[i] = transform.GetChild(i).gameObject.GetComponent<Wave>();
        }

        _coroutine = StartWave();
        _checkIsWaveOver = CheckIsTheWaveOver();

        StartCoroutine(_checkIsWaveOver);
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
        Debug.Log("NextWave");
        SetWave(_waveIndex + 1);
    }

    private IEnumerator CheckIsTheWaveOver()
    {
        while (true)
        {
            if (_waveIndex < _waves.Length) _waveIndex = resetWave;
            if (_waves[_waveIndex].isWaveOver())
            {
                NextWave();
            }
            yield return new WaitForSeconds(1);
        }
    }

    public int getWaveCount()
    {
        return _waveCounter;
    }

    private IEnumerator StartWave()
    {
        _waveCounter++;
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
