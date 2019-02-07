using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpanner : MonoBehaviour
{
    public enum SpawnState {Spawning, Counting, Waiting};
    [System.Serializable]
  public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;

    }
    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    private SpawnState state = SpawnState.Counting;

    void Start()
    {
        waveCountDown = timeBetweenWaves;

    }
    void Update()
    {
        if(waveCountDown<= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
            else
            {
                waveCountDown -= Time.deltaTime;
            }

        }
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.Spawning;
        for(int i = 0; i<_wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.Waiting;

        

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemies: " + _enemy.name);
    }
}
