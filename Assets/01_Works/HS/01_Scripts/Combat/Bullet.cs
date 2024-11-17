using GGMPool;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
     [field: SerializeField] public PoolTypeSO PoolType { get; private set; }
     public GameObject GameObject => gameObject;
     private SpriteRenderer _spriteRenderer;

     private void Awake()
     {
          _spriteRenderer = GetComponent<SpriteRenderer>();
     }

     private void Update()
     {
          transform.position += transform.up * (Time.deltaTime * 10);
     }

     public void SetUpSprite(Sprite sprite)
     {
          _spriteRenderer.sprite = sprite;
     }
     
     public void SetUpPool(Pool pool)
     {
          
     }

     public void ResetItem()
     {
          
     }
}
