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
}
