using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    private Dictionary<Type, ITowerComponent> _components;

    private List<Enemy> _attackingEnemy = new List<Enemy>();
    
    private float _damageTimer;
    [SerializeField] private int maxHealth = 5;
    private int _health;
    public bool isDead;
    
    [SerializeField] private FeedbackPlayer hitFeedback;
    
    private Coroutine _coroutine;
    
    public UnityEvent OnDeathEvent;
    public UnityEvent<float, float> OnDamageEvent;

    private int Health
    {
        get => _health;
        set
        {
            _health = value;
            if (value <= 0)
            {
                _health = 0;
                if (_coroutine != null)
                    StopCoroutine(_coroutine);
            }
            OnDamageEvent?.Invoke(_health, maxHealth);
        } 
        
    }

    private void Awake()
    {
        _components = new Dictionary<Type, ITowerComponent>();
    
        GetComponentsInChildren<ITowerComponent>().ToList()
            .ForEach(x => _components.Add(x.GetType(), x));
    
        _components.Values.ToList().ForEach(compo => compo.Initialize(this));
    }

    private void Start()
    {
        StartCoroutine(TakeDamageCoroutine());
        _health = maxHealth;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            _attackingEnemy.Add(other.gameObject.GetComponent<Enemy>());
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            _attackingEnemy.Remove(other.gameObject.GetComponent<Enemy>());
    }

    private IEnumerator TakeDamageCoroutine()
    {
        while (!isDead)
        {
            yield return null;
            
            if (_damageTimer >= 3.0f)
            {
                if (_attackingEnemy.Count > 0)
                {
                    _damageTimer = 0;
                    Health--;
                    _attackingEnemy[0].GetDamage(999999, 0);
                }
                else
                    continue;
            }
            
            _damageTimer += Time.deltaTime;
        }
    }

    public T GetCompo<T>() where T : class
    {
        Type type = typeof(T);
        if (_components.TryGetValue(type,out ITowerComponent compo))
        {
            return compo as T;
        }
    
        return default;
    }
}
