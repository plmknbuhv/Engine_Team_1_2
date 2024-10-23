using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridHeight = 5;
    private bool[,] _isOccupiedSlots;

    public bool CanPlaceItem(int itemWidth, int itemHeight, int xPos, int yPos)
    {
        if (xPos + itemWidth > gridWidth || yPos + itemHeight > gridHeight)
        {
            return false;
        }

        for (int x = 0; x < itemWidth; x++)
        {
            for (int y = 0; y < itemHeight; y++)
            {
                if (_isOccupiedSlots[xPos + x, yPos + y])
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    public void PlaceItem(int itemWidth, int itemHeight, int xPos, int yPos)
    {
        for (int x = 0; x < itemWidth; x++)
        {
            for (int y = 0; y < itemHeight; y++)
            {
                _isOccupiedSlots[xPos + x, yPos + y] = true;
            }
        }
    }
}
