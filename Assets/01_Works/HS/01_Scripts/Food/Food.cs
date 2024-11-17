using System.Collections.Generic;
using GGMPool;
using UnityEngine;

public class Food : MonoBehaviour, IPoolable
{
    private FoodType _foodType;
    public FoodDataSO foodDataSO;
    
    [field:SerializeField] public PoolTypeSO PoolType { get; private set; }
    public GameObject GameObject => gameObject;

    public FoodRenderer FoodRenderer {get; private set;}
    public FoodDragHandler FoodDragHandler {get; private set;}
    public InventoryChecker InventoryChecker { get; private set; }
    private BoxCollider2D _boxCollider;
    public FoodAttack FoodAttack { get; private set; }
    
    public RectTransform RectTransform { get; private set; }
    public bool isPurchased;
    public int width;
    public int height;
    
    public List<Slot> slotList = new List<Slot>();

    private void Awake()
    {
        InventoryChecker = GetComponent<InventoryChecker>();
        FoodDragHandler = GetComponent<FoodDragHandler>();
        _boxCollider = GetComponent<BoxCollider2D>();
        RectTransform = GetComponent<RectTransform>();
        FoodRenderer = GetComponent<FoodRenderer>();
        FoodAttack = GetComponent<FoodAttack>();
    }

    public void SetUpFood(FoodDataSO foodDataSO)
    {
        width = foodDataSO.width;
        height = foodDataSO.height;
        FoodDragHandler.returnPosition = transform.position;
        this.foodDataSO = foodDataSO;
        _foodType = foodDataSO.foodType;
        gameObject.name = _foodType.ToString();
        FoodRenderer.SpriteRenderer.sprite = foodDataSO.sprite;
        FoodRenderer.AdjustFoodSize();
        FoodDragHandler.SetUpFood();
        FoodAttack.Initialize(this);
        
        var colliderSize = new Vector2(width * 0.5f, height * 0.5f);
        _boxCollider.size = colliderSize;
    }
    
    public void SetUpPool(Pool pool)
    {
        
    }

    public void ResetItem()
    {
        isPurchased = false;
        foreach (var slot in slotList)
            slot.isCanEquip = true;
        slotList.Clear();
        FoodRenderer.AdjustFoodSize();
        FoodRenderer.AdjustFoodGauge(1, 1);
        FoodRenderer.ChangeFoodRotation(0);
    }
}