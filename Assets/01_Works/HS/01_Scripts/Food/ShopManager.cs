using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoSingleton<ShopManager>
{
    [SerializeField] private FoodDataListSO foodDataListSO;
    [SerializeField] private List<FoodDataSO> shopFoodList = new List<FoodDataSO>();
    [SerializeField] private List<TextMeshProUGUI> foodPriceTextList = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI goldValueText;

    [SerializeField] private int gold;

    public int Gold
    {
        get => gold;
        set => gold = value > 0 ? value : 0;
    }

    public void ReRollShop()
    {
        shopFoodList.Clear();
        List<FoodDataSO> foodDataList = foodDataListSO.normalFoodDataList; 

        for (int i = 0; i < 4; i++)
        {
            var ranIndex = Random.Range(0, foodDataList.Count);
            shopFoodList.Add(foodDataList[ranIndex]);
            var foodPrice = foodDataList[ranIndex].height * foodDataList[ranIndex].width;
            foodPriceTextList[i].text = foodPrice.ToString();
        }
    }
}
