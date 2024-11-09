 using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private FoodRenderer _foodRenderer;
    private InventoryChecker _inventoryChecker;
    
    [HideInInspector] public Vector3 startPosition;
    
    public bool IsDragging { get; private set; }

    private void Awake()
    {
        _foodRenderer = GetComponent<FoodRenderer>();
        _inventoryChecker = GetComponent<InventoryChecker>();
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

        _inventoryChecker.CheckInventory();
    }

    public Vector3 GetMousePos()
    {
        var vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0;
        return vec;
    }
}
