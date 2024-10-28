using System.Collections;
using System.Collections.Generic;
using GGMPool;
using UnityEngine;

public class Food : MonoBehaviour, IPoolable
{
    private FoodType _foodType;
    private FoodDataSO _foodDataSO;
    
    [field:SerializeField] public PoolTypeSO PoolType { get; private set; }
    public GameObject GameObject => gameObject;

    public void SetUpFood(FoodDataSO foodDataSO)
    {
        _foodDataSO = foodDataSO;
        _foodType = _foodDataSO.foodType;
        gameObject.name = _foodType.ToString();
    }
    
    public void SetUpPool(Pool pool)
    {
        
    }

    public void ResetItem()
    {
        
    }
}