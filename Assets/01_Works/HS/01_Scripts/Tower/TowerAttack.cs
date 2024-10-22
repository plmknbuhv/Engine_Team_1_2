using GGMPool;
using UnityEngine;

public class TowerAttack : MonoBehaviour , ITowerComponent
{
    private Tower _tower;
    private EnemyChecker _enemyChecker;
    
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO bulletType;
    [SerializeField] private float attackCooldown;
    private float _attackTimer;
    
    public void Initialize(Tower tower)
    {
        _tower = tower;
        _enemyChecker = _tower.GetCompo<EnemyChecker>();
    }

    private void Update()
    {
        _attackTimer += Time.deltaTime;

        if (_attackTimer >= attackCooldown && _enemyChecker.Targets.Count > 0)
        {
            Attack();
            _attackTimer = 0f;
        }
    }

    public void Attack(FoodType foodType)
    {
        var bullet = poolManager.Pop(bulletType) as Bullet;
        
        var targetDir = _enemyChecker.FindNearEnemy().transform.position - transform.position;
        
        var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        var targetRotate = Quaternion.Euler(0, 0, angle);
        
        bullet.transform.SetPositionAndRotation(transform.position, targetRotate);
    }
}
