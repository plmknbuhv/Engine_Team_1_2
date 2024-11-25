using DG.Tweening;
using UnityEngine;

public class WaveBtn : MonoBehaviour
{
    

    public void OnStartGame()
    {
        transform.DOLocalMoveY(transform.localPosition.y + 200, 1).SetEase(Ease.OutBack);
    }

    public void Down()
    {
        transform.DOLocalMoveY(transform.localPosition.y - 200, 1).SetEase(Ease.InBack);
    }
}
