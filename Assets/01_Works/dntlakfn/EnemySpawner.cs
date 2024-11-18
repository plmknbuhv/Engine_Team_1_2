using GGMPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{

    private WaveManager waveManager;
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO[] enemyType;
    public Transform spawnPoint;
    private float timer;
    public static int enemyCount;
    public int enemySpawnCount;
    private int wave = 1;


    private void Awake()
    {
        waveManager = FindAnyObjectByType<WaveManager>();
        enemyType = waveManager.enemyList[0].enemys.ToArray();
        wave = 1;
        timer = 0;

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3 && waveManager.isWaveStart && enemySpawnCount <= waveManager.Waves[wave])
        {
            timer = 0;
            Spawn();
            return;
        }
        if (enemyCount >= waveManager.Waves[wave])
        {
            wave++;
            enemyCount = 0;
            enemySpawnCount = 0;
            timer = 0;
            try
            {
                enemyType = waveManager.enemyList[wave - 1].enemys.ToArray();
            }
            catch
            {
                Debug.Log("¿Ã∞‘ ≥°");
            }

            waveManager.WaveEnd();
        }
    }

    public void Spawn()
    {
        var enemy = poolManager.Pop(enemyType[UnityEngine.Random.Range(0, enemyType.Length)]) as Enemy;

        enemy.transform.position = spawnPoint.position + new Vector3(UnityEngine.Random.Range(-6, 6), 0);
        enemy.transform.localRotation = Quaternion.identity;

        enemySpawnCount++;
    }


}
