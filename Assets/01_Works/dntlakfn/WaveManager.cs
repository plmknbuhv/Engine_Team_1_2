using GGMPool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public Dictionary<int, int> Waves = new Dictionary<int, int>();
    public List<PoolTypeSO> EnemyList = new List<PoolTypeSO>();
    [SerializeField] private t t;
    [SerializeField] private Button startBtn;
    public bool isWaveStart = false;
    int wave = 1;



    private void Awake()
    {
        startBtn.gameObject.SetActive(false);
        for(int i = 1; i <= 21; i++)
        {
            Waves.Add(i, i*2);
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
