using GGMPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy, IPoolable
{
    [field: SerializeField] public PoolTypeSO PoolType { get; set; }

    public GameObject GameObject => gameObject;

    public void ResetItem()
    {

    }

    public void SetUpPool(Pool pool)
    {

    }

    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, knockbackPower, action);
    }

    

    private void Update()
    {
        //transform.LookAt(target.position);

        if (Input.GetKeyDown(KeyCode.A)) // ÅÂ½ºÆ®
        {
            GetDamage(10, 3);
        }

    }

}
