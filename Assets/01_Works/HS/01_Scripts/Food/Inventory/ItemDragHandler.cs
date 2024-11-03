using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private FoodRenderer _foodRenderer;
    
    public Vector3 StartPosition { get; private set; }
    public bool IsDragging { get; private set; }

    private void Awake()
    {
        _foodRenderer = GetComponent<FoodRenderer>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragging = true;
        _foodRenderer.SpriteRenderer.sortingOrder = 10;
        StartPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
        transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _foodRenderer.SpriteRenderer.sortingOrder = 0;
        IsDragging = false;
        // 여기서 인벤토리 시스템 구현
    }
}
