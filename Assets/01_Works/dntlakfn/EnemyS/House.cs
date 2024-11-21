using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Enemy
{
    // �˹�, ���ο� �ȹ���, ���� �� ������, �ٸ� ������ �о���� ������

    public override void OnEnable()
    {
        
        EnemySpawner.isHouseLive = true;
        Debug.Log(EnemySpawner.isHouseLive);
        UniqueSkill();
    }

    private void OnDisable()
    {
        EnemySpawner.isHouseLive = false;

    }
    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, 0, action);
    }

    protected override void UniqueSkill()
    {
        explosion.transform.localScale *= 1.5f;
    }

}
