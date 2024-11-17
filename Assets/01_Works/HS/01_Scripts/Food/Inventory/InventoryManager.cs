using System.Collections.Generic;
using System.Linq;
using GGMPool;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    public PoolManagerSO poolManager;
    public PoolTypeSO poolType;
    
    public FoodDataListSO foodDataList;
    private Dictionary<FoodType, int> _foods = new Dictionary<FoodType, int>();
    public List<InventorySystem> inventoryList = new List<InventorySystem>();
    public List<Food> kitchenFoods = new List<Food>();
    public GameObject kitchenPanel;
    private InventorySystem _kitchen;
    public Tower tower;

    public bool isCanActiveKitchen = true;
    
    [SerializeField] private RectTransform cookPointRect;

    private void Awake()
    {
        kitchenPanel.SetActive(true);
        _kitchen = kitchenPanel.GetComponentInChildren<InventorySystem>();
        _kitchen.isOpen = false;
        _kitchen.isKitchen = true;
        kitchenPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isCanActiveKitchen) return;
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            kitchenPanel.SetActive(!kitchenPanel.activeSelf);
            _kitchen.isOpen = !_kitchen.isOpen;
            foreach (var food in kitchenFoods)
            {
                food.gameObject.SetActive(!food.gameObject.activeSelf);
            }
        }
    }

    public void CookFood()
    {
        foreach (var fusionFood in foodDataList.fusionFoodDataList)
        {
            List<Food> ingredientsList = new List<Food>();
            if (CheckCanFoodChange(fusionFood, ref ingredientsList))
            {
                ingredientsList.ForEach(food =>
                {
                    food.slotList.ForEach(slot => slot.isCanEquip = true);
                    food.InventoryChecker.ResetSlots();
                    kitchenFoods.Remove(food);
                    poolManager.Push(food);
                });
                CreateFood(fusionFood);
                break;
            }
            else
            {
                // 실패
            }
        }
    }

    private void CreateFood(FusionFoodDataSO fusionFood)
    {
        var food = poolManager.Pop(poolType) as Food;
        food.RectTransform.SetParent(ShopManager.Instance.shopCanvas.transform);
        
        float xSpace = 0;
        float ySpace = 0;
        if (fusionFood.width == 2)
            xSpace = 26.9961f;
        if (fusionFood.height == 2)
            ySpace = 26.9961f;
        
        Vector3 foodPosition = new Vector3(
            cookPointRect.anchoredPosition.x - xSpace,
            cookPointRect.anchoredPosition.y - ySpace);
        food.RectTransform.anchoredPosition = foodPosition;
        food.SetUpFood(fusionFood);
        food.isPurchased = true;
        food.FoodDragHandler.startPosition = ShopManager.Instance.foodStartPointTrm.position;
        kitchenFoods.Add(food);
    }

    private bool CheckCanFoodChange(FusionFoodDataSO fusionFoodData, ref List<Food> ingredientsList)
    {
        List<Food> kitchenFoodList = kitchenFoods.ToList();
        
        bool canFood = true;
        foreach (var foodData in fusionFoodData.ingredients)
        {
            Food food = kitchenFoodList.Find(food => food.foodDataSO == foodData);
            if (food != null)
            {
                kitchenFoodList.Remove(food);
                food.myPool.Push(food);
                ingredientsList.Add(food);
            }
            else
                canFood = false;
        }
        return canFood;
    }
}