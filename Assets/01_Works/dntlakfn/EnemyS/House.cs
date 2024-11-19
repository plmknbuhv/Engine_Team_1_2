using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Enemy
{
    // �˹�, ���ο� �ȹ���, ���� �� ������, �ٸ� ������ �о���� ������

    private void OnEnable()
    {
        EnemySpawner.isHouseLive = true;
        UniqueSkill();
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
