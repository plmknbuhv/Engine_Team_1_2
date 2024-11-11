using System;
using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FoodDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private FoodRenderer _foodRenderer;
    private InventoryChecker _inventoryChecker;
    private Food _food;
    
    public Vector3 startPosition;
    [HideInInspector] public Vector3 returnPosition;
    
    public bool IsDragging { get; private set; }
    public bool IsRotating { get; private set; }
    private float _targetRotation;
    private float _prevRotation;
    private int _prevHeight;
    private int _prevWidth;

    private void Awake()
    {
        _food = GetComponent<Food>();
        _foodRenderer = GetComponent<FoodRenderer>();
        _inventoryChecker = GetComponent<InventoryChecker>();
    }

    private void Start()
    {
        _foodRenderer.AdjustFoodSize();
    }

    private void Update()
    {
        RotateFood();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        
        IsDragging = true;
        _foodRenderer.SpriteRenderer.sortingOrder = 20;
        _prevRotation = _food.RectTransform.rotation.eulerAngles.z;
        _prevHeight = _food.height;
        _prevWidth = _food.width;
        foreach (var slot in _food.slotList)
            slot.isCanEquip = true;
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
        IsDragging = false;
        if (IsRotating) return;

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
        
        foreach (var slot in _food.slotList)
        {
            slot.isCanEquip = false;
        }
        _inventoryChecker.ResetSlots();
        _foodRenderer.SpriteRenderer.sortingOrder = 10;
        _foodRenderer.AdjustFoodSize();
    }

    private void RotateFood()
    {
        if(!IsDragging) return;
        if(IsRotating) return;
        
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            IsRotating = true;
            StartCoroutine(FoodRotateCoroutine());
        }
    }

    private IEnumerator FoodRotateCoroutine()
    {
        (_food.width, _food.height) = (_food.height, _food.width);
        _targetRotation += 90;
        Tween foodRotateTween = _food.RectTransform.DORotate(new Vector3(0,0,_targetRotation), 0.7f).SetEase(Ease.OutCubic);
        _inventoryChecker.ResetSlots();
        yield return foodRotateTween.WaitForCompletion();
        _foodRenderer.AdjustFoodSize();
        _inventoryChecker.CheckInventorySlot();
        if (!IsDragging)
            DropItem();
        IsRotating = false;
    }

    private Vector3 GetMousePos()
    {
        var vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0;
        return vec;
    }
}
