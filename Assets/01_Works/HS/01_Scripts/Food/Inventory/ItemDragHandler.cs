using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 _startPosition;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
        transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 여기서 인벤토리 시스템 구현
    }
}
