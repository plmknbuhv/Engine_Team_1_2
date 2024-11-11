using GGMPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : Enemy, IPoolable
{
    [field: SerializeField] public PoolTypeSO PoolType { get; set; }
    
    public GameObject GameObject => gameObject;

    public void ResetItem()
    {
        
    }

    public void SetUpPool(Pool pool)
    {
        
    }

    protected override void UniqueSkill()
    {
        // 영역전개해서 맵을 빙판으로 바꿈
    }
    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, 0, action);
    }


    private void Update()
    {
        
        
    }


    
}
