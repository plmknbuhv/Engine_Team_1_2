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
        yield return new WaitForSeconds(1.5f);
        _attackTimer = 0;
        while (_isCanAttack)
        {
            yield return null;

            _attackTimer += Time.deltaTime;
            _foodRenderer.AdjustFoodGauge(_attackTimer, _foodData.attackCooldown);
            if (_attackTimer >= _foodData.attackCooldown)
            {
                _towerAttack.Attack(_foodData);
                _attackTimer = 0;
            }
        }
    }
}
