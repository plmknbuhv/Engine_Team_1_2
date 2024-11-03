using GGMPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    private WaveManager waveManager;
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO enemyType;
    public Transform spawnPoint;
    private float timer;
    public static int enemyCount;
    private int wave = 1;


    private void Awake()
    {
        waveManager = FindAnyObjectByType<WaveManager>();
        enemyType = waveManager.EnemyList[0];
        wave = 1;
        timer = 0;

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
            enemyCount = 0;
            try
            {
                enemyType = waveManager.EnemyList[wave - 1];
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
        var enemy = poolManager.Pop(enemyType) as Enemy;

        enemy.transform.position = spawnPoint.position + new Vector3(Random.Range(-6, 6), 0);
        enemy.transform.localRotation = Quaternion.identity;
        
        enemyCount++;
    }


}
