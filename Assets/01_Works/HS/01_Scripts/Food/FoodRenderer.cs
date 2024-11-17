using UnityEngine;

public class FoodRenderer : MonoBehaviour
{
    private readonly int _gaugeValue = Shader.PropertyToID("_GaugeValue");
    private readonly int _isFoodVer = Shader.PropertyToID("_IsFoodVer");
    
    private Food _food;
    private FoodDragHandler _foodDragHandler;
    public SpriteRenderer SpriteRenderer { get; private set;}
    public Material material;

    private void Awake()
    {
        _foodDragHandler = GetComponent<FoodDragHandler>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        material = SpriteRenderer.material;
        _food = GetComponent<Food>();
    }

    private void Update()
    {
        if (!_foodDragHandler.isDragging) return;
        AdjustFoodSize();
    }

    public void ChangeFoodRotation(float isRotate)
    {
        material.SetFloat(_isFoodVer, isRotate);
    }
    
    public void AdjustFoodGauge(float attackTimer, float foodDataAttackCooldown)
    {
        var t = attackTimer / foodDataAttackCooldown;
        var gaugeValue = Mathf.Lerp(0, 1, t);
        material.SetFloat(_gaugeValue, gaugeValue);
    }

    public void AdjustFoodSize()
    {
        var distance = Vector2.Distance(transform.position, _foodDragHandler.startPosition);
        var t = distance / 10f;
        var scaleValue = Mathf.Lerp(1.6f, 1.97f, t);
        var canvasRectTransform = ShopManager.Instance.shopCanvas.transform as RectTransform;
        
        _food.transform.localScale = new Vector3(scaleValue * (1f / canvasRectTransform.lossyScale.x),
            scaleValue * (1f / canvasRectTransform.lossyScale.y));
    }
}