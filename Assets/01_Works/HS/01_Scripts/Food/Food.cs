using System.Collections.Generic;
using GGMPool;
using UnityEngine;
using UnityEngine.Serialization;

public class Food : MonoBehaviour, IPoolable
{
    private FoodType _foodType;
    public FoodDataSO foodDataSO;
    
    [field:SerializeField] public PoolTypeSO PoolType { get; private set; }
    public GameObject GameObject => gameObject;

    private SpriteRenderer _spriteRenderer;
    private FoodRenderer _foodRenderer;
    public FoodDragHandler FoodDragHandler {get; private set;}
    private BoxCollider2D _boxCollider;
    public RectTransform RectTransform { get; private set; }
    public bool isPurchased;
    
    public List<Slot> slotList = new List<Slot>();

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        FoodDragHandler = GetComponent<FoodDragHandler>();
        _boxCollider = GetComponent<BoxCollider2D>();
        RectTransform = GetComponent<RectTransform>();
        _foodRenderer = GetComponent<FoodRenderer>();
    }

    public void SetUpFood(FoodDataSO foodDataSO)
    {
        FoodDragHandler.startPosition = transform.position;
        FoodDragHandler.returnPosition = transform.position;
        this.foodDataSO = foodDataSO;
        _foodType = foodDataSO.foodType;
        gameObject.name = _foodType.ToString();
        _spriteRenderer.sprite = foodDataSO.sprite;
        _foodRenderer.AdjustFoodSize();
        
        var colliderSize = new Vector2(foodDataSO.width * 0.5f, foodDataSO.height * 0.5f);
        _boxCollider.size = colliderSize;
    }
    
    public void SetUpPool(Pool pool)
    {
        
    }

    public void ResetItem()
    {
        
    }
}