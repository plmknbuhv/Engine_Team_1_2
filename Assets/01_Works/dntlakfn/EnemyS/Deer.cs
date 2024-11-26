using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deer : Enemy
{
    
    bool isDamage;
    protected override void UniqueSkill()
    {
        animator.speed = 2;
        speed = 1f;

    }

    public override void OnEnable()
    {
        base.OnEnable();
        UniqueSkill();
        isDamage = false;
    }

    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, knockbackPower, action);
        if(isDamage)
        {
            speed = 0.5f;
        }
        else
        {
            isDamage = true;
        }
        
    }
    
}
