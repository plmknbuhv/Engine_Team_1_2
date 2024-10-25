using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Dictionary<int, int> Waves = new Dictionary<int, int>();
    public List<GameObject> EnemyList = new List<GameObject>();
    public bool isWaveStart = false;


    private void Awake()
    {
        for(int i = 1; i <= 21; i++)
        {
            Waves.Add(i, i*2);
        }
    }

    public void WaveStart()
    {
        isWaveStart = true;
    }

    public void WaveEnd()
    {
        isWaveStart = false;
    }


}
