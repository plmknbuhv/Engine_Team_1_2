using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Enemy
{
    // 자기보다 앞으로 가지못함(새 제외), 자신보다 앞에 가까이있는 적들을 빨라지게함 , 처음으로 맞기 전까지 이동속도가 빨라짐
    bool isDamage;
    protected override void UniqueSkill()
    {
        animator.speed = 2;
        speed = 2f;

    }

    private void Start()
    {
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
