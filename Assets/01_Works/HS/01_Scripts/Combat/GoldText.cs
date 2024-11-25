using System;
using System.Collections;
using DG.Tweening;
using GGMPool;
using TMPro;
using UnityEngine;
using IPoolable = GGMPool.IPoolable;

public class GoldText : MonoBehaviour, IPoolable
{
    [SerializeField] public PoolTypeSO PoolType { get; private set; }
    public GameObject GameObject => gameObject;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(int gold)
    {
        _text.text = $"+{gold}";
        StartCoroutine(ShowTextCoroutine());
    }

    private IEnumerator ShowTextCoroutine()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_text.DOFade(1f, 0.1f))
            .Insert(0.2f, _text.DOFade(0f, 0.5f));
        transform.DOMoveY(transform.position.y + 0.2f, 0.6f).SetEase(Ease.Linear);
        yield return sequence.WaitForCompletion();
    }

    public void SetUpPool(Pool pool)
    {
        
    }

    public void ResetItem()
    {
        _text.alpha = 0;
    }
}
