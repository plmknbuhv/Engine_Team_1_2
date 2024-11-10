using UnityEngine;
using UnityEngine.EventSystems;

public class FoodDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private FoodRenderer _foodRenderer;
    private InventoryChecker _inventoryChecker;
    private Food _food;
    
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Vector3 returnPosition;
    
    public bool IsDragging { get; private set; }

    private void Awake()
    {
        _food = GetComponent<Food>();
        _foodRenderer = GetComponent<FoodRenderer>();
        _inventoryChecker = GetComponent<InventoryChecker>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragging = true;
        _foodRenderer.SpriteRenderer.sortingOrder = 20;
        foreach (var slot in _food.slotList)
            slot.isCanEquip = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _food.RectTransform.position = GetMousePos();
        _inventoryChecker.CheckInventorySlot();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_inventoryChecker.CheckEquipInventory())
        {
            transform.position = returnPosition;
        }
        
        foreach (var slot in _food.slotList)
        {
            slot.isCanEquip = false;
        }
        _inventoryChecker.ResetSlotS();
        _foodRenderer.SpriteRenderer.sortingOrder = 10;
        _foodRenderer.AdjustFoodSize();
        IsDragging = false;
    }

    private void RotateFood()
    {
              
    }

    private Vector3 GetMousePos()
    {
        var vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0;
        return vec;
    }
}
