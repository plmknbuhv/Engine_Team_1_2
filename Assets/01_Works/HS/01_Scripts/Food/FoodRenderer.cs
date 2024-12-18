using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FoodRenderer : MonoBehaviour
{
    private readonly int _gaugeValue = Shader.PropertyToID("_GaugeValue");
    private readonly int _isFoodVer = Shader.PropertyToID("_IsFoodVer");
    private readonly int _isEnterMouse = Shader.PropertyToID("_IsEnterMouse");
    
    private Food _food;
    private FoodDragHandler _foodDragHandler;
    public SpriteRenderer SpriteRenderer { get; private set;}
    public Material material;

    public bool isAnimating;
    
    private RectTransform _canvasRectTransform;

    private void Awake()
    {
        _foodDragHandler = GetComponent<FoodDragHandler>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        material = SpriteRenderer.material;
        _food = GetComponent<Food>();
        _canvasRectTransform = ShopManager.Instance.shopCanvas.transform as RectTransform;
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
        
        SetSize(scaleValue);
    }

    public void SetSize(float scaleValue)
    {
        _food.transform.localScale = new Vector3(scaleValue * (1f / _canvasRectTransform.lossyScale.x),
            scaleValue * (1f / _canvasRectTransform.lossyScale.y));
    }
    
    public void EnterMouse()
    {
        if (!_food.isPurchased) return;
        if (_foodDragHandler.isDragging) return;
        
        print("Change");
        material.SetFloat(_isEnterMouse, 1f);
    }
    
    public void ExitMouse()
    {
        material.SetFloat(_isEnterMouse, 0f);
    }

    public void DropAnimation()
    {
        StartCoroutine(DropAnimationCoroutine());
    }

    private IEnumerator DropAnimationCoroutine()
    {
        isAnimating = true;
        Tween animCoroutine = transform.DOPunchScale(Vector3.one * 13.5f, 0.3f, 3);
        yield return animCoroutine.WaitForCompletion();
        isAnimating = false;
    }
}