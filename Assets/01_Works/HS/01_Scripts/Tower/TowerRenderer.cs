using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TowerRenderer : MonoBehaviour, ITowerComponent
{
    private Tower _tower;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    
    private int _towerAnimationHash = Animator.StringToHash("HP");
    
    [SerializeField] private List<Sprite> sprites;
    
    public void Initialize(Tower tower)
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tower = tower;
    }
    
    public void SetupSprite(float currentHp, float maxHp)
    {
        _animator.SetFloat(_towerAnimationHash, (currentHp / maxHp) * 3f);
        if (currentHp <= 0)
            _tower.isDead = true;
    }

    public void ShakeSprite(float currentHp, float maxHp)
    {
        transform.DOShakePosition(1f, 0.25f, 7);
    }

    public void TowerDeadTrigger()
    {
        MenuManager.Instance.GameOver();
    }
}
