using GGMPool;
using UnityEngine;

public class TowerAttack : MonoBehaviour , ITowerComponent
{
    private Tower _tower;
    private EnemyChecker _enemyChecker;
    
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO bulletType;
    [SerializeField] private PoolTypeSO foodType;
    
    public void Initialize(Tower tower)
    {
        _tower = tower;
        _enemyChecker = _tower.GetCompo<EnemyChecker>();
    }

    public bool CheckCanAttack()
    {
        return _enemyChecker.Targets.Count > 0;
    }

    public void Attack(FoodDataSO foodData)
    {
        if (_enemyChecker.Targets.Count < 1) return;

        if (foodData.foodType == FoodType.Yoe)
        {
            for (var i = -2; i < 3; i++)
            {
                var bullet = poolManager.Pop(bulletType) as Bullet;
        
                var angle = SetAngle(out var rand);
                var targetRotate = Quaternion.Euler(0, 0, (angle - 90 + rand) + i * 10);
        
                bullet.transform.SetPositionAndRotation(transform.position, targetRotate);
                bullet.SetUpBullet(InventoryManager.Instance.yogurtData, poolManager, foodType);
            }
        }
        else
        {
            var bullet = poolManager.Pop(bulletType) as Bullet;
        
            var angle = SetAngle(out var rand);
            var targetRotate = Quaternion.Euler(0, 0, angle - 90 + rand);
        
            bullet.transform.SetPositionAndRotation(transform.position, targetRotate);
            bullet.SetUpBullet(foodData, poolManager, foodType);
        }
    }

    private float SetAngle(out float rand)
    {
        var targetDir = _enemyChecker.FindNearEnemy().transform.position - transform.position;
        
        var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        rand = Random.Range(-0.5f, 0.5f) * 2;
        return angle;
    }
}
