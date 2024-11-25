using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polarbear : Enemy
{
    // 채력 많음, 느림, 넉백 절반만 받음

    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, knockbackPower/5, action);
    }
}
