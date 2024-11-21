using GGMPool;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Button startBtn;
    public bool isWaveStart = false;
    public static int wave = 1;



    private void Awake()
    {
        
        for(int i = 1; i <= 8; i++)
        {
            if(i == 8) Waves.Add(i, 1);
            else if(i % 2 == 0) Waves.Add(i, i*5);
            else Waves.Add(i, i * 3);
        }
        
    }

    private void Start()
    {
        WaveStart();
    }

    public void WaveStart()
    {
        
        if(!isWaveStart)
        {
            isWaveStart = true;
            startBtn.gameObject.SetActive(false);

            t.GetComponent<TextMeshProUGUI>().text = "Wave " + wave;

            StartCoroutine(t.FadeInOut(0, 0f, 3));
            

            
        }
    }

    public void WaveEnd()
    {
        startBtn.gameObject.SetActive(true);
        wave++;
        isWaveStart = false;
    }

   
}
