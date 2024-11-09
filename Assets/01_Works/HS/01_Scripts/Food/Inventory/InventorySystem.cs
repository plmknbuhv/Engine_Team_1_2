using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private Slot slotPrefab;
    private Slot[,] _slotArray;
    private List<Slot> _preSlotList = new List<Slot>();

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

    private void EquipItem(int x, int y, int itemWidth, int itemHeight)
    {
        if (!(x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)) return;
        
        print(x);
        print(y);
    }

    public void EquipItem(Vector3 worldPosition, int itemWidth, int itemHeight)
    {
        GetXY(worldPosition, out var x, out var y, itemWidth, itemHeight);
        EquipItem(x, y, itemWidth, itemHeight);
    }

    public void CheckSlot(Vector3 worldPosition, int itemWidth, int itemHeight)
    {
        GetXY(worldPosition, out var x, out var y, itemWidth, itemHeight);
        
        // if (_preSlotList.Count <= 0) return;
        
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

                _slotArray[i, j].ShowSlotAvailability();
                _preSlotList.Add(_slotArray[i, j]);
            }
        }
    }
}
