using UnityEngine;

public class InventoryChecker : MonoBehaviour
{
    private Food _food;

    private void Awake()
    {
        _food = GetComponent<Food>();
    }
    
    public void CheckEquipInventory()
    {
        for (int i = 0; i < 2; i++)
        {
            FoodManager.Instance.inventoryList[i].EquipItem(GetLocalMousePos(i),
                _food.foodDataSO.width, _food.foodDataSO.height);
        }
    }
    
    private Vector3 GetLocalMousePos(int inventoryNum)
    {
        var targetTrm = FoodManager.Instance.inventoryList[inventoryNum].GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(targetTrm, Input.mousePosition, Camera.main, out var localPoint);
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
}
