using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Enemy
{
    // 넉백, 슬로우 안받음, 직접 못 움직임, 다른 적들이 밀어줘야 움직임

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
