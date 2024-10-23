using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float knockbackPower;
    public int maxHp;
    public int hp;
    protected Animator animator;
    protected float timer;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Move()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime);
    }

    public virtual void UniqueSkill()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }
}
