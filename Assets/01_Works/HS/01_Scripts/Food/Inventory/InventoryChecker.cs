using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryChecker : MonoBehaviour
{
    private Food _food;

    private void Awake()
    {
        _food = GetComponent<Food>();
    }
    
    public void CheckInventory()
    {
        for (int i = 0; i < 2; i++)
        {
            FoodManager.Instance.inventoryList[i].EquipItem(GetLocalMousePos(i),
                _food.foodDataSO.width, _food.foodDataSO.height);
        }
    }
    
    public Vector3 GetLocalMousePos(int inventoryNum)
    {
        var targetTrm = FoodManager.Instance.inventoryList[inventoryNum].GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(targetTrm, Input.mousePosition, Camera.main, out var localPoint);
        return localPoint / 67.5f;
    }
}
