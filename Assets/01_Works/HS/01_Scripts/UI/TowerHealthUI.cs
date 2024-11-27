using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealthUI : MonoBehaviour
{
    [SerializeField] private Image hpBarObj;
    [SerializeField] private Image hpBgBarObj;
    private float _lastHitTime;
    private bool _isChaseFill;
    [SerializeField] private float delayTime = 1;

    public void SetHpBar(float currentHp, float maxHp)
    {
        try
        {
            hpBarObj.fillAmount = currentHp / maxHp;
        }
        catch (DivideByZeroException exception)
        {
            Console.WriteLine("최대 체력이 0으로 되어있습니다.");    
            throw;
        }// 예외처리
        
        _lastHitTime = Time.time;
        transform.DOShakePosition(0.4f, 1f, 100);
    }
    
    private void Update()
    {
        if (!_isChaseFill && _lastHitTime + delayTime > Time.time)
        {
            _isChaseFill = true;
            hpBgBarObj.DOFillAmount(hpBarObj.fillAmount, 0.6f)
                .SetEase(Ease.InCubic)
                .OnComplete(() => _isChaseFill = false);
        }
    }
}
