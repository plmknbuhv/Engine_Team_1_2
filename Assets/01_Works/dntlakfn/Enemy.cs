using System;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int maxHp;
    public int hp;
    private float knockbackPower;
    private bool isGetDamage = false;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Transform target;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = FindAnyObjectByType<Tower>().transform;
        hp = maxHp;
    }

    private void Move()
    {
        if (isGetDamage)
        {
            animator.speed = knockbackPower / 5;
            animator.SetBool("isHit", false);
            rb.velocity = new Vector2(0, -Math.Clamp(knockbackPower -= Time.deltaTime, 0, 10));
            if (knockbackPower <= 0)
            {
                animator.speed = 1f;
                isGetDamage = false;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public virtual void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        this.knockbackPower = knockbackPower;
        isGetDamage = true;

        animator.SetBool("isHit", true);
        

        hp -= damage;
        
        if (action != null)
        {
            action?.Invoke();
        }
    }

    protected virtual void UniqueSkill()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) // ÅÂ½ºÆ®
        {
            GetDamage(10, 3);
        }
    }


}
