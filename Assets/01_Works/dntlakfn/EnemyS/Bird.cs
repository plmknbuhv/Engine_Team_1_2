using GGMPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    // 다른 적들 다 통과함

    protected override void UniqueSkill()
    {
        
    }

    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, knockbackPower, action);
    }

   

}
