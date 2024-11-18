using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] private Enemy owner;
    [SerializeField] private Transform hpbar;
    private void Awake()
    {
        owner = GetComponentInParent<Enemy>();
    }

    private void Update()
    {
        
        hpbar.localScale = new Vector3(Mathf.Clamp(0.115f * ((float)owner.hp / (float)owner.maxHp), 0, 0.115f), hpbar.localScale.y);
    }

}
