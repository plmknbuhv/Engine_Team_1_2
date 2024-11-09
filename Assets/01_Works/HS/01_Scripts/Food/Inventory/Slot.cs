using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Image _image;
    
    public bool isCanEquip = true;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    public void ShowSlotAvailability()
    {
        Color color;
        if (isCanEquip)
            color = Color.green;
        else
            color = Color.red;
        
        color.a = 0.38f;
        _image.color = color;
    }

    public void ResetSlotColor()
    {
        _image.color = Color.white;
    }
}
