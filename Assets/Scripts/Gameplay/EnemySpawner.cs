using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int resetWave = 0;
    public EnemySettings enemySettings;
    public List<EnemyCore> enemyList = new List<EnemyCore>();

    private Wave[] _waves;
    private int _waveIndex = 0;
    private int _waveCounter = 0;
    private IEnumerator _coroutine;

    private void Start()
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

    public bool isDone()
    {
        return _waveIndex >= _waves.Length && resetWave < 0;
    }

    public void SetWave(int index)
    {
        if (index < _waves.Length)
        {
            _waveIndex = index;
        }
        else
        {
            _waveIndex = resetWave;
            if (resetWave < 0) return;
        }

        StopCoroutine(_coroutine);
        _coroutine = StartWave();
        StartCoroutine(_coroutine);
    }

    public void NextWave()
    {
        SetWave(_waveIndex + 1);
    }

    public int getWaveCount()
    {
        return _waveCounter;
    }

    private IEnumerator StartWave()
    {
        if (_waveIndex < _waves.Length)
        {
            if (_waves[_waveIndex].activationTime > 0)
            {
                yield return new WaitForSeconds(_waves[_waveIndex].activationTime);
            }
            _waves[_waveIndex].Activate();
        }


        yield return new WaitForSeconds(3);
        while (enemyList.Count > 0)
        {
            yield return new WaitForSeconds(1);
        }

        NextWave();
    }
}
