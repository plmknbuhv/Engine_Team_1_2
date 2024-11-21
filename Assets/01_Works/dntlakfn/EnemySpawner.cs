using GGMPool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{

    private WaveManager waveManager;
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private List<PoolTypeSO> enemyType;
    public static Action OnSpawned;
    public Transform spawnPoint;
    private float timer;
    public static int enemyCount;
    public static bool isHouseLive = false;
    public static bool isKoreaLive = false;
    public static bool isBossLive = false;
    public int enemySpawnCount;
    private int wave = 1;


    private void Awake()
    {
        waveManager = FindAnyObjectByType<WaveManager>();
        enemyType = waveManager.enemyList[0].enemys;
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
                enemyType = waveManager.enemyList[wave - 1].enemys;
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
        Enemy enemy = null;
        int rand = 0;


        rand = UnityEngine.Random.Range(0, enemyType.Count);
        while (enemyType[rand].typeName == "House")
        {
            if (isHouseLive) rand = UnityEngine.Random.Range(0, enemyType.Count);
            else
            {
                enemy = poolManager.Pop(enemyType[rand]) as Enemy;
                enemy.transform.position = spawnPoint.position + new Vector3(UnityEngine.Random.Range(-6, 6), 0);
                enemy.transform.localRotation = Quaternion.identity;
                enemyType.Remove(enemyType[rand]);
                return;
            }
        }
        while (enemyType[rand].typeName == "Korea")
        {
            if (isKoreaLive) rand = UnityEngine.Random.Range(0, enemyType.Count);
            else
            {
                enemy = poolManager.Pop(enemyType[rand]) as Enemy;
                enemy.transform.position = spawnPoint.position + new Vector3(UnityEngine.Random.Range(-6, 6), 6);
                enemy.transform.localRotation = Quaternion.identity;
                enemyType.Remove(enemyType[rand]);

                return;
            }
        }
        while (enemyType[rand].typeName == "Refrigerator")
        {
            if (isBossLive) rand = UnityEngine.Random.Range(0, enemyType.Count);
            else
            {
                enemy = poolManager.Pop(enemyType[rand]) as Enemy;
                enemy.transform.position = spawnPoint.position + new Vector3(UnityEngine.Random.Range(-6, 6), 0);
                enemy.transform.localRotation = Quaternion.identity;
                enemyType.Remove(enemyType[rand]);

                return;
            }
        }


        enemy = poolManager.Pop(enemyType[rand]) as Enemy;


        enemy.transform.position = spawnPoint.position + new Vector3(UnityEngine.Random.Range(-6, 6), 0);
        enemy.transform.localRotation = Quaternion.identity;

        OnSpawned?.Invoke();
        enemySpawnCount++;
    }


}
