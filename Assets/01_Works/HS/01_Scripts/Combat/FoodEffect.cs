using System.Collections;
using GGMPool;
using UnityEngine;

public class FoodEffect : MonoBehaviour, IPoolable
{
    [field:SerializeField] public PoolTypeSO PoolType { get; private set; }
    public GameObject GameObject => gameObject;
    private Pool _myPool;
    
    private ParticleSystem _particle;
    private float _duration;
    private WaitForSeconds _particleDuration;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
        _duration = _particle.main.duration;
        _particleDuration = new WaitForSeconds(_duration);
    }   
    
    public void SetPositionAndPlay(Vector3 position)
    {
        transform.position = position;
        _particle.Play();
        StartCoroutine(DelayAndGotoPoolCoroutine());
    }

    private IEnumerator DelayAndGotoPoolCoroutine()
    {
        yield return _particleDuration; 
        _myPool.Push(this);
    }
    
    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }

    public void ResetItem()
    {
        
    }
}
