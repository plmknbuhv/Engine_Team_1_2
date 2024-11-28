using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private Slot slotPrefab;
    private Slot[,] _slotArray;
    [SerializeField] private List<Slot> _preSlotList = new List<Slot>();
    public bool isOpen = true;
    public bool isKitchen;

    private void Awake()
    {
        _slotArray = new Slot[gridHeight, gridWidth];
        SetUpInventory();
    }

    private void SetUpInventory()
    {
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                Slot slot = Instantiate(slotPrefab,transform);
                _slotArray[i, j] = slot;
            }
        }
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y, int itemWidth, int itemHeight)
    {
        x = Mathf.FloorToInt(worldPosition.x - 0.5f * (itemWidth - 1));
        y = Mathf.FloorToInt(worldPosition.y - 0.5f * (itemHeight - 1));
    }

    public void EquipInventory(int x, int y,  int itemWidth, int itemHeight, Food food)
    {
        Vector3 slotPos = _slotArray[y, x].transform.position;
        Vector3 equipPos = new Vector3(
            slotPos.x + 0.56f * (itemWidth - 1),
            slotPos.y + 0.56f * (itemHeight - 1));

        food.transform.position = equipPos;
        food.FoodDragHandler.returnPosition = equipPos;

        for (int i = y; i < itemHeight + y; i++)
            for (int j = x; j < itemWidth + x; j++)
                food.slotList.Add(_slotArray[i, j]);
    }

    public bool EquipItem(Vector3 worldPosition, int itemWidth, int itemHeight, Food food)
    {
        if (!isOpen) return false;
        
        GetXY(worldPosition, out var x, out var y, itemWidth, itemHeight);
        if (!(x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)) return false;
        if (!food.isPurchased && !ShopManager.Instance.CheckCanBuyFood(food)) return false;
        
        if (CheckCanEquipItem(itemWidth, itemHeight))
        {
            foreach (var slot in _preSlotList)
                slot.isCanEquip = false;

            foreach (var slot in food.slotList)
                slot.isCanEquip = true;
            
            food.slotList.Clear();
            EquipInventory(x, y, itemWidth, itemHeight, food);
            food.slotList.ForEach(slot => slot.isCanEquip = false);

            if (!food.isPurchased)
                ShopManager.Instance.BuyFood(food);

            if (isKitchen)
            {
                if (!InventoryManager.Instance.kitchenFoods.Contains(food))
                    InventoryManager.Instance.kitchenFoods.Add(food);
                food.transform.SetParent(InventoryManager.Instance.kitchenPanel.transform.parent);
                food.FoodAttack.StopAttack();
            }
            else
            {
                if (InventoryManager.Instance.kitchenFoods.Contains(food))
                    InventoryManager.Instance.kitchenFoods.Remove(food);
                food.transform.SetParent(ShopManager.Instance.shopCanvas.transform);
                food.FoodAttack.StartAttack();
            }     
            return true;
        }
        return false;
    }

    private bool CheckCanEquipItem(int itemWidth, int itemHeight)
    {
        if (_preSlotList.Count < itemWidth * itemHeight) return false;

        bool isCanEquip = true;
        foreach (var slot in _preSlotList)
        {
            if (!slot.isCanEquip)
            {
                isCanEquip = false;
                break;
            }
        }
        return isCanEquip;
    }

    public void CheckSlot(Vector3 worldPosition, int itemWidth, int itemHeight, bool isRotating)
    {
        GetXY(worldPosition, out var x, out var y, itemWidth, itemHeight);
        
        foreach (var slot in _preSlotList)
        {
            slot.ResetSlotColor();
        }
        _preSlotList.Clear();
        
        for (int i = y; i < y + itemHeight; i++)
        {
            for (int j = x; j < x + itemWidth; j++)
            {
                if (!(i >= 0 && j >= 0 && i < gridHeight && j < gridWidth)) return;
                _preSlotList.Add(_slotArray[i, j]);
            }
        }

        if (_preSlotList.Count < itemWidth * itemHeight) return;

        foreach (var slot in _preSlotList)
        {   
            if (isRotating) return;
            slot.ShowSlotAvailability();
        }
    }

    public void ResetSlots()
    {
        foreach (var slot in _slotArray)
        {
            slot.ResetSlotColor();
        }
    }
}
