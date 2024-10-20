using UnityEngine;

public class TowerAnimation : MonoBehaviour
{
    private Animator _animator;
    private readonly int _attackBoolHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(_attackBoolHash, true);
    }
}
