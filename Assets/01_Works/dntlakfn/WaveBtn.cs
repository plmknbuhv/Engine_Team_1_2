using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBtn : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, -700);
        transform.DOLocalMoveY(-350, 1).SetEase(Ease.InBack);
    }


    public void Down()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, -350);
        transform.DOLocalMoveY(-700, 1).SetEase(Ease.InBack);
    }
}
