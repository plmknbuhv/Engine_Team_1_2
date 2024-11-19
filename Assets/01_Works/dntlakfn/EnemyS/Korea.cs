using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Korea : Enemy
{
    // �� ������, �ʵ忡 ������ �������� ü������ ���� �ɾ���, ����

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