using UnityEngine;

public class FoodRenderer : MonoBehaviour
{
    private readonly int _gaugeValue = Shader.PropertyToID("_GaugeValue");
    private readonly int _rotateValue = Shader.PropertyToID("_RotateValue");
    private Food _food;
    private FoodDragHandler _foodDragHandler;
    public SpriteRenderer SpriteRenderer { get; private set;}
    private Material _material;

    private void Awake()
    {
        _foodDragHandler = GetComponent<FoodDragHandler>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _material = SpriteRenderer.material;
        _food = GetComponent<Food>();
    }

    private void Update()
    {
        AdjustFoodSize();
    }
    
    public void AdjustFoodGauge(float attackTimer, float foodDataAttackCooldown)
    {
        var t = attackTimer / foodDataAttackCooldown;
        var gaugeValue = Mathf.Lerp(0, 1, t);
        _material.SetFloat(_gaugeValue, gaugeValue);
    }

    public void AdjustFoodSize()
    {
        if (!_foodDragHandler.IsDragging) return;
        
        var distance = Vector2.Distance(transform.position, _foodDragHandler.startPosition);
        var t = distance / 7f;
        var scaleValue = Mathf.Lerp(1.6f, 1.97f, t);
        var canvasRectTransform = ShopManager.Instance.shopCanvas.transform as RectTransform;
        
        _food.transform.localScale = new Vector3(scaleValue * (1f / canvasRectTransform.lossyScale.x),
            scaleValue * (1f / canvasRectTransform.lossyScale.y));
    }
}