using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using GGMPool;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    public PoolManagerSO poolManager;
    public PoolTypeSO poolType;
    
    public FoodDataListSO foodDataList;
    private Dictionary<FoodType, int> _foods = new Dictionary<FoodType, int>();
    public List<InventorySystem> inventoryList = new List<InventorySystem>();
    public List<Food> kitchenFoods = new List<Food>();
    public InventorySystem kitchen;
    public Tower tower;
    public GameObject kitchenPanel;

    public bool isCanCook = true;
    public bool isCanActiveKitchen = true;
    private bool isKitchenActivating;
    
    [SerializeField] private RectTransform cookPointRect;
    [SerializeField] private CookingButton cookingButton;
    [SerializeField] private RectTransform fillImage;

    public FoodDataSO yogurtData;

    private void Awake()
    {
        kitchen = kitchenPanel.GetComponentInChildren<InventorySystem>();
        kitchen.isOpen = false;
        kitchen.isKitchen = true;
    }

    private void Update()
    {
        OpenKitchen();
    }
    

    private void OpenKitchen()
    {
        if (!isCanActiveKitchen) return;
        if (MenuManager.Instance.isMenuOpen) return;
        if (isKitchenActivating) return;
        if (!isCanCook) return;
        
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            StartCoroutine(OpenKitchenCoroutine());
        }
    }

    private IEnumerator OpenKitchenCoroutine()
    {
        isKitchenActivating = true;
        
        fillImage.transform.gameObject.SetActive(kitchen.isOpen);
        
        Tween moveTween = kitchenPanel.transform.parent.DOMoveY(
            kitchen.isOpen ? -17.5f : 0, 0.9f).SetEase(Ease.OutBack);
        kitchen.isOpen = !kitchen.isOpen;
        
        yield return moveTween.WaitForCompletion();
        
        fillImage.transform.gameObject.SetActive(!kitchen.isOpen);
        
        isKitchenActivating = false;
    }

    public void CookFood()
    {
        var successCook = CheckCanCookFood();

        if (successCook)
        {
            isCanCook = false; 
        }
        cookingButton.OnClick(successCook);
    }

    public bool CheckCanCookFood()
    {
        bool isCooked = false;
        foreach (var fusionFood in foodDataList.fusionFoodDataList)
        {
            List<Food> ingredientsList = new List<Food>();
            if (CheckCanFoodChange(fusionFood, ref ingredientsList) && isCanCook)
            {
                ingredientsList.ForEach(food =>
                {
                    food.slotList.ForEach(slot => slot.isCanEquip = true);
                    food.slotList.Clear();
                    food.InventoryChecker.ResetSlots();
                    kitchenFoods.Remove(food);
                    food.myPool.Push(food);
                });
                isCooked = true;
                CreateFood(fusionFood);
                break;
            }
        }
        return isCooked;
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
        food.isCooked = true;
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
                ingredientsList.Add(food);
            }
            else
                canFood = false;
        }
        return canFood;
    }
}
