using GGMPool;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
     [field: SerializeField] public PoolTypeSO PoolType { get; private set; }
     public GameObject GameObject => gameObject;
     private SpriteRenderer _spriteRenderer;
     private float _lifeTimer;
     private Pool _myPool;

     private void Awake()
     {
          _spriteRenderer = GetComponent<SpriteRenderer>();
     }

     private void Update()
     {
          _lifeTimer += Time.deltaTime;
          transform.position += transform.up * (Time.deltaTime * 13.7f);

          if (_lifeTimer >= 5.5f)
               _myPool.Push(this);
     }

     public void SetUpSprite(Sprite sprite)
     {
          _spriteRenderer.sprite = sprite;
     }
     
     public void SetUpPool(Pool pool)
     { 
          _myPool = pool;
     }

     public void ResetItem()
     {
          _lifeTimer = 0;
     }
}
