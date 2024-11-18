using GGMPool;
using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour, IPoolable
{
    public float speed;
    public int maxHp;
    public int hp;
    public Explosion explosion;
    private float knockbackPower;
    private bool isGetDamage = false;
    public bool isStun = false;
    private bool isDead = false;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Transform target;
    protected int dropGold;
    

    [field: SerializeField] public PoolTypeSO PoolType { get; set; }

    public GameObject GameObject => gameObject;

    [SerializeField] private PoolManagerSO poolManager;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = FindAnyObjectByType<Tower>().transform;
        
    }
    private void OnEnable()
    {
        hp = maxHp;
    }

    private void Move()
    {
        if (isStun)
        {
            GetStun(5);
            return;
        }
        if (isGetDamage)
        {
            
            animator.SetBool("isHit", false);
            animator.speed = 1.7f;
            rb.velocity = new Vector2(0, -Math.Clamp(knockbackPower -= 1.5f, 0, 10));
            if (knockbackPower <= 0)
            {
                animator.speed = 1f;

                isGetDamage = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
        
    }

    public virtual void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        this.knockbackPower = knockbackPower;
        isGetDamage = true;

        animator.SetBool("isHit", true);
        

        hp -= damage;
        if(hp <= 0)
        {
            isDead = true;
        }
        
        if (action != null)
        {
            action?.Invoke();
        }
    }

    public void GetStun(int time)
    {
        if (isStun) return;
        StartCoroutine(GetStunCoroutine(time));
    }

    IEnumerator GetStunCoroutine(int time)
    {
        isStun = true;
        animator.speed = 0;
        yield return  new WaitForSeconds(time);
        animator.speed = 1;
        isStun = false;
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
        if (Input.GetKeyDown(KeyCode.A)) // �½�Ʈ
        {
            GetDamage(10, 30);
        }
        if (Input.GetKeyDown(KeyCode.S)) // �½�Ʈ
        {
            GetStun(3);
        }
        if (isDead)
        {
            var boom = poolManager.Pop(explosion.PoolType);
            boom.GameObject.transform.position = transform.position;
            //ShopManager.Instance.Gold += dropGold;
            poolManager.Push(this);
            
            
        }
        
    }

    

    public void SetUpPool(Pool pool)
    {
        
    }

    public void ResetItem()
    {
        
    }
}
