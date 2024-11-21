using GGMPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : Enemy
{
    // 보스, 큼, 넉백,스턴 안받음, 느림

    private void Start()
    {
        UniqueSkill();
    }

    protected override void UniqueSkill()
    {
        explosion.transform.localScale *= 3;
    }
    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, 0, action);
    }

    private void OnEnable()
    {
        EnemySpawner.isBossLive = true;

    }

    private void OnDisable()
    {
        EnemySpawner.isBossLive = false;
    }



}
