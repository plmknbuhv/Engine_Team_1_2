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
    int wave = 1;



    private void Awake()
    {
        
        for(int i = 1; i <= 21; i++)
        {
            if(i % 7 == 0) Waves.Add(i, 1);
            else Waves.Add(i, i*2);
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

            t.GetComponent<TextMeshProUGUI>().text = "Wave " + wave++;

            StartCoroutine(t.Show());
        }
    }

    public void WaveEnd()
    {
        startBtn.gameObject.SetActive(true);

        isWaveStart = false;
    }

   
}
