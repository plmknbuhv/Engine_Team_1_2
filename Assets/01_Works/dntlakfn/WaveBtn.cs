using DG.Tweening;
using UnityEngine;

public class WaveBtn : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 200);
    }

    public void OnStartGame()
    {
        transform.DOLocalMoveY(transform.localPosition.y + 200, 1).SetEase(Ease.OutBack);
    }

    public void Down()
    {
        transform.DOLocalMoveY(transform.localPosition.y - 200, 1).SetEase(Ease.InBack);
    }
}
