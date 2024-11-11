using UnityEngine;

public class FoodRenderer : MonoBehaviour
{
    private Food _food;
    private FoodDragHandler _foodDragHandler;
    public SpriteRenderer SpriteRenderer { get; private set;}

    private void Awake()
    {
        _foodDragHandler = GetComponent<FoodDragHandler>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _food = GetComponent<Food>();
    }

    private void Update()
    {
        if (!_foodDragHandler.IsDragging) return;
        AdjustFoodSize();
    }

    public void AdjustFoodSize()
    {
        var distance = Vector2.Distance(transform.position, _foodDragHandler.startPosition);
        var t = distance / 10;
        var scaleValue = Mathf.Lerp(1.6f, 1.97f, t);
        var canvasRectTransform = ShopManager.Instance.shopCanvas.transform as RectTransform;
        
        _food.transform.localScale = new Vector3(scaleValue * (1f / canvasRectTransform.lossyScale.x),
            scaleValue * (1f / canvasRectTransform.lossyScale.y));
    }
}