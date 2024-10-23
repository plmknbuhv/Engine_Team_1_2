using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] private Enemy owner;

    private void Awake()
    {
        owner = GetComponentInParent<Enemy>();
    }

    private void Update()
    {
        transform.localScale = new Vector3(0.8f * (owner.hp / owner.maxHp), transform.localScale.y);
    }

}
