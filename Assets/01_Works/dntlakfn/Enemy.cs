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
    public GameObject boost;
    private float knockbackPower;
    private bool isGetDamage = false;
    public bool isStun = false;
    public bool isSlow = false;
    private bool isDead = false;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Transform target;
    protected int dropGold;
    
    

    [field: SerializeField] public PoolTypeSO PoolType { get; set; }

    public GameObject GameObject => gameObject;

    [SerializeField] private PoolManagerSO poolManager;
    public static Action OnknockbackStop;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = FindAnyObjectByType<Tower>().transform;
        OnknockbackStop += KnockBackStop;
        
    }
    private void OnEnable()
    {
        dropGold = Mathf.Clamp(WaveManager.wave/2, 1, 4);
        hp = maxHp;
        Debug.Log("체력 리셋");
    }

    protected virtual void Move()
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
                OnknockbackStop?.Invoke();
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

    public void KnockBackStop()
    {
        rb.velocity = Vector2.zero;
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

    public void GetSlow(float percent, float time)
    {
        if(isSlow) return;
        float currentSpeed = speed;
        speed = currentSpeed * (percent / 100);
        animator.speed = (percent / 100);


        StartCoroutine(GetSlowCoroutine(time, currentSpeed));
    }

    IEnumerator GetSlowCoroutine(float time, float currentSpeed)
    {
        isSlow = true;
        yield return new WaitForSeconds(time);
        speed = currentSpeed;
        animator.speed = 1;
        isSlow = false;
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
        
        if (Input.GetKeyDown(KeyCode.A)) // 태스트
        {
            GetDamage(10,10);
        }
        if (Input.GetKeyDown(KeyCode.S)) // 태스트
        {
            GetStun(2);
        }
        if (Input.GetKeyDown(KeyCode.D)) // 태스트
        {
            GetSlow(20, 3);
        }
        if (isDead)
        {
            var boom = poolManager.Pop(explosion.PoolType);
            boom.GameObject.transform.position = transform.position;

            //ShopManager.Instance.Gold += dropGold;

            isDead = false;
            isGetDamage = false;
            isSlow = false;

            EnemySpawner.enemyCount++;

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
