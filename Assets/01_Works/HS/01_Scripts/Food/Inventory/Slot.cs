using System;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private RectTransform _rectTransform;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        
    }

    private void Update()
    {
        print(_rectTransform.sizeDelta);
    }
}
