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
        Debug.Log(owner.hp / owner.maxHp);
        transform.localScale = new Vector3(Mathf.Clamp(0.8f * ((float)owner.hp / (float)owner.maxHp), 0, 0.8f), transform.localScale.y);
    }

}
