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
        bool isCanEquip = InventoryManager.Instance.inventoryList[0].EquipItem(GetLocalMousePos(0), 
                             _food.width, _food.height, _food) ||
                         InventoryManager.Instance.inventoryList[1].EquipItem(GetLocalMousePos(1), 
                             _food.width, _food.height, _food) ||
                         InventoryManager.Instance.inventoryList[2].EquipItem(GetLocalMousePos(2), 
                             _food.width, _food.height, _food);

        return isCanEquip;
    }
    
    private Vector3 GetLocalMousePos(int inventoryNum)
    {
        var targetTrm = InventoryManager.Instance.inventoryList[inventoryNum].GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(targetTrm, Camera.main.WorldToScreenPoint(transform.position), Camera.main, out var localPoint);
        return localPoint / 67.5f;
    }

    public void CheckInventorySlot()
    {
        for (int i = 0; i < 3; i++)
        {
            InventoryManager.Instance.inventoryList[i].CheckSlot(GetLocalMousePos(i),
                _food.width, _food.height);
        }
    }

    public void ResetSlots()
    {
        InventoryManager.Instance.inventoryList[0].ResetSlots();
        InventoryManager.Instance.inventoryList[1].ResetSlots();
        InventoryManager.Instance.inventoryList[2].ResetSlots();
    }
}
