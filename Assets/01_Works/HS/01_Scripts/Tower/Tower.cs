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
    private int _health = 5;
    
    private Coroutine _coroutine;

    private int Health
    {
        get => _health;
        set
        {
            _health = value;
            if (value <= 0)
            {
                OnDeathEvent?.Invoke();
                if (_coroutine != null)
                    StopCoroutine(_coroutine);
            }
        } 
        
    }
    
    public UnityEvent OnDeathEvent;

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
        while (true)
        {
            yield return null;
            
            if (_damageTimer >= 3.0f)
            {
                if (_attackingEnemy.Count > 0)
                {
                    _damageTimer = 0;
                    Health--;
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
