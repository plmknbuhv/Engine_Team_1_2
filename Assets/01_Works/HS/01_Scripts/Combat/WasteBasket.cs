using DG.Tweening;
using GGMPool;
using UnityEngine;

public class WasteBasket : MonoBehaviour
{
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO poolType;
        
    public void ThrowGarbage(Vector3 position)
    {
        var effect = poolManager.Pop(poolType) as FoodEffect;
        effect.SetPositionAndPlay(position);
        transform.DOShakePosition(1f, 10, 10);
    }
}
