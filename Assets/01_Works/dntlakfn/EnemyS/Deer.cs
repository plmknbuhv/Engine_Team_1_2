using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Enemy
{
    // �ڱ⺸�� ������ ��������(�� ����), �ڽź��� �տ� �������ִ� ������ ���������� , ó������ �±� ������ �̵��ӵ��� ������
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
