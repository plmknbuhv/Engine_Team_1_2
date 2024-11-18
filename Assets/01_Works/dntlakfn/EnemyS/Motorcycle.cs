using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motorcycle : Enemy
{
    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, knockbackPower, action);
        Debug.Log("나는 바이크다.");
    }
}
