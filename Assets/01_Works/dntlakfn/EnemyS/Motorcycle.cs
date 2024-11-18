using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motorcycle : Enemy
{
    // 매우빠름, 한방 컷
    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, knockbackPower, action);
        
    }
}
