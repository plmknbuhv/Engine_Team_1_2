using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private Slot slotPrefab;
    private Slot[,] _slotArray;

    private void Awake()
    {
        _slotArray = new Slot[gridHeight, gridWidth];
        SetUpInventory();
    }

    public void SetUpInventory()
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

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / gridWidth);
        y = Mathf.FloorToInt(worldPosition.y / gridHeight);
    }

    public void EquipItem(int x, int y, int itemWidth, int itemHeight)
    {
        if (!(x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)) return;
        
        print(_slotArray[x,y].name);
    }

    public void EquipItem(Vector3 worldPosition, int itemWidth, int itemHeight)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        EquipItem(x, y, itemWidth, itemHeight);
    }
}
