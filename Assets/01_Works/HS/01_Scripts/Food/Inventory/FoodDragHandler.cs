using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private FoodRenderer _foodRenderer;
    private Food _food;
    
    public List<InventorySystem> inventoryList = new List<InventorySystem>();
    [HideInInspector] public Vector3 startPosition;
    
    public bool IsDragging { get; private set; }

    private void Awake()
    {
        _foodRenderer = GetComponent<FoodRenderer>();
        _food = GetComponent<Food>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragging = true;
        _foodRenderer.SpriteRenderer.sortingOrder = 20;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMousePos();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDragging = false;
        _foodRenderer.SpriteRenderer.sortingOrder = 10;

        for (int i = 0; i < 2; i++)
        {
            print(GetLocalMousePos(i));
            inventoryList[0].EquipItem(GetLocalMousePos(i),
                _food.foodDataSO.width, _food.foodDataSO.height);
        }
    }

    public Vector3 GetMousePos()
    {
        var vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0;
        return vec;
    }

    public Vector3 GetLocalMousePos(int inventoryNum)
    {
        var targetTrm = inventoryList[inventoryNum].transform;
        return targetTrm.InverseTransformPoint(GetMousePos());
    }
}
