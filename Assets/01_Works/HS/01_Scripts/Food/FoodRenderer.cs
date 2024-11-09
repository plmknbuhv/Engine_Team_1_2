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
        AdjustFoodSize();
    }

    public void AdjustFoodSize()
    {
        if (!_foodDragHandler.IsDragging) return;
        
        var distance = Vector2.Distance(transform.position, _foodDragHandler.startPosition);
        var t = distance / 10;
        var scaleValue = Mathf.Lerp(1.65f, 1.95f, t);
        _food.RectTransform.sizeDelta = new Vector3(scaleValue, scaleValue, 1);
    }
}