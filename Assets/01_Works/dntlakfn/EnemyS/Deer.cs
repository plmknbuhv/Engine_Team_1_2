using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Enemy
{

    protected override void UniqueSkill()
    {
        animator.speed = 2;

    }

    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, knockbackPower, action);
    }
}
