using System.Collections.Generic;
using GGMPool;
using TMPro;
using UnityEngine;

public class ShopManager : MonoSingleton<ShopManager>
{
    public Canvas shopCanvas;
    
    [SerializeField] private FoodDataListSO foodDataListSO;
    [SerializeField] private List<Food> shopFoodList = new List<Food>();
    
    [SerializeField] private List<TextMeshProUGUI> foodPriceTextList = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI goldValueText;
    
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO poolType;
    
    [SerializeField] private int gold;

    [SerializeField] private RectTransform shopPointRect;
    public Transform foodStartPointTrm;

    private List<FoodDataSO> _prevShopDataList = new List<FoodDataSO>();
    
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

        for (int i = 0; i < 4;)
        {
            var ranIndex = Random.Range(0, foodDataList.Count);
            if (_prevShopDataList.Contains(foodDataList[ranIndex]))
                continue;
            CreateFoodItem(foodDataList[ranIndex], i);
            _prevShopDataList.Add(foodDataList[ranIndex]);
            
            var foodPrice = foodDataList[ranIndex].height * foodDataList[ranIndex].width;
            foodPriceTextList[i].text = foodPrice.ToString();
            i++;
        }
        _prevShopDataList.Clear();
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
        var food = poolManager.Pop(poolType) as Food;
        food.RectTransform.SetParent(shopCanvas.transform);
        shopFoodList.Add(food);

        float xSpace = 0;
        float ySpace = 0;
        if (foodData.width == 2)
            xSpace = 26.9961f;
        if (foodData.height == 2)
            ySpace = 26.9961f;
        
        Vector3 foodPosition = new Vector3(shopPointRect.anchoredPosition.x - xSpace, shopPointRect.anchoredPosition.y + number * -213.5682f - ySpace);
        food.RectTransform.anchoredPosition = foodPosition;
        food.FoodDragHandler.startPosition = food.transform.position;
        food.SetUpFood(foodData);
    }

    public void BuyFood(Food food)
    {
        if (Gold < food.foodDataSO.height * food.foodDataSO.width * 2) return;

        Gold -= food.foodDataSO.height * food.foodDataSO.width * 2;
        shopFoodList.Remove(food);
        food.isPurchased = true;
    }

    public bool CheckCanBuyFood(Food food)
    {
        return Gold >= food.foodDataSO.height * food.foodDataSO.width * 2;
    }
}
