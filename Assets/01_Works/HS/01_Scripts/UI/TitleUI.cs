using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private GameObject titleUI;
    
    private void Start()
    {
        MoveTitle();
    }

    private void MoveTitle()
    {
        StartCoroutine(TitleCoroutine());
    }

    private IEnumerator TitleCoroutine()
    {
        Tween UITween = titleUI.transform.DOMove(Vector3.zero, 1.1f).SetEase(Ease.InOutBack);
        
        yield return UITween.WaitForCompletion();
        yield return new WaitForSeconds(0.4f);
        
        Sequence sequence = DOTween.Sequence();
        sequence.
            Append(titleUI.transform.DOMove(Vector3.up * 2, 0.85f).SetEase(Ease.InOutSine))
            .Join(titleUI.transform.DOScale(Vector3.one / 1.6f, 0.85f).SetEase(Ease.InOutSine));
    }
}
