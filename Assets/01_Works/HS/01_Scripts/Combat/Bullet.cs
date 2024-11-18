using GGMPool;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
     [field: SerializeField] public PoolTypeSO PoolType { get; private set; }
     public GameObject GameObject => gameObject;
     private SpriteRenderer _spriteRenderer;
     private GameObject _visualObj;
     private Pool _myPool;

     private float _lifeTimer;
     private float _rotateValue;
     private float _rotateConstant;
     
     private void Awake()
     {
          _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
          _visualObj = transform.Find("Visual").gameObject;
     }

     private void Update()
     {
          _lifeTimer += Time.deltaTime;
          transform.position += transform.up * (Time.deltaTime * 11.5f);
          _visualObj.transform.Rotate(Vector3.forward, Time.deltaTime * (_rotateValue / _rotateConstant));

          if (_lifeTimer >= 5f)
               _myPool.Push(this);
     }

     public void SetUpBullet(FoodDataSO foodData)
     {
          _spriteRenderer.sprite = foodData.sprite;
          
          var rand = Random.Range(-20, 20);
          _rotateValue = rand * 3.5f;
          _rotateConstant = (foodData.height * foodData.width) / 2f;
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
