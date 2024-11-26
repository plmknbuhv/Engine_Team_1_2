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
    
    private FeedbackPlayer _equipFeedbackPlayer;
    private FeedbackPlayer _swingFeedbackPlayer;
    private bool _isWasting;

    private void Awake()
    {
        _food = GetComponent<Food>();
        _foodRenderer = GetComponent<FoodRenderer>();
        _inventoryChecker = GetComponent<InventoryChecker>();
        _foodAttack = GetComponent<FoodAttack>();
        _equipFeedbackPlayer = transform.Find("EquipFeedback").GetComponent<FeedbackPlayer>();
        _swingFeedbackPlayer = transform.Find("SwingFeedback").GetComponent<FeedbackPlayer>();
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
        if (InventoryManager.Instance.isKitchenActivating) return;
        
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
        GarbageManager.Instance.ShowGarbage(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (InventoryManager.Instance.isKitchenActivating) return;
        
        _food.RectTransform.position = GetMousePos();
        _inventoryChecker.CheckInventorySlot();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (InventoryManager.Instance.isKitchenActivating) return;
        
        isDragging = false;
        if (isRotating) return;
        
        _food.TrailRenderer.Clear();
        DropItem();
    }

    private void DropItem()
    {
        _food.TrailRenderer.enabled = false;
        InventoryManager.Instance.isCanActiveKitchen = true;
        MenuManager.Instance.isCanActiveMenu = true;
        
        RaycastHit2D hit = Physics2D.Raycast(GetMousePos(), Vector2.zero);
        if (hit.transform.CompareTag("Garbage") && _food.isPurchased)
        {
            StartCoroutine(WasteCoroutine(hit));
            return;
        }
        GarbageManager.Instance.ShowGarbage(false);
        
        if (!_inventoryChecker.CheckEquipInventory())
        {
            transform.position = returnPosition;
            _food.RectTransform.eulerAngles = new Vector3(0,0,_prevRotation);
            (_food.width, _food.height) = (_prevWidth, _prevHeight);
            targetRotation = _prevRotation;
            _foodRenderer.ChangeFoodRotation(targetRotation);
            foreach (var slot in _food.slotList)
                slot.isCanEquip = false;
        }
        else
        {
            if (_food.isCooked)
            {
                InventoryManager.Instance.isCanCook = true;
                _food.isCooked = false;
            }

            _foodRenderer.DropAnimation();
            _equipFeedbackPlayer.PlayFeedbacks();
        }
        _foodRenderer.SpriteRenderer.sortingOrder = 2500;
        _foodRenderer.AdjustFoodSize();
        _inventoryChecker.ResetSlots();

        if (_food.isPurchased && _isMouseOver)
        {
            _foodRenderer.OnMouseEnter(); 
            _coroutine = StartCoroutine(WavingFoodSizeCoroutine());
        }
        _food.TrailRenderer.enabled = true;
        _foodAttack.StartAttack();
    }

    private IEnumerator WasteCoroutine(RaycastHit2D hit)
    {
        _isWasting = true;
        var waste = hit.transform.GetComponent<WasteBasket>();
        
        Tween animCoroutine = transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        yield return animCoroutine.WaitForCompletion();
        waste.ThrowGarbage(transform.position); 
        
        _food.slotList.ForEach(slot => slot.isCanEquip = true);
        _food.slotList.Clear();
        
        yield return new WaitForSeconds(0.5f);
        GarbageManager.Instance.ShowGarbage(false);

        _isWasting = false;
        _food.myPool.Push(_food);
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
        _swingFeedbackPlayer.PlayFeedbacks();
        (_food.width, _food.height) = (_food.height, _food.width);
        
        targetRotation = Mathf.Approximately(targetRotation, 90) ? 0 : 90;
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
        if (_isWasting) return;
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

            if (glowTimer >= 3f)
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
