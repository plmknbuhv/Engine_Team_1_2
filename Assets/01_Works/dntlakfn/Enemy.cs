using GGMPool;
using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IPoolable
{
    public UnityEvent<float, float> OnDamaged;
    public float speed;
    public int maxHp;
    public int hp;
    public Explosion explosion;
    public EnemyHpBar hpBar;
    public GameObject boost;
    private float knockbackPower;
    private bool isGetDamage = false;
    protected bool isStun = false;
    protected bool isSlow = false;
    protected bool isDead = false;
    protected Rigidbody2D rb;
    public Animator animator;
    protected Transform target;
    protected int dropGold;
    protected Vector3 targetVec;
    
    

    [field: SerializeField] public PoolTypeSO PoolType { get; set; }

    public GameObject GameObject => gameObject;

    [SerializeField] protected PoolManagerSO poolManager;
    public static Action OnknockbackStop;

    public virtual void Awake()
    {
        hpBar = GetComponentInChildren<EnemyHpBar>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = FindAnyObjectByType<Tower>().transform;
        OnknockbackStop += KnockBackStop;
        
    }
    public virtual void OnEnable()
    {
        dropGold = Mathf.Clamp(WaveManager.wave/3, 1, 4);
        hp = maxHp;
        hpBar.SetHpBar(hp, maxHp);
        Debug.Log("ü�� ����");
        targetVec = target.position + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0, 0);
        if(EnemySpawner.isKoreaLive)
        {
            boost.SetActive(true);
            speed *= 1.3f;
            animator.speed = 1.3f;
        }
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
            transform.position = Vector2.MoveTowards(transform.position, targetVec, speed * Time.deltaTime);
        }
        transform.position  = new Vector3(transform.position.x, transform.position.y, transform.position.y/10);
        
        
    }

    public virtual void GetDamage(int damage, float knockbackPower, Action action = null)
    {
        this.knockbackPower = knockbackPower;
        isGetDamage = true;

        animator.SetBool("isHit", true);
        

        hp -= damage;
        hpBar.SetHpBar(hp, maxHp);
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

    public virtual void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A)) // �½�Ʈ
        {
            GetDamage(10,10);
        }
        if (Input.GetKeyDown(KeyCode.S)) // �½�Ʈ
        {
            GetStun(2);
        }
        if (Input.GetKeyDown(KeyCode.D)) // �½�Ʈ
        {
            GetSlow(20, 3);
        }
        if (isDead)
        {
            var boom = poolManager.Pop(explosion.PoolType);
            boom.GameObject.transform.position = transform.position;
            ShopManager.Instance.Gold += dropGold;
            var text = poolManager.Pop(ShopManager.Instance.goldPoolType) as GoldText;
            text.transform.SetParent(ShopManager.Instance.shopCanvas.transform);
            text.transform.position = hpBar.transform.position;
            text.SetText(dropGold);
            
            if(EnemySpawner.isKoreaLive)
            {
                if(boost != null)
                {
                    boost.SetActive(false);
                    speed /= 1.3f;
                    animator.speed = 1;
                }
            }



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
        hpBar.SetHpBar(100, 100);
    }
}
