using GGMPool;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour, IPoolable
{
     [field: SerializeField] public PoolTypeSO PoolType { get; private set; }
     public GameObject GameObject => gameObject;
     private SpriteRenderer _spriteRenderer;
     private GameObject _visualObj;
     private Pool _myPool;
     private FoodDataSO _foodData;

     private float _lifeTimer;
     private float _rotateValue;
     private float _rotateConstant;

     private bool _isDead;
     private PoolManagerSO _poolManager;
     private PoolTypeSO _effectType;

     private void Awake()
     {
          _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
          _visualObj = transform.Find("Visual").gameObject;
     }

     private void Update()
     {
          _lifeTimer += Time.deltaTime;
          transform.position += transform.up * (Time.deltaTime * 12f);
          _visualObj.transform.Rotate(Vector3.forward, Time.deltaTime * (_rotateValue / _rotateConstant));

          if (_lifeTimer >= 5f)
          {
               _myPool.Push(this);
               _isDead = true;
          }
     }

     private void OnTriggerEnter2D(Collider2D other)
     {
          if (_isDead) return;
          
          if (other.CompareTag("Enemy"))
          {
               var enemy = other.GetComponent<Enemy>();
               AttackEnemy(enemy);
          }
     }

     private void AttackEnemy(Enemy enemy)
     {
          switch (_foodData.foodType)
          {
               case FoodType.CheesePizza:
                    enemy.GetSlow(30f, 2);
                    enemy.GetDamage(_foodData.damage, 7);
                    break;
               case FoodType.GimBap2XL:
                    enemy.GetDamage(_foodData.damage, 18);
                    break;
               case FoodType.TwoEgg:
                    enemy.GetDamage(_foodData.damage, 18);
                    break;
               case FoodType.RamenMandu:
                    enemy.GetStun(2);
                    enemy.GetDamage(_foodData.damage, 7);
                    break;
               default:
                    enemy.GetDamage(_foodData.damage, 7);
                    break;
          }
          _isDead = true;
          
          var effect = _poolManager.Pop(_effectType) as FoodEffect;
          effect.SetPositionAndPlay(transform.position);
          
          _myPool.Push(this);
     }

     public void SetUpBullet(FoodDataSO foodData, PoolManagerSO poolManagerSo, PoolTypeSO poolType)
     {
          _poolManager = poolManagerSo;
          _effectType = poolType;
          _foodData = foodData;
          _spriteRenderer.sprite = foodData.sprite;
          
          var rand = Random.Range(-20, 20);
          _rotateValue = rand * 4f;
          _rotateConstant = (foodData.height * foodData.width) / 2f;
     }
     
     public void SetUpPool(Pool pool)
     {
          _myPool = pool;
     }

     public void ResetItem()
     {
          _lifeTimer = 0;
          _isDead = false;
     }
}
