using System.Collections.Generic;
using UnityEngine;

public class EnemyChecker : MonoBehaviour, ITowerComponent
{
    private Tower _tower;
    
    [SerializeField] private ContactFilter2D contactFilter;
    
    public List<Collider2D> Targets { get; private set; } = new List<Collider2D>();
    private Collider2D _nearEnemy;
    
    public void Initialize(Tower tower)
    {
        _tower = tower;
    }

    public Collider2D FindNearEnemy()
    {
        _nearEnemy = Targets[0];
        foreach (var enemy in Targets)
        {
            var enemyDistance = Vector3.Distance(enemy.transform.position, _tower.transform.position);
            var nearEnemyDistance = Vector3.Distance(_nearEnemy.transform.position, _tower.transform.position);
            
            if (enemyDistance < nearEnemyDistance)
            {
                _nearEnemy = enemy;
            }
        }

        return _nearEnemy;
    }

    #region TriggerRegion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Targets.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Targets.Add(other);
        }
    }

    #endregion
}
