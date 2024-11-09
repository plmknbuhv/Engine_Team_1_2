using System.Collections.Generic;
using GGMPool;
using TMPro;
using UnityEngine;

public class ShopManager : MonoSingleton<ShopManager>
{
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
        shopFoodList.Add(food);

        float xSpace = 0;
        float ySpace = 0;
        if (foodData.width == 2)
            xSpace = 0.45f;
        if (foodData.height == 2)
            ySpace = 0.45f;
        
        Vector3 foodPosition = new Vector3(-11.48f - xSpace, 5.25f - number * 3.56f - ySpace);
        food.RectTransform.localPosition = foodPosition;
        food.SetUpFood(foodData);
    }
}
