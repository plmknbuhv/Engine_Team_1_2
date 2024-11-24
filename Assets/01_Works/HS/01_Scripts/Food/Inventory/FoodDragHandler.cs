using System.Collections;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FoodDragHandler : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
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
    private bool _isCanGrowing = true;
    private bool _isMouseOver;
    
    private Coroutine _coroutine;

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
        _foodRenderer.SpriteRenderer.sortingOrder = 2600;
        _foodRenderer.OnMouseExit();
        _prevRotation = _food.RectTransform.rotation.eulerAngles.z;
        _prevHeight = _food.height;
        _prevWidth = _food.width;
        foreach (var slot in _food.slotList)
            slot.isCanEquip = true;
        _foodAttack.StopAttack();

        if (_coroutine != null)
            StopCoroutine(_coroutine);
        
        InventoryManager.Instance.isCanActiveKitchen = false;
        MenuManager.Instance.isCanActiveMenu = false;
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
        
        DropItem();
    }

    private void DropItem()
    {
        InventoryManager.Instance.isCanActiveKitchen = true;
        MenuManager.Instance.isCanActiveMenu = true;
        if (!_inventoryChecker.CheckEquipInventory())
        {
            transform.position = returnPosition;
            _food.RectTransform.eulerAngles = new Vector3(0,0,_prevRotation);
            (_food.width, _food.height) = (_prevWidth, _prevHeight);
            targetRotation = _prevRotation;
        }
        else
        {
            if (_food.isCooked)
            {
                InventoryManager.Instance.isCanCook = true;
                _food.isCooked = false;
            }

            _foodRenderer.DropAnimation();
        }
        
        foreach (var slot in _food.slotList)
            slot.isCanEquip = false;
        _foodRenderer.SpriteRenderer.sortingOrder = 2500;
        _foodRenderer.AdjustFoodSize();
        _inventoryChecker.ResetSlots();

        if (_food.isPurchased && _isMouseOver)
        {
            _foodRenderer.OnMouseEnter(); 
            _coroutine = StartCoroutine(WavingFoodSizeCoroutine());
        }
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
        targetRotation = Mathf.Approximately(targetRotation, 90) ? 0 : 90;
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
    }
    #endregion
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _isMouseOver = true;
        if (!_food.isPurchased) return;
        if (isDragging) return;

        _foodRenderer.OnMouseEnter();
        _coroutine = StartCoroutine(WavingFoodSizeCoroutine());
    }

    private IEnumerator WavingFoodSizeCoroutine()
    {
        float glowTimer = 0;
        while (_isMouseOver)
        {
            yield return null;
            
            if (_foodRenderer.isAnimating)
                continue;
            
            if (_isCanGrowing)
                glowTimer += Time.deltaTime;
            else
                glowTimer -= Time.deltaTime;
            
            var t = glowTimer / 3f;
            var scaleValue = Mathf.Lerp(1.96f, 2.25f, t);
        
            _foodRenderer.SetSize(scaleValue);

            if (glowTimer >= 3.75f)
                _isCanGrowing = false;
            else if (glowTimer <= 0)
                _isCanGrowing = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isDragging) return;
        _isMouseOver = false;
        if (!_food.isPurchased) return;
        
        _foodRenderer.OnMouseExit();
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _foodRenderer.SetSize(1.97f);
    }

    private Vector3 GetMousePos()
    {
        var vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0;
        return vec;
    }
}
