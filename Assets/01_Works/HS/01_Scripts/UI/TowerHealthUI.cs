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
        _lastHitTime = Time.time;
        hpBarObj.fillAmount = currentHp / maxHp;
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
