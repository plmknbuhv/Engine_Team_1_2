using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    private Dictionary<FoodType, int> _foods = new Dictionary<FoodType, int>();
    public List<InventorySystem> inventoryList = new List<InventorySystem>();
    public GameObject kitchenPanel;
    public Tower tower;

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
            kitchenPanel.SetActive(!kitchenPanel.activeSelf);
    }
}
