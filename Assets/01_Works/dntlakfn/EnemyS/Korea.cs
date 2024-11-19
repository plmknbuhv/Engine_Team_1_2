using System;
using System.Collections;
using System.Collections.Generic;
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
        var enemys = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        for(int i = 0; i < enemys.Length; i++)
        {
            enemys[i].speed *= 1.2f;
        }
        
    }

    private void OnDisable()
    {
        
    }



}