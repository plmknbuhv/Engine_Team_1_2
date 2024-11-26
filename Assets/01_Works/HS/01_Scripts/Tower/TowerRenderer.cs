using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TowerRenderer : MonoBehaviour, ITowerComponent
{
    private Tower _tower;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    
    [SerializeField] private List<Sprite> sprites;
    
    public void Initialize(Tower tower)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tower = tower;
    }
    
    public void SetupSprite(float currentHp, float maxHp)
    {
        _spriteRenderer.sprite = sprites[Mathf.FloorToInt((currentHp / maxHp) * 3)];
    }

    public void ShakeSprite(float currentHp, float maxHp)
    {
        transform.DOShakePosition(1f, 0.25f, 7);
    }

    public void AnimateTowerDead()
    {
        // _animator
    }
}
