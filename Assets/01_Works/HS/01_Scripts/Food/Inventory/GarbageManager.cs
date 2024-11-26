using DG.Tweening;
using UnityEngine;

public class GarbageManager : MonoSingleton<GarbageManager>
{
    [SerializeField] private WasteBasket wasteBasket;

    public void ShowGarbage(bool isShowing)
    {
        wasteBasket.transform.DOScale(isShowing ? Vector3.one : Vector3.zero, 0.15f).SetEase(Ease.InOutSine);
    }
}
