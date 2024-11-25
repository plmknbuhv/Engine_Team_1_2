using GGMPool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Enemys
{
    public List<PoolTypeSO> enemys = new List<PoolTypeSO>();
}

public class WaveManager : MonoBehaviour
{
    public Dictionary<int, int> Waves = new Dictionary<int, int>();
    public List<Enemys> enemyList = new List<Enemys>();
    
    
    [SerializeField] private t t;
    [SerializeField] private WaveBtn startBtn;
    public bool isWaveStart = false;
    public static int wave = 1;



    private void Awake()
    {
        for(int i = 1; i <= 8; i++)
        {
            Waves[i] = i % 2 == 0 ? 40 : 20;
        }
        Waves[8] = 1;
        
        
    }


    public void WaveStart()
    {
        
        if(!isWaveStart)
        {
            isWaveStart = true;
            startBtn.Down();

            t.GetComponent<TextMeshProUGUI>().text = "Wave " + wave;

            StartCoroutine(t.FadeInOut(0, 0f, 3));
            

            
        }
    }

    public void WaveEnd()
    {
        startBtn.OnStartGame();

        wave++;
        isWaveStart = false;
    }

   
}
