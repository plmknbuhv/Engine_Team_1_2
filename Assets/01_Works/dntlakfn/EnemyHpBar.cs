using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] private Enemy owner;
    [SerializeField] private GameObject hpBarObj;
    [SerializeField] private GameObject hpBgBarObj;
    private float _lastHitTime;
    private bool _isChaseFill;
    [SerializeField] private float delayTime = 1;
    private void Awake()
    {
        owner = GetComponentInParent<Enemy>();
        
    }
    

    public void SetHpBar(float currentHp, float maxHp)
    {
        _lastHitTime = Time.time;
        hpBarObj.transform.localScale = new Vector3(Mathf.Clamp(currentHp / maxHp, 0f, 1f), 1f, 1f);
        //transform.DOShakePosition(0.4f, 1f, 100);
    }

    private void Update()
    {
        if (!_isChaseFill && _lastHitTime + delayTime > Time.time)
        {
            _isChaseFill = true;
            hpBgBarObj.transform.DOScaleX(hpBarObj.transform.localScale.x, 0.6f).SetEase(Ease.InCubic).OnComplete(() => _isChaseFill = false);
        }

        
        
    }

}
