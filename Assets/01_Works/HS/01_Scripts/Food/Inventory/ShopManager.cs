using System;
using System.Collections.Generic;
using DG.Tweening;
using GGMPool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : MonoSingleton<ShopManager>
{
    public Canvas shopCanvas;
    
    [SerializeField] private FoodDataListSO foodDataListSO;
    [SerializeField] private List<Food> shopFoodList = new List<Food>();
    
    [SerializeField] private List<TextMeshProUGUI> foodPriceTextList = new List<TextMeshProUGUI>();
    [SerializeField] private TMP_Text goldValueText;
    
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO poolType;
    
    [SerializeField] private int gold;

    [SerializeField] private RectTransform shopPointRect;
    public Transform foodStartPointTrm;

    private FoodDataSO[] _prevShopDataList = new FoodDataSO[4];
    
    [SerializeField] private Image buttonImage;
    
    public PoolTypeSO goldPoolType;
    
    public int Gold
    {
        get => gold;
        set
        {
            gold = value >= 0 ? value : 0;
            var currentGold = Int32.Parse(goldValueText.text);

            DOTween.To(() => currentGold, goldValue => goldValueText.text = goldValue.ToString(),
                    gold, Mathf.Abs(currentGold - gold) / 8f)
                .SetEase(Ease.OutSine);
        }
    }

    public void ReRollShop()
    {
        if (Gold < 2) return;
        Gold -= 2;
        
        buttonImage.transform.DOPunchScale(Vector3.one * 0.1f,  0.21f, 3);
        
        ClearShop();
        ShowText();
        
        List<FoodDataSO> foodDataList = foodDataListSO.normalFoodDataList; 

        for (int i = 0; i < 4;)
        {
            var ranIndex = Random.Range(0, foodDataList.Count);
            if (Array.IndexOf(_prevShopDataList, foodDataList[ranIndex]) != -1)
                continue;
            CreateFoodItem(foodDataList[ranIndex], i);
            _prevShopDataList[i] = foodDataList[ranIndex];
            
            var foodPrice = foodDataList[ranIndex].height * foodDataList[ranIndex].width * 3;
            foodPriceTextList[i].text = foodPrice.ToString();
            i++;
        }
        Array.Clear(_prevShopDataList, 0, _prevShopDataList.Length);
    }

    private void ShowText()
    {
        foreach (var text in foodPriceTextList)
            text.DOFade(1, 0.22f);
    }

    private void ClearShop()
    {
        foreach (var food in shopFoodList)
        {
            if (food != null)
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
        food.transform.position = new Vector3(food.transform.position.x, food.transform.position.y, 90);
        food.FoodDragHandler.startPosition = food.transform.position;
        food.SetUpFood(foodData);
    }

    public void BuyFood(Food food)
    {
        if (!CheckCanBuyFood(food)) return;

        var foodIndex = shopFoodList.IndexOf(food);

        foodPriceTextList[foodIndex].DOFade(0, 0.22f);
        Gold -= food.foodDataSO.height * food.foodDataSO.width * 3;
        shopFoodList[foodIndex] = null;
        food.isPurchased = true;
    }

    public bool CheckCanBuyFood(Food food)
    {
        return Gold >= food.foodDataSO.height * food.foodDataSO.width * 3;
    }
}
