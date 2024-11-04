using GGMPool;
using UnityEngine;

public class Food : MonoBehaviour, IPoolable
{
    private FoodType _foodType;
    private FoodDataSO _foodDataSO;
    
    [field:SerializeField] public PoolTypeSO PoolType { get; private set; }
    public GameObject GameObject => gameObject;

    private SpriteRenderer _spriteRenderer;
    private FoodDragHandler _foodDragHandler;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _foodDragHandler = GetComponent<FoodDragHandler>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public void SetUpFood(FoodDataSO foodDataSO)
    {
        _foodDragHandler.startPosition = transform.position;
        _foodDataSO = foodDataSO;
        _foodType = _foodDataSO.foodType;
        gameObject.name = _foodType.ToString();
        _spriteRenderer.sprite = _foodDataSO.sprite;
        
        var colliderSize = new Vector2(_foodDataSO.width * 0.5f, _foodDataSO.height * 0.5f);
        _boxCollider.size = colliderSize;
    }
    
    public void SetUpPool(Pool pool)
    {
        
    }

    public void ResetItem()
    {
        
    }
}