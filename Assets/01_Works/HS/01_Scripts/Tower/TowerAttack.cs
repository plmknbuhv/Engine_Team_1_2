using GGMPool;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO bulletType;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    private float _attackTimer;
    
    private Tower _tower;

    private void Awake()
    {
        _tower = GetComponent<Tower>();
    }

    private void Update()
    {
        _attackTimer += Time.deltaTime;

        if (_attackTimer >= attackCooldown && _tower._colliders[0] != null)
        {
            Attack();
            
            _attackTimer = 0f;
        }
    }

    private void Attack()
    {
        _tower.TowerAnimation.isAttackAnim = true;
        var bullet = poolManager.Pop(bulletType) as Bullet;
        
        var targetDir = _tower._colliders[0].transform.position - transform.position;
        
        var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        var targetRotate = Quaternion.Euler(0, 0, angle);
        
        bullet.transform.SetPositionAndRotation(transform.position, targetRotate);
    }
}
