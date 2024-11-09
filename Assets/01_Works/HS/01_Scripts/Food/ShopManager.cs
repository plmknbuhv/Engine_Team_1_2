using System.Collections.Generic;
using GGMPool;
using TMPro;
using UnityEngine;

public class ShopManager : MonoSingleton<ShopManager>
{
    [SerializeField] private Canvas shopCanvas;
    
    [SerializeField] private FoodDataListSO foodDataListSO;
    [SerializeField] private List<Food> shopFoodList = new List<Food>();
    
    [SerializeField] private List<TextMeshProUGUI> foodPriceTextList = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI goldValueText;
    
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO bulletType;
    
    [SerializeField] private int gold;
    
    public int Gold
    {
        get => gold;
        set
        {
            gold = value > 0 ? value : 0;
            
            goldValueText.text = gold.ToString();
        }
    }

    public void ReRollShop()
    {
        if (Gold < 1) return;
        Gold--;
        
        ClearShop();
        
        List<FoodDataSO> foodDataList = foodDataListSO.normalFoodDataList; 

        for (int i = 0; i < 4; i++)
        {
            var ranIndex = Random.Range(0, foodDataList.Count);
            CreateFoodItem(foodDataList[ranIndex], i);
            
            var foodPrice = foodDataList[ranIndex].height * foodDataList[ranIndex].width;
            foodPriceTextList[i].text = foodPrice.ToString();
        }
    }

    private void ClearShop()
    {
        foreach (var food in shopFoodList)
        {
            poolManager.Push(food);
        }
        shopFoodList.Clear();
    }

    private void CreateFoodItem(FoodDataSO foodData, int number)
    {
        var food = poolManager.Pop(bulletType) as Food;
        food.RectTransform.SetParent(shopCanvas.transform);
        shopFoodList.Add(food);

        float xSpace = 0;
        float ySpace = 0;
        if (foodData.width == 2)
            xSpace = 26.9961f;
        if (foodData.height == 2)
            ySpace = 26.9961f;
        
        Vector3 foodPosition = new Vector3(-688.6971f- xSpace, 314.9529f + number * -213.5682f - ySpace);
        food.RectTransform.anchoredPosition = foodPosition;
        food.SetUpFood(foodData);
    }
}
