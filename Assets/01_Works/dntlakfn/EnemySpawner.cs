using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    private WaveManager waveManager;
    private float timer;
    private int enemyCount;
    private int wave = 1;


    private void Awake()
    {
        waveManager = FindAnyObjectByType<WaveManager>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 3 && waveManager.isWaveStart && enemyCount <= waveManager.Waves[wave])
        {
            timer = 0;
            Spawn();
            return;
        }
        if(enemyCount >= waveManager.Waves[wave])
        {
            wave++;
            waveManager.WaveEnd();
        }
    }

    public void Spawn()
    {
        Instantiate(EnemyPrefab, new Vector3(Random.Range(-6, 6), -12), Quaternion.identity);
        enemyCount++;
    }


}
