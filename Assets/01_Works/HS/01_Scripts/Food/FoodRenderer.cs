using UnityEngine;
using UnityEngine.Serialization;

public class FoodRenderer : MonoBehaviour
{
    private ItemDragHandler _itemDragHandler;
    public SpriteRenderer SpriteRenderer { get; private set;}

    private void Awake()
    {
        _itemDragHandler = GetComponent<ItemDragHandler>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        AdjustFoodSize();
    }

    private void AdjustFoodSize()
    {
        if (!_itemDragHandler.IsDragging) return;
        
        float distance = Vector2.Distance(transform.position, _itemDragHandler.StartPosition);
        float t = distance / 10;
        float scaleValue = Mathf.Lerp(1.66f, 2f, t);
        transform.localScale = new Vector3(scaleValue, scaleValue, 1);
    }
}