using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoSingleton<ShopManager>
{
    [SerializeField] public FoodDataListSO foodDataListSO;
    [SerializeField] public List<FoodDataSO> shopFoodList = new List<FoodDataSO>();
    
    public void ReRollShop()
    {
        shopFoodList.Clear();

        for (int i = 0; i < 4; i++)
        {
            var ranIndex = Random.Range(0, foodDataListSO.normalFoodDataList.Count);
            shopFoodList.Add(foodDataListSO.normalFoodDataList[ranIndex]);
        }
    }
}
