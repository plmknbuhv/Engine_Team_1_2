using UnityEngine;

public class TowerAnimation : MonoBehaviour
{
    private Tower _tower;
    
    private Animator _animator;
    private readonly int _attackBoolHash = Animator.StringToHash("Attack");

    public bool isAttackAnim;

    private void Awake()
    {
        _tower = GetComponentInParent<Tower>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(_attackBoolHash, isAttackAnim);
    }

    public void StopAttackAnim()
    {
        isAttackAnim = false;
    }
}
