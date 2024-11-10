using UnityEngine;

public class InventoryChecker : MonoBehaviour
{
    private Food _food;

    private void Awake()
    {
        _food = GetComponent<Food>();
    }
    
    public bool CheckEquipInventory()
    {
        bool isCanEquip = FoodManager.Instance.inventoryList[0].EquipItem(GetLocalMousePos(0), 
                             _food.foodDataSO.width, _food.foodDataSO.height, _food) ||
                         FoodManager.Instance.inventoryList[1].EquipItem(GetLocalMousePos(1), 
                             _food.foodDataSO.width, _food.foodDataSO.height, _food);

        return isCanEquip;
    }
    
    private Vector3 GetLocalMousePos(int inventoryNum)
    {
        var targetTrm = FoodManager.Instance.inventoryList[inventoryNum].GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(targetTrm, Camera.main.WorldToScreenPoint(transform.position), Camera.main, out var localPoint);
        return localPoint / 67.5f;
    }

    public void CheckInventorySlot()
    {
        for (int i = 0; i < 2; i++)
        {
            FoodManager.Instance.inventoryList[i].CheckSlot(GetLocalMousePos(i),
                _food.foodDataSO.width, _food.foodDataSO.height);
        }
    }

    public void ResetSlotS()
    {
        FoodManager.Instance.inventoryList[0].ResetSlots();
        FoodManager.Instance.inventoryList[1].ResetSlots();
    }
}
