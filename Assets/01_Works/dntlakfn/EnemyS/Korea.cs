using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Korea : Enemy
{
    // 안 움직임, 필드에 있으면 적들한테 체력증가 버프 걸어줌, 토탬

    
    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, 0, action);
    }

    

    protected override void UniqueSkill()
    {
        
        
    }

    private void Start()
    {
        var enemys = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Debug.Log(enemys[0]);
        EnemySpawner.isKoreaLive = true;
    }

    private void OnDisable()
    {
        var enemys = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i].boost != null)
            {
                enemys[i].boost.SetActive(false);
                enemys[i].speed /= 1.5f;
                enemys[i].animator.speed = 1;
            }
            
        }
        EnemySpawner.isKoreaLive = false;

    }



}