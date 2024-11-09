using UnityEngine;
using UnityEngine.EventSystems;

public class FoodDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private FoodRenderer _foodRenderer;
    private InventoryChecker _inventoryChecker;
    private Food _food;
    
    [HideInInspector] public Vector3 startPosition;
    
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        _food.RectTransform.position = GetMousePos();
        _inventoryChecker.CheckInventorySlot();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_inventoryChecker.CheckEquipInventory())
            transform.position = startPosition;
        else
        {
            
        }
        
        _foodRenderer.SpriteRenderer.sortingOrder = 10;
        _foodRenderer.AdjustFoodSize();
        IsDragging = false;
    }

    public Vector3 GetMousePos()
    {
        var vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0;
        return vec;
    }
}
