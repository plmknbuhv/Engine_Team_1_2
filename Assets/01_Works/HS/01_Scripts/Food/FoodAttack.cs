using System.Collections;
using UnityEngine;

public class FoodAttack : MonoBehaviour
{
    private FoodDataSO _foodData;
    private FoodRenderer _foodRenderer;
    private Tower _tower;
    private TowerAttack _towerAttack;
    private bool _isCanAttack;
    private float _attackTimer;
    private float _attackCooldown;

    public void Initialize(Food food)
    {
        _foodRenderer = food.FoodRenderer;
        _foodData = food.foodDataSO;
        _tower = InventoryManager.Instance.tower;
        _towerAttack = _tower.GetCompo<TowerAttack>();
    }

    public void StartAttack()
    {
        _isCanAttack = true;
        _attackCooldown = _foodData.attackCooldown + Random.Range(-0.055f, 0.055f);
        StartCoroutine(AttackCoroutine());
    }

    public void StopAttack()
    {
        StopAllCoroutines();
        _foodRenderer.AdjustFoodGauge(1, 1);
        _isCanAttack = false;
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _attackTimer = 0;
        while (_isCanAttack)
        {
            yield return null;
            if (!_towerAttack.CheckCanAttack())
                continue;
            if (_tower.isDead) break;

            _attackTimer += Time.deltaTime;
            _foodRenderer.AdjustFoodGauge(_attackTimer, _attackCooldown);
            if (_attackTimer >= _attackCooldown)
            {
                _towerAttack.Attack(_foodData);
                _attackCooldown = _foodData.attackCooldown + Random.Range(-0.055f, 0.055f);
                _attackTimer = 0;
            }
        }
    }
}
