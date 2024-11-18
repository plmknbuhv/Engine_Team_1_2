using GGMPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IPoolable
{
    [SerializeField] private PoolManagerSO poolManager;

    [field:SerializeField] public PoolTypeSO PoolType { get; set; }

    public GameObject GameObject => gameObject;

    public void ResetItem()
    {
        
    }

    public void SetUpPool(Pool pool)
    {
        
    }

    private void OnEnable()
    {
        Invoke("DisibleExplosion", 0.5f);
    }

    public void DisibleExplosion()
    {
        poolManager.Push(this);
    }
}
