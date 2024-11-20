using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Enemy
{
    // �ڱ⺸�� ������ ��������(�� ����), �ڽź��� �տ� �������ִ� ������ ���������� , ó������ �±� ������ �̵��ӵ��� ������
    protected override void UniqueSkill()
    {
        animator.speed = 2;
        speed *= 1.5f;

    }

    private void Start()
    {
        UniqueSkill();
    }

    public override void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        base.GetDamage(damage, knockbackPower, action);
        speed *= 0.5f;
    }
    private void Update()
    {
        
    }
}
