using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    private Dictionary<FoodType, int> _foods = new Dictionary<FoodType, int>();
    public List<InventorySystem> inventoryList = new List<InventorySystem>();
    public List<Food> kitchenFoods = new List<Food>();
    public GameObject kitchenPanel;
    private InventorySystem _kitchen;
    public Tower tower;

    public bool isCanActiveKitchen = true;

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

    public void CheckCanFoodChange()
    {
        
    }
}
