using System.Collections;
using UnityEngine;

public class FoodAttack : MonoBehaviour
{
    private Food _food;
    private FoodDataSO _foodData;
    private Tower _tower;
    private TowerAttack _towerAttack;
    private bool _isCanAttack;

    public void Initialize(Food food)
    {
        _food = food;
        _foodData = food.foodDataSO;
        _tower = FoodManager.Instance.tower;
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
        _isCanAttack = false;
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        while (_isCanAttack)
        {
            _towerAttack.Attack(_foodData);
            yield return new WaitForSeconds(_foodData.attackCooldown);
        }
    }
}
