using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FoodDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
{
    private InventoryChecker _inventoryChecker;
    private FoodRenderer _foodRenderer;
    private FoodAttack _foodAttack;
    private Food _food;
    
    public Vector3 startPosition;
    [HideInInspector] public Vector3 returnPosition;

    public bool isDragging;
    public bool isRotating;
    public float targetRotation;
    private float _prevRotation;
    private int _prevHeight;
    private int _prevWidth;

    private void Awake()
    {
        _food = GetComponent<Food>();
        _foodRenderer = GetComponent<FoodRenderer>();
        _inventoryChecker = GetComponent<InventoryChecker>();
        _foodAttack = GetComponent<FoodAttack>();
    }

    private void Start()
    {
        _foodRenderer.AdjustFoodSize();
    }

    private void Update()
    {
        RotateFood();
    }

    #region Drag And Drop

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        
        isDragging = true;
        _foodRenderer.SpriteRenderer.sortingOrder = 20;
        _prevRotation = _food.RectTransform.rotation.eulerAngles.z;
        _prevHeight = _food.height;
        _prevWidth = _food.width;
        foreach (var slot in _food.slotList)
            slot.isCanEquip = true;
        _foodAttack.StopAttack();
        InventoryManager.Instance.isCanActiveKitchen = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        
        _food.RectTransform.position = GetMousePos();
        _inventoryChecker.CheckInventorySlot();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        isDragging = false;
        if (isRotating) return;

        InventoryManager.Instance.isCanActiveKitchen = true;
        DropItem();
    }

    private void DropItem()
    {
        if (!_inventoryChecker.CheckEquipInventory())
        {
            transform.position = returnPosition;
            _food.RectTransform.eulerAngles = new Vector3(0,0,_prevRotation);
            (_food.width, _food.height) = (_prevWidth, _prevHeight);
        }
        else
        {
            if (_food.isCooked)
            {
                InventoryManager.Instance.isCanCook = true;
                _food.isCooked = false;
            }
        }
        
        foreach (var slot in _food.slotList)
            slot.isCanEquip = false;
        _foodRenderer.SpriteRenderer.sortingOrder = 10;
        _foodRenderer.AdjustFoodSize();
        _inventoryChecker.ResetSlots();
    }

    #endregion

    #region Rotate
    private void RotateFood()
    {
        if(!isDragging) return;
        if(isRotating) return;
        
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            isRotating = true;
            StartCoroutine(FoodRotateCoroutine());
        }
    }

    private IEnumerator FoodRotateCoroutine()
    {
        (_food.width, _food.height) = (_food.height, _food.width);
        
        print(targetRotation);
        targetRotation = targetRotation >= 90 ? 0 : 90;
        print(targetRotation);
        Tween foodRotateTween = _food.RectTransform.DORotate(new Vector3(0,0,targetRotation), 0.7f).SetEase(Ease.OutCubic);
        _foodRenderer.ChangeFoodRotation(targetRotation);
        
        _inventoryChecker.ResetSlots();
        
        yield return foodRotateTween.WaitForCompletion();
        
        _foodRenderer.AdjustFoodSize();
        _inventoryChecker.CheckInventorySlot();
        if (!isDragging)
            DropItem();
        
        isRotating = false;
        InventoryManager.Instance.isCanActiveKitchen = true;
    }
    #endregion
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragging) return;
        
        
    }

    private Vector3 GetMousePos()
    {
        var vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0;
        return vec;
    }
}
