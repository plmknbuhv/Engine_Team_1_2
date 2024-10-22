using System;
using GGMPool;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
     [field: SerializeField] public PoolTypeSO PoolType { get; private set; }
     public GameObject GameObject => gameObject;
     
     private Rigidbody2D _rigidCompo;

     private void Awake()
     {
          _rigidCompo = GetComponent<Rigidbody2D>();
     }

     private void Update()
     {
          transform.position += transform.right * Time.deltaTime;
     }
     
     public void SetUpPool(Pool pool)
     {
          
     }

     public void ResetItem()
     {
          
     }
}