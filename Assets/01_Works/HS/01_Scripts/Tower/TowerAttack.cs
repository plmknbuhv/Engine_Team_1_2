using GGMPool;
using UnityEngine;

public class TowerAttack : MonoBehaviour , ITowerComponent
{
    private Tower _tower;
    private EnemyChecker _enemyChecker;
    
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO bulletType;
    
    public void Initialize(Tower tower)
    {
        _tower = tower;
        _enemyChecker = _tower.GetCompo<EnemyChecker>();
    }

    public void Attack(FoodDataSO foodData)
    {
        if (_enemyChecker.Targets.Count < 1) return;
        
        var bullet = poolManager.Pop(bulletType) as Bullet;
        
        var targetDir = _enemyChecker.FindNearEnemy().transform.position - transform.position;
        
        var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        var rand = Random.Range(-0.85f, 0.85f);
        var targetRotate = Quaternion.Euler(0, 0, angle - 90 + rand);
        
        bullet.transform.SetPositionAndRotation(transform.position, targetRotate);
        bullet.SetUpSprite(foodData.sprite);
    }
}
